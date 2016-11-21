﻿using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace XamarinRest
{
	public partial class XamarinRestPage : ContentPage
	{
		public ObservableCollection<Country> Countries { get; set;}
		//public bool IsBusy {get; set; }
		private RestClient _client;
		public Command RefreshCommand { get; set;}

		public XamarinRestPage()
		{
			RefreshCommand = new Command(() => Load());
			Countries = new ObservableCollection<Country>();
			_client = new RestClient();
			InitializeComponent();

		}

		public async void Load()
		{
			var result = await _client.GetCountries();
			Countries.Clear();

			foreach (var item in result)
			{
				Countries.Add(item);
			}
			IsBusy = false;
		}
	}
}
