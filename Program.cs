using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using TournamentManagementSystem.BusinessServices;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DbContexts;
using TournamentManagementSystem.Helpers;
using TournamentManagementSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TournamentDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ISystemRepository, SystemRepository>();
builder.Services.AddScoped<ITournamentService, TournamentService>();
builder.Services.AddScoped<IOrganizerService, OrganizerService>();
builder.Services.AddScoped<ITeamService, TeamService>();

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

//Не ради ништа сам по себи, већ само гледа да ли је HttpContext.User присутан и има дозволу.
app.UseAuthorization();

//Мапира путање ка контролерима, Позива одговарајући контролер метод ако је руту нашао
app.MapControllers();

//Ако га имаш после MapControllers(), неће се ни позвати,
//јер MapControllers() већ враћа резултат ако је руту нашао.
app.Run();
