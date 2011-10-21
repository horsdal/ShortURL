namespace ShortUrl
{
	using Nancy;
	using DataAccess;

	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureApplicationContainer(TinyIoC.TinyIoCContainer container)
		{
			base.ConfigureApplicationContainer(container);

			container.Register<UrlStore>(new MongoUrlStore());
		}
	}
}
