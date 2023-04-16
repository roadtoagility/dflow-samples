using Ecommerce.Business.Extensions;
using Ecommerce.Persistence;
using Ecommerce.Persistence.ExtensionMethods;
using EcommerceWebAPI.ApiEndpoints;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    // .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
    .AddJsonFile($"appsettings.{builder.Environment}.json", true, false);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<EcommerceAppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ModelConnection"),
        o => o.UseNodaTime()));

builder.Services.AddRepositories();
builder.Services.AddHandlers();
builder.Services.AddWorkers();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(o =>
{
    o.SerializerOptions.IgnoreReadOnlyProperties = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

EndpointRoutes.StateChangeApis(app);

app.Run();