namespace ShortUrl
{
    using Nancy;
    using Nancy.Responses.Negotiation;

  public class ShortUrlModule : NancyModule
    {
        public ShortUrlModule(UrlStore urlStore)
        {
            Get["/"] = _ => View["index.html"];
            Post["/"] = _ => ShortenUrl(urlStore);
            Get["/{shorturl}"] = param =>
            {
                string shortUrl = param.shorturl;
                return Response.AsRedirect(urlStore.GetUrlFor(shortUrl.ToString()));
            };
        }

        private Negotiator ShortenUrl(UrlStore urlStore)
        {
            string longUrl = Request.Form.url;
            var shortUrl = ShortenUrl(longUrl);
            urlStore.SaveUrl(longUrl, shortUrl);
            
            return View["shortened_url", new { Request.Headers.Host, ShortUrl = shortUrl }];
        }

        private string ShortenUrl(string longUrl)
        {
            return longUrl.GetHashCode().ToString();
        }
    }
}
