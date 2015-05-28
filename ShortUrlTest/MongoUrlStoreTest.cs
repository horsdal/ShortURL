namespace ShortUrlTest
{
  using System.Threading.Tasks;
  using MongoDB.Bson;
  using MongoDB.Driver;
  using ShortUrl.DataAccess;
  using Xunit;

  public class MongoUrlStoreTest
	{
		private string connectionString = "mongodb://localhost:27010/short_url_test";
		private IMongoDatabase database;
		private IMongoCollection<BsonDocument> urlCollection;

		public MongoUrlStoreTest()
		{
      //given
      this.database = new MongoClient(this.connectionString).GetDatabase("short_url");
      this.urlCollection = this.database.GetCollection<BsonDocument>("urls");
		}

		[Fact]
		public async Task should_store_urls_in_mongo()
		{
			//when
			var store = new MongoUrlStore(this.connectionString);
			store.SaveUrl("http://somelongurl.com/", "http://shorturl/abc");

			//then
			var urlFromDB = await this.urlCollection
				.Find(Builders<BsonDocument>.Filter.Eq("url", "http://somelongurl.com/"))
				.FirstOrDefaultAsync();

			Assert.NotNull(urlFromDB);
			Assert.Equal(urlFromDB["shortenedUrl"], "http://shorturl/abc");
		}

		[Fact]
		public void should_be_able_to_find_shortened_urls()
		{
			//given
			var store = new MongoUrlStore(this.connectionString);
			store.SaveUrl("http://somelongurl.com/", "http://shorturl/abc");

			//when
			var longUrl = store.GetUrlFor("http://shorturl/abc");

			//then
			Assert.Equal("http://somelongurl.com/", longUrl);
		}
	}
}
