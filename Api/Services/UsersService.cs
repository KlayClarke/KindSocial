using Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Services;

public class UsersService
{
  private readonly IMongoCollection<User> _usersCollection;

  public UsersService(IOptions<KindSocialDatabaseSettings> kindSocialDatabaseSettings)
  {
    var mongoClient = new MongoClient(
      kindSocialDatabaseSettings.Value.ConnectionString
    );

    var mongoDatabase = mongoClient.GetDatabase(
      kindSocialDatabaseSettings.Value.DatabaseName
    );

    _usersCollection = mongoDatabase.GetCollection<User>(
      kindSocialDatabaseSettings.Value.UsersCollectionName
    );
  }

  public async Task<List<User>> GetUsers() => await _usersCollection.Find(_ => true).ToListAsync(); // get list of users

  public async Task<User?> GetUser(string id) => await _usersCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); // get specified user (by id)

  public async Task CreateUser(User newUser) => await _usersCollection.InsertOneAsync(newUser); // create new user

  public async Task UpdateUser(string id, User updatedUser) => await _usersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser); // update specified user (by id)

  public async Task RemoveUser(string id) => await _usersCollection.DeleteOneAsync(x => x.Id == id); // delete specified user (by id)
}