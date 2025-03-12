using Boxed.AspNetCore;
using Carter;
using Inventory.Common.Options;
using Inventory.Data;
using Inventory.Handlers.Inventories.Commands;
using Inventory.Handlers.Inventories.Queries;
using Inventory.Repositories.Repositories;
using Inventory.Repositories.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Load configuration based on environment
builder.Configuration
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = typeof(Program).Assembly;

builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateInventoryHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetInventoryByIdHandler).Assembly));

builder.Services.AddCarter();


var configuration = builder.Configuration;

builder.Services.ConfigureAndValidateSingleton<SqlServerOption>(configuration.GetSection("SqlServer"));

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddTransient<IEntityStateManager, EntityStateManager>();
builder.Services.AddScoped<IDataContext, DataContext>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


app.MapCarter();

app.UseHttpsRedirection();

app.Run();