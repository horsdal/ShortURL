namespace ShortUrl
{
	public interface UrlStore
	{
		void SaveUrl(string url, string shortenedUrl);
	}
}