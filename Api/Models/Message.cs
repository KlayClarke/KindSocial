using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models;

public class Message
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string Body { get; set; } = null!;
  public DateTime DateCreated { get; set; }

  [BsonRepresentation(BsonType.ObjectId)]
  public string AuthorId { get; set; } = null!;

  [BsonRepresentation(BsonType.ObjectId)]
  public string RecieverId { get; set; } = null!;
}