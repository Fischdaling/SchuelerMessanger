using Microsoft.EntityFrameworkCore;

namespace SchuelerChatBackendProject.Infrastructure;

public class StudentContext (DbContextOptions<StudentContext> options) : DbContext(options)
{
	public DbSet<Student> Students { get; set; }
	public DbSet<Message> Messages { get; set; }
}