using MongoDB.Bson.Serialization.Attributes;

namespace SchuelerChatBackendProject;

public record Address(string Strasse, int Hausnummer, string Posleitzahl, string Postort);