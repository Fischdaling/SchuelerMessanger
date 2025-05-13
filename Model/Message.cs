namespace SchuelerChatBackendProject;

public class Message : BaseEntity
{
	public Message(string messageText, Student sender, Student receiver)
	{
		MessageText = messageText;
		Sender = sender;
		Receiver = receiver;
	}

	public string MessageText { get; set; }
	public Student Sender { get; set; }
	public Student Receiver { get; set; }

	protected Message(Student sender, Student receiver)
	{
		Sender = sender;
		Receiver = receiver;
	}
}