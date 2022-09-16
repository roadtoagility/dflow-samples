// Copyright (C) 2022  Road to Agility
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at https://mozilla.org/MPL/2.0/.


using Ecommerce.Business.Extensions;
using Ecommerce.Persistence;
using Ecommerce.Persistence.ExtensionMethods;
using EcommerceAPI.ApiEndpoints;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EcommerceAppDbContext>((options =>
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

EndpointRoutes.StateChangeApis(app);

app.Run();