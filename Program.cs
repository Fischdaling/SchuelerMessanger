using Microsoft.Extensions.Options;
using SchuelerChatBackendProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Bind MongoDbSettings from config
builder.Services.Configure<MongoDbSettings>(
	builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton(sp =>
{
	var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
	return new StudentContext(settings.ConnectionString, settings.DatabaseName);
});

// Add Controllers etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
