using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using TournamentManagementSystem.BusinessServices;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DbContexts;
using TournamentManagementSystem.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TournamentDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
builder.Services.AddScoped<ITournamentService, TournamentService>();

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
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
