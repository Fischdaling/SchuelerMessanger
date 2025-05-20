using Microsoft.Extensions.Options;
using SchuelerChatBackendProject.Controllers;
using SchuelerChatBackendProject.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton(sp =>
{
	return new StudentContext("mongodb://localhost:27017","schueler");
});

builder.Services.AddSignalR();
builder.Services.AddSingleton<Neo4jService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
