namespace ShortUrl.DataAccess
{
    using System.Threading;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoUrlStore : UrlStore
	{
		private IMongoDatabase database;
		private IMongoCollection<BsonDocument> urls;

		public MongoUrlStore(string connectionString)
		{
			database = new MongoClient(connectionString).GetDatabase("short_url");
			urls = database.GetCollection<BsonDocument>("urls");
		}

      public void SaveUrl(string url, string shortenedUrl)
      {
        var newDoc = new BsonDocument {{"Id", url}, { "url",  url }, { "shortenedUrl", shortenedUrl} };
        urls.InsertOneAsync(newDoc, CancellationToken.None);
      }

      public string GetUrlFor(string shortenedUrl)
		{
			var urlDocument =  
				urls
				.Find(Builders<BsonDocument>.Filter.Eq("shortenedUrl", shortenedUrl))
				.FirstOrDefaultAsync()
        .Result;

			return 
				urlDocument == null ? 
				null : urlDocument["url"].AsString;
		}
	}
}
