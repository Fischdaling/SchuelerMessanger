using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace SchuelerChatBackendProject;

public abstract class BaseEntity
{
	[BsonId] 
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime CreatedAt = DateTime.Now;
}