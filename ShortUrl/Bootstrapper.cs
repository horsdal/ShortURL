namespace ShortUrl
{
  using Nancy;
  using Nancy.Bootstrapper;
  using Nancy.ViewEngines;
  using DataAccess;
  using Nancy.TinyIoc;

  public class Bootstrapper : DefaultNancyBootstrapper
  {
    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
      base.ConfigureApplicationContainer(container);

      var mongoUrlStore = new MongoUrlStore("mongodb://localhost:27010/");
      container.Register<UrlStore>(mongoUrlStore);
    }

    protected override NancyInternalConfiguration InternalConfiguration
    {
      get
      {
        return NancyInternalConfiguration.WithOverrides(
          x => x.ViewLocationProvider = typeof (ResourceViewLocationProvider));
      }
    }
  }
}
