using System.ComponentModel.DataAnnotations;

namespace SchuelerChatBackendProject;

public abstract class BaseEntity
{
	[Key] 
	public Guid Id { get; set; } = Guid.NewGuid();
	public DateTime CreatedAt = DateTime.Now;
}