namespace SchuelerChatBackendProject.DTO;

public record SendMessageDto(string MessageText, Guid SenderId, Guid ReceiverId);
