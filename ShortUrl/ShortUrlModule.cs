namespace ShortUrl
{
	using System.Collections.Generic;
	using Nancy;
	
	public class ShortUrlModule : NancyModule
    {
        private static readonly Dictionary<string, string> urlMap = new Dictionary<string, string>();

        public ShortUrlModule()
        {
            Get["/"] = _ => View["index.html"];
            Post["/"] = _ => ShortenUrl();
            Get["/{shorturl}"] = param =>
            {
                string shortUrl = param.shorturl;
                return Response.AsRedirect(urlMap[shortUrl.ToString()]);
            };
        }

        private Response ShortenUrl()
        {
            string longUrl = Request.Form.url;
            var shortUrl = ShortenUrl(longUrl);
            urlMap[shortUrl] = longUrl;

			return View["shortened_url", new { Host = Request.Headers.Host, ShortUrl = shortUrl }];
        }

        private string ShortenUrl(string longUrl)
        {
            return "a" + longUrl.GetHashCode();
        }
    }
}
