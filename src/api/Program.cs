using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using TournamentManagementSystem.BusinessServices;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DbContexts;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.Helpers;
using TournamentManagementSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TournamentDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
{
    opts.Password.RequireDigit = true;
    opts.Password.RequiredLength = 6;
    opts.Password.RequireNonAlphanumeric = false;
    opts.User.RequireUniqueEmail = true;
})
    //use TournamentDbContext to store user and role data in the database 
    .AddEntityFrameworkStores<TournamentDbContext>()
    .AddDefaultTokenProviders();

// 1) Bind JwtSettings from configuration
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));
// 2) Grab a copy for use in setting up JWT middleware:
var jwtSettings = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtSettings>()!;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        //Configures the middleware to save the received JWT in the authentication properties
        options.SaveToken = true;
        options.TokenValidationParameters =
        new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Key)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Auth failed: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("✅ JWT token validated");
                Console.WriteLine("User: " + context.Principal.Identity.Name);
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CanManageTournaments", policy => policy.RequireRole("Organizer", "Admin"));
});

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<ISystemRepository, SystemRepository>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<IOrganizerService, OrganizerService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IPlayerMatchStatsService, PlayerMatchStatsService>();

builder.Services.AddControllers(configure =>
{
    configure.ReturnHttpNotAcceptable = true;
})
    .AddNewtonsoftJson(setupAction =>
    {
        setupAction.SerializerSettings.ContractResolver =
        new CamelCasePropertyNamesContractResolver();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(
    AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    new UnprocessableEntityObjectResult(context.ModelState);
});

//FLUENT VALIDATION
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TournamentDbContext>();

    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    //identity
    await scope.ServiceProvider.EnsureSeedDataAsync();
}

// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionHandlingMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
//Проверава да ли је захтев дошао преко HTTP-а. Ако јесте, аутоматски га преусмерава на HTTPS
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

//Не ради ништа сам по себи, већ само гледа да ли је HttpContext.User присутан и има дозволу.
app.UseAuthorization();

//Мапира путање ка контролерима, Позива одговарајући контролер метод ако је руту нашао
app.MapControllers();

//Ако га имаш после MapControllers(), неће се ни позвати,
//јер MapControllers() већ враћа резултат ако је руту нашао.
app.Run();
