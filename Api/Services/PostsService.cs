using Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Services;

public class PostsService
{
  private readonly IMongoCollection<Post> _postsCollection;

  public PostsService(IOptions<KindSocialDatabaseSettings> kindSocialDatabaseSettings)
  {
    var mongoClient = new MongoClient(
      kindSocialDatabaseSettings.Value.ConnectionString
    );

    var mongoDatabase = mongoClient.GetDatabase(
      kindSocialDatabaseSettings.Value.DatabaseName
    );

    _postsCollection = mongoDatabase.GetCollection<Post>(
      kindSocialDatabaseSettings.Value.PostsCollectionName
    );
  }

  public async Task<List<Post>> GetPosts() => await _postsCollection.Find(_ => true).ToListAsync(); // get list of posts

  public async Task<Post?> GetPost(string id) => await _postsCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); // get specific post (if id matches)

  public async Task CreatePost(Post newPost) => await _postsCollection.InsertOneAsync(newPost); // create new post

  public async Task UpdatePost(string id, Post updatedPost) => await _postsCollection.ReplaceOneAsync(x => x.Id == id, updatedPost); // update specific post (if id matches)

  public async Task RemovePost(string id) => await _postsCollection.DeleteOneAsync(x => x.Id == id); // delete specific post (if id matches)
}