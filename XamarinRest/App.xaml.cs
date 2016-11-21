using Xamarin.Forms;

namespace XamarinRest
{
	public partial class App : Application
	{
		private XamarinRestPage _mainPage;

		public App()
		{
			_mainPage = new XamarinRestPage();
			InitializeComponent();

			MainPage = _mainPage;
		}

		protected override  void OnStart()
		{
			/*var client = new XamarinRest.RestClient();
			var json = client.Serialize();

			await MainPage.DisplayAlert("Json:", json, "Cancel");*/
			_mainPage.Load();
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
