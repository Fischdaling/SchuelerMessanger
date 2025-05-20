using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SchuelerChatBackendProject;

public abstract class BaseEntity
{
	[BsonId] [BsonRepresentation(BsonType.ObjectId)]public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
	public DateTime CreatedAt = DateTime.Now;
	
}