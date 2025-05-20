using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SchuelerChatBackendProject;

public class Student : BaseEntity
{
	public Student(string vorname, string nachname, string geschlecht, DateOnly gebDatum,  string staatsbuerger, Klasse klasse, string strasse, int hausnummer, string posleitzahl, string postort)
	{
		Vorname = vorname;
		Nachname = nachname;
		Geschlecht = geschlecht;
		GebDatum = gebDatum;
		Staatsbuerger = staatsbuerger;
		Klasse = klasse;
		Strasse = strasse;
		Hausnummer = hausnummer;
		Posleitzahl = posleitzahl;
		Postort = postort;
	}
	
	[BsonExtraElements]
	public BsonDocument CatchAll { get; set; }

	[BsonElement("S_Nr")]
	public string StudentNumber { get; set; }
	
	[BsonElement("S_Zuname")]
	public string Nachname { get; set; }
	[BsonElement("S_Vorname")]
	public string Vorname { get; set; }
	
	[BsonElement("S_Geschlecht")]
	public string Geschlecht { get; set; }
	[BsonElement("S_Gebdatum")]
	public DateOnly GebDatum { get; set; }
	[BsonElement("S_Strasse")]
	public string Strasse { get; set; }
	[BsonElement("S_Hausnummer")]
	public int Hausnummer { get; set; }
	[BsonElement("S_Postleitzahl")]
	public string Posleitzahl { get; set; }
	[BsonElement("S_Postort")]
	public string Postort { get; set; }
	
	[BsonElement("S_Religion")]
	public Religion Religion { get; set; }
	[BsonElement("S_Staatsbuergerschaft")]
	public string Staatsbuerger { get; set; }
	[BsonElement("S_Klasse")]
	public Klasse Klasse { get; set; }
	
	public List<Message> Messages { get; set; } = [];

	public Message SendMessage(Student receiver, string text)
	{
		var message = new Message(text, this, receiver);
		Messages.Add(message);
		return message;
	}
	protected Student()
	{
	}
}