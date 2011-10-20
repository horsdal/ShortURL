namespace ShortUrl
{
	using System.Collections.Generic;
	using Nancy;
	
	public class ShortUrlModule : NancyModule
    {
        private static readonly Dictionary<string, string> urlMap = new Dictionary<string, string>();

        public ShortUrlModule(UrlStore urlStore)
        {
            Get["/"] = _ => View["index.html"];
            Post["/"] = _ => ShortenUrl(urlStore);
            Get["/{shorturl}"] = param =>
            {
                string shortUrl = param.shorturl;
                return Response.AsRedirect(urlMap[shortUrl.ToString()]);
            };
        }

        private Response ShortenUrl(UrlStore urlStore)
        {
            string longUrl = Request.Form.url;
            var shortUrl = ShortenUrl(longUrl);
         //   urlMap[shortUrl] = longUrl;
			urlStore.SaveUrl(longUrl, shortUrl);

			return View["shortened_url", new { Host = Request.Headers.Host, ShortUrl = shortUrl }];
        }

        private string ShortenUrl(string longUrl)
        {
            return "a" + longUrl.GetHashCode();
        }
    }
}
