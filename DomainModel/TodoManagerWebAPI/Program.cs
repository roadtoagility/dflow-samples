using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using TodoManagerApp.Business;
using TodoManagerApp.Persistence;
using TodoManagerWebAPI.ApiEndpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoAppDbContext>((options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ModelConnection"))));

builder.Services.AddRepositories();
builder.Services.AddCommandHandlers();
builder.Services.AddMediatorOperations();
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

TodoApiEndpoints.StateChangeApis(app);

app.Run();