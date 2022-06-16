using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models;

public class Comment
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string Body { get; set; } = null!;
  public DateTime DateCreated { get; set; }

  [BsonRepresentation(BsonType.ObjectId)]
  public string PostId { get; set; } = null!;

  [BsonRepresentation(BsonType.ObjectId)]
  public string AuthorId { get; set; } = null!;
}