using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Api.Models;

public class User
{
  [BsonId]
  [BsonRepresentation(BsonType.ObjectId)]
  public string? Id { get; set; }
  public string DisplayName { get; set; } = null!;
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;

}