namespace SchuelerChatBackendProject;

public class User
{
	public string Vorname { get; set; }
	public string Nachname { get; set; }
	public char Geschlecht { get; set; }
	public DateOnly GebDatum { get; set; }
	public Address Address { get; set; }
	public enum Religion { get; set; }
	
}