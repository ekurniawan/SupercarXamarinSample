using SampleRESTSecurity.Models;
using SampleRESTSecurity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SampleRESTSecurity
{
    public partial class MainPage : ContentPage
    {
        private LeaderBoardServices _myServices;
        public MainPage()
        {
            InitializeComponent();
            _myServices = new LeaderBoardServices();
        }

        protected async override void OnAppearing()
        {
            lvLeaderboard.ItemsSource = await _myServices.GetLeaderboards();

            /*try
            {
                
            }
            catch(Exception ex)
            {
                await DisplayAlert("Kesalahan", ex.Message, "OK");
            }*/
        }

        private async void LvLeaderboard_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var car = e.Item as Leaderboard;
            try
            {
                var data = await _myServices.GetLeaderboardById(car.SupercarId);
                var detailPage = new DetailPage();
                data.Votes.RemoveAll(item => item == null);
                detailPage.BindingContext = data;
               

                await Navigation.PushAsync(detailPage);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
           
        }
    }
}
