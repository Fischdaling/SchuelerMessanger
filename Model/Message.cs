namespace SchuelerChatBackendProject;

public class Message : BaseEntity
{
	public Message(string messageText)
	{
		MessageText = messageText;
	}

	public string MessageText { get; set; }

	protected Message(){}
}