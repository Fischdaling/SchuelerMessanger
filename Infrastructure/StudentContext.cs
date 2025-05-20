using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace SchuelerChatBackendProject.Infrastructure;

public class StudentContext
{
	private readonly IMongoDatabase _database;
	
	public StudentContext(string connectionString, string databaseName)
	{
		var client = new MongoClient(connectionString);
		_database = client.GetDatabase(databaseName);
	}

	public IMongoCollection<Student> Students => _database.GetCollection<Student>("Schueler");
	public IMongoCollection<Message> Messages => _database.GetCollection<Message>("Messages");
}