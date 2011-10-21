namespace ShortUrl
{
	using Nancy;
	using DataAccess;

	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureApplicationContainer(TinyIoC.TinyIoCContainer container)
		{
			base.ConfigureApplicationContainer(container);

			var mongoUrlStore = new MongoUrlStore("mongodb://localhost:27010/short_url");
			container.Register<UrlStore>(mongoUrlStore);
		}
	}
}
