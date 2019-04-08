using SampleRESTSecurity.Models;
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
	public partial class LoginPage : ContentPage
	{
        private LeaderBoardServices _myServices;
        public LoginPage ()
		{
			InitializeComponent ();
            _myServices = new LeaderBoardServices();
		}

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                UserLoginView inputLogin = new UserLoginView
                {
                    Email = entryEmail.Text,
                    Password = entryPasswword.Text,
                    RememberMe = true
                };

                var resData = await _myServices.LoginUser(inputLogin);
                Application.Current.Properties["user"] = resData;
                await DisplayAlert("Keterangan", $"Login Berhasil: {resData.UserId} - {resData.FirstName}", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Kesalahan", ex.Message, "OK");
            }
        }
    }
}