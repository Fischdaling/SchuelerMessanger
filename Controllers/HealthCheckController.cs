using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SchuelerChatBackendProject.Infrastructure;

namespace SchuelerChatBackendProject.Controllers;

[ApiController]
[Route("api/health")]
public class HealthCheckController : ControllerBase
{
	private readonly StudentContext _studentContext;

	public HealthCheckController(StudentContext studentContext)
	{
		_studentContext = studentContext;
	}

	[HttpGet]
	public async Task<IActionResult> Check()
	{
		try
		{
			// Check if MongoDB is accessible by counting documents
			long studentCount = await _studentContext.Students.CountDocumentsAsync(FilterDefinition<Student>.Empty);
			long messageCount = await _studentContext.Messages.CountDocumentsAsync(FilterDefinition<Message>.Empty);

			return Ok(new {
				mongoDb = "Connected",
				studentDocuments = studentCount,
				messageDocuments = messageCount
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, new {
				error = "MongoDB connection failed",
				details = ex.Message
			});
		}
	}
}