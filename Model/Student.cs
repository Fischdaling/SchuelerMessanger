namespace SchuelerChatBackendProject;

public class Student : BaseEntity
{
	public Student(string vorname, string nachname, char geschlecht, DateOnly gebDatum, Address address, string staatsbuerger, string klasse)
	{
		Vorname = vorname;
		Nachname = nachname;
		Geschlecht = geschlecht;
		GebDatum = gebDatum;
		Address = address;
		Staatsbuerger = staatsbuerger;
		Klasse = klasse;
	}

	public string Vorname { get; set; }
	public string Nachname { get; set; }
	public char Geschlecht { get; set; }
	public DateOnly GebDatum { get; set; }
	public Address Address { get; set; }
	public Religion Religion { get; set; }
	public string Staatsbuerger { get; set; }
	public string Klasse { get; set; }
	public List<Message> Messages { get; set; } = [];

	public Message SendMessage(Student receiver, string text)
	{
		var message = new Message(text, this, receiver);
		Messages.Add(message);
		return message;
	}
	protected Student() {}
}