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
using Scalar.AspNetCore;

var bld = WebApplication.CreateBuilder();
var configuration = bld.Configuration;
bld.Services.AddFastEndpoints();
bld.Services.AddFastEndpoints().SwaggerDocument();


bld.Services.Configure<SqlServerOption>(configuration.GetSection("SqlServer"));
bld.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<SqlServerOption>>().Value);

bld.Services.AddScoped<IOrderRepository, OrderRepository>();
bld.Services.AddScoped<IOrderService, OrderService>();
bld.Services.AddTransient<IEntityStateManager, EntityStateManager>();
bld.Services.AddScoped<IDataContext, DataContext>();

var app = bld.Build();
app.UseFastEndpoints();
app.UseOpenApi(c => c.Path = "/openapi/v1.json");
app.MapScalarApiReference(options =>
{
    options.Title = "Order API Documentation";
    options.Theme = ScalarTheme.Mars;
});

app.Run();