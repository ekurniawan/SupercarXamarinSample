using SampleRESTSecurity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleRESTSecurity
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdminPage : ContentPage
	{
        private LeaderBoardServices _myServices;
        public AdminPage ()
		{
			InitializeComponent ();
            _myServices = new LeaderBoardServices();
		}

        protected async override void OnAppearing()
        {
            var data = await _myServices.GetAllUser();
            lvUser.ItemsSource = data;
        }
    }
}