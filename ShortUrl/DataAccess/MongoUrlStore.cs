namespace ShortUrl.DataAccess
{
	using System;
	using System.Collections.Generic;

	public class MongoUrlStore : UrlStore
	{
		private static readonly Dictionary<string, string> urlMap = new Dictionary<string, string>();

		public void SaveUrl(string url, string shortenedUrl)
		{
			urlMap[shortenedUrl] = url;
		}

		public string GetUrlFor(string shortenedUrl)
		{
			return urlMap[shortenedUrl];
		}
	}
}
