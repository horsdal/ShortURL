namespace ShortUrlTest
{
	using Xunit;
	using Nancy;
	using Nancy.Testing;
	using ShortUrl;
	
	public class UrlStorageTest
	{
		private Browser app;

		[Fact]
		public void app_should_store_url_when_one_is_posted()
		{
			//Given
			var fakeStorage = new Moq.Mock<UrlStore>();

			ShortUrlModule artificiaReference;
			app = new Browser(new ConfigurableBootstrapper(
				with => with.Dependency(fakeStorage.Object)));

			//When
			var baseUrlPostResponse = app.Post("/",
				with =>
				{
					with.FormValue("url", "http://www.longurlplease.com/");
					with.HttpRequest();
				});

			//Then
			fakeStorage
				.Verify(store => 
					store.SaveUrl("http://www.longurlplease.com/", Moq.It.IsAny<string>()));
		}
	}
}
