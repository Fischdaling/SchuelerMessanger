using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using SchuelerChatBackendProject.DTO;
using SchuelerChatBackendProject.Infrastructure;

namespace SchuelerChatBackendProject.Controllers;

[ApiController]
[Route("api/students")]
public class StudentController(StudentContext db, Neo4jService neo) : ControllerBase
{
	
	[HttpGet]
	public async Task<ActionResult<List<Student>>> GetAllStudents()
	{
		try
		{
			var students = await db.Students.Find(_ => true).ToListAsync();
			return Ok(students);
		}
		catch (Exception ex)
		{
			return StatusCode(500, new { message = "An error occurred", details = ex.Message });
		}
	}
	
	[HttpGet("{id:guid}")]
	public async Task<IActionResult> GetStudendById(ObjectId id)
	{
		if (!ObjectId.TryParse(id.ToString(), out _))
			return BadRequest(new { Message = "Invalid ID format" });

		var student = await db.Students.Find(s => s.Id == id).FirstOrDefaultAsync();
		if (student == null)
			return NotFound(new { Message = $"Student with id {id} not found" });

		return Ok(student);
	}
	
	

	[HttpPost]
	public async Task<IActionResult> SendMessage(SendMessageDto dto)
	{
		if (!ObjectId.TryParse(dto.SenderId.ToString(), out _) || !ObjectId.TryParse(dto.ReceiverId.ToString(), out _))
			return BadRequest(new { Message = "Invalid sender or receiver ID format" });

		var sender =await db.Students.Find(s => s.Id == dto.SenderId).FirstOrDefaultAsync();
		if (sender == null)
			return NotFound(new { Message = $"Sender with id {dto.SenderId} not found" });

		var receiver = await db.Students.Find(s => s.Id == dto.ReceiverId).FirstOrDefaultAsync();
		if (receiver == null)
			return NotFound(new { Message = $"Receiver with id {dto.ReceiverId} not found" });

		var message = sender.SendMessage(receiver, dto.MessageText);

		// Optional: Nachricht auch in separate Messages-Sammlung speichern
		db.Messages.InsertOneAsync(message);

		// Update sender with new message
		var update = Builders<Student>.Update.Push(s => s.Messages, message);
		db.Students.UpdateOneAsync(s => s.Id.Equals(sender.Id), update);

		return Ok(message);
	}
	
	[HttpGet("/countHops")]
	public async Task<IActionResult> GetFriendsOfFriendsCount(string user1Id, string user2Id)
	{
		var count = await neo.GetShortestFriendPathLengthAsync(user1Id,user2Id);
		return Ok(new { count });
	}

}