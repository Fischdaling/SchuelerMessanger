using Microsoft.AspNetCore.Mvc;
using SchuelerChatBackendProject.DTO;
using SchuelerChatBackendProject.Infrastructure;

namespace SchuelerChatBackendProject.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController(StudentContext db) : ControllerBase
{
	[HttpGet]
	public IActionResult GetAllStudends()
	{
		return Ok(db.Students.ToList());
	}
	
	[HttpGet("{id:guid}")]
	public IActionResult GetStudendById(Guid id)
	{
		var student = db.Students.Find(id);
		if (student == null)
		{
			return NotFound(new {Message = $"student with id {id} not found"});
		}

		return Ok(student);
	}
	
	
	[HttpDelete("{id:guid}")]
	public IActionResult DeleteCustomer(Guid id)
	{
		var student = db.Students.Find(id);
		if (student == null)
		{
			return NotFound(new {Message = $"Customer with id {id} not found"});
		}
		db.Students.Remove(student);
		db.SaveChanges();
		return NoContent();
	}

	[HttpPost]
	public IActionResult SendMessage(SendMessageDto dto)
	{
		var sender = db.Students.Find(dto.SenderId);
		if (sender == null)
		{
			return NotFound(new {Message = $"Customer with id {dto.SenderId} not found"});
		}
		var receiver = db.Students.Find(dto.ReceiverId);
		if (receiver == null)
		{
			return NotFound(new {Message = $"Customer with id {dto.SenderId} not found"});
		}

		sender.SendMessage(receiver, dto.MessageText);
		db.SaveChanges();
		return Ok();
	}
	
}