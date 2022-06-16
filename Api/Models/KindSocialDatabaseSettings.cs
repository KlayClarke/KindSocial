namespace Api.Models;

public class KindSocialDatabaseSettings
{
  public string ConnectionString { get; set; } = null!;
  public string DatabaseName { get; set; } = null!;
  public string UsersCollectionName { get; set; } = null!;
  public string PostsCollectionName { get; set; } = null!;
  public string CommentsCollectionName { get; set; } = null!;
  public string MessagesCollectionName { get; set; } = null!;

}