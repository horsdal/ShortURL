using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace ShortUrl.DataAccess
{
	using System.Collections.Generic;

	public class MongoUrlStore : UrlStore
	{

#if false
		private static readonly Dictionary<string, string> urlMap = new Dictionary<string, string>();

		public void SaveUrl(string url, string shortenedUrl)
		{
			urlMap[shortenedUrl] = url;
		}

		public string GetUrlFor(string shortenedUrl)
		{
			return urlMap[shortenedUrl];
		}
#endif
		private MongoDatabase database;
		private MongoCollection<BsonDocument> urls;

		public MongoUrlStore(string connectionString)
		{
			database = MongoDatabase.Create(connectionString);
			urls = database.GetCollection("urls");
		}

		public void SaveUrl(string url, string shortenedUrl)
		{
			urls.Insert(new {url, shortenedUrl});
		}

		public string GetUrlFor(string shortenedUrl)
		{
			var urlDocument =  
				urls
				.Find(Query.EQ("shortenedUrl", shortenedUrl))
				.FirstOrDefault();

			return 
				urlDocument == null ? 
				null : urlDocument["url"].AsString;
		}
	}
}
