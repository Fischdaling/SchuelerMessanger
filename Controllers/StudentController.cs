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
	public IActionResult GetAllStudends()
	{
		var students = db.Students.Find(_ => true).ToListAsync();
		return Ok(students);
	}
	
	[HttpGet("{id:guid}")]
	public IActionResult GetStudendById(Guid id)
	{
		if (!ObjectId.TryParse(id.ToString(), out _))
			return BadRequest(new { Message = "Invalid ID format" });

		var student = db.Students.Find(s => s.Id == id).FirstOrDefaultAsync();
		if (student == null)
			return NotFound(new { Message = $"Student with id {id} not found" });

		return Ok(student);
	}
	
	
	[HttpDelete("{id:guid}")]
	public IActionResult DeleteCustomer(Guid id)
	{
		if (!ObjectId.TryParse(id.ToString(), out _))
			return BadRequest(new { Message = "Invalid ID format" });

		var result =  db.Students.DeleteOneAsync(s => s.Id == id);
		if (result.Result.DeletedCount == 0)
			return NotFound(new { Message = $"Student with id {id} not found" });

		return NoContent();
	}

	[HttpPost]
	public IActionResult SendMessage(SendMessageDto dto)
	{
		if (!ObjectId.TryParse(dto.SenderId.ToString(), out _) || !ObjectId.TryParse(dto.ReceiverId.ToString(), out _))
			return BadRequest(new { Message = "Invalid sender or receiver ID format" });

		var sender = db.Students.Find(s => s.Id == dto.SenderId).FirstOrDefaultAsync();
		if (sender == null)
			return NotFound(new { Message = $"Sender with id {dto.SenderId} not found" });

		var receiver = db.Students.Find(s => s.Id == dto.ReceiverId).FirstOrDefaultAsync();
		if (receiver == null)
			return NotFound(new { Message = $"Receiver with id {dto.ReceiverId} not found" });

		var message = sender.Result.SendMessage(receiver.Result, dto.MessageText);

		// Optional: Nachricht auch in separate Messages-Sammlung speichern
		db.Messages.InsertOneAsync(message);

		// Update sender with new message
		var update = Builders<Student>.Update.Push(s => s.Messages, message);
		db.Students.UpdateOneAsync(s => s.Id.Equals(sender.Id), update);

		return Ok(message);
	}
	
	[HttpGet("/countHops")]
	public async Task<IActionResult> GetFriendsOfFriendsCount(Guid user1Id, Guid user2Id)
	{
		var count = await neo.GetShortestFriendPathLengthAsync(user1Id,user2Id);
		return Ok(new { count });
	}

}