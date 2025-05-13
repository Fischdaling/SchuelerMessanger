using Microsoft.Extensions.Options;
using SchuelerChatBackendProject.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.Configure<MongoDbSettings>(
	builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton(sp =>
{
	var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
	return new StudentContext(settings.ConnectionString, settings.DatabaseName);
});
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
