using MongoDB.Bson;

namespace SchuelerChatBackendProject.DTO;

public record SendMessageDto(string MessageText, ObjectId SenderId, ObjectId ReceiverId);
