using Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Api.Services;

public class MessagesService
{
  private readonly IMongoCollection<Message> _messagesCollection;

  public MessagesService(IOptions<KindSocialDatabaseSettings> kindSocialDatabaseSettings)
  {
    var mongoClient = new MongoClient(
      kindSocialDatabaseSettings.Value.ConnectionString
    );

    var mongoDatabase = mongoClient.GetDatabase(
      kindSocialDatabaseSettings.Value.DatabaseName
    );

    _messagesCollection = mongoDatabase.GetCollection<Message>(
      kindSocialDatabaseSettings.Value.MessagesCollectionName
    );
  }

  public async Task<List<Message>> GetMessages() => await _messagesCollection.Find(_ => true).ToListAsync(); // get list of messages

  public async Task<Message?> GetMessage(string id) => await _messagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync(); // get specific message (if id matches)

  public async Task CreateMessage(Message newMessage) => await _messagesCollection.InsertOneAsync(newMessage); // create new message

  public async Task UpdateMessage(string id, Message updatedMessage) => await _messagesCollection.ReplaceOneAsync(x => x.Id == id, updatedMessage); // update specific message (by id)

  public async Task RemoveMessage(string id) => await _messagesCollection.DeleteOneAsync(x => x.Id == id); // delete specified message (by id)
}