using MongoDB.Bson.Serialization.Attributes;

namespace SchuelerChatBackendProject;

public class Student : BaseEntity
{
	public Student(string vorname, string nachname, char geschlecht, DateOnly gebDatum, Address address, string staatsbuerger, Klasse klasse)
	{
		Vorname = vorname;
		Nachname = nachname;
		Geschlecht = geschlecht;
		GebDatum = gebDatum;
		Address = address;
		Staatsbuerger = staatsbuerger;
		Klasse = klasse;
	}

	[BsonElement("vorname")]
	public string Vorname { get; set; }
	[BsonElement("nachname")]
	public string Nachname { get; set; }
	[BsonElement("geschlecht")]
	public char Geschlecht { get; set; }
	[BsonElement("gebDatum")]
	public DateOnly GebDatum { get; set; }
	[BsonElement("address")]
	public Address Address { get; set; }
	[BsonElement("religion")]
	public Religion Religion { get; set; }
	[BsonElement("staatsbuerger")]
	public string Staatsbuerger { get; set; }
	[BsonElement("klasse")]
	public Klasse Klasse { get; set; }
	
	public List<Message> Messages { get; set; } = [];

	public Message SendMessage(Student receiver, string text)
	{
		var message = new Message(text, this, receiver);
		Messages.Add(message);
		return message;
	}
	protected Student() {}
}