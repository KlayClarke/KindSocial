using Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Services;

public class CommentsService
{
  private readonly IMongoCollection<Comment> _commentsCollection;

  public CommentsService(IOptions<KindSocialDatabaseSettings> kindSocialDatabaseSettings)
  {
    var mongoClient = new MongoClient(
      kindSocialDatabaseSettings.Value.ConnectionString
    );

    var mongoDatabase = mongoClient.GetDatabase(
      kindSocialDatabaseSettings.Value.DatabaseName
    );

    _commentsCollection = mongoDatabase.GetCollection<Comment>(
      kindSocialDatabaseSettings.Value.CommentsCollectionName
    );
  }

  public async Task<List<Comment>> GetComments() => await _commentsCollection.Find(_ => true).ToListAsync(); // get list of comments

  public async Task<Comment?> GetComment(string id) => await _commentsCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); // get specified comment (by id)

  public async Task CreateComment(Comment newComment) => await _commentsCollection.InsertOneAsync(newComment); // create new comment

  public async Task UpdateComment(string id, Comment updatedComment) => await _commentsCollection.ReplaceOneAsync(x => x.Id == id, updatedComment); // update specified comment (by id)

  public async Task RemoveComment(string id) => await _commentsCollection.DeleteOneAsync(x => x.Id == id); // delete specified comment (by id)
}