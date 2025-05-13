using Microsoft.AspNetCore.Mvc;

namespace SchuelerChatBackendProject.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController : ControllerBase
{
	[HttpGet]
	public IActionResult GetAllStudends()
	{
		
	}
}