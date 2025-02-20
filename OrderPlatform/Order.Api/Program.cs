using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Order.Common.Options;
using Order.Data;
using Order.Repositories.Repositories;
using Order.Repositories.Repositories.Interfaces;
using Order.Services;
using Order.Services.Interfaces;
using FastEndpoints.Swagger;
using Microsoft.Extensions.Logging;

var bld = WebApplication.CreateBuilder();
var configuration = bld.Configuration;
bld.Services.AddCors(); 

bld.Services.AddFastEndpoints().SwaggerDocument();

bld.Services.AddEndpointsApiExplorer();

bld.Services.Configure<SqlServerOption>(configuration.GetSection("SqlServer"));
bld.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<SqlServerOption>>().Value);

bld.Services.AddScoped<IOrderRepository, OrderRepository>();
bld.Services.AddScoped<IOrderService, OrderService>();
bld.Services.AddTransient<IEntityStateManager, EntityStateManager>();
bld.Services.AddScoped<IDataContext, DataContext>();

var app = bld.Build();
app.UseFastEndpoints().UseSwaggerGen();

app.UseOpenApi();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

app.Run();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("App is starting...");