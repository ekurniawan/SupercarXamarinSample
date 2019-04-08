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
    public partial class DetailPage : ContentPage
    {
        private LeaderBoardServices _myServices;
        private UserProfile _myProfile;
        private void BeginInit()
        {
            if (Application.Current.Properties.ContainsKey("user"))
            {
                if (Application.Current.Properties["user"] == null)
                {
                    CommentPanel.IsVisible = false;
                    //VoteButton.IsVisible = false;
                    LoginButton.IsVisible = true;
                    btnAdmin.IsVisible = false;
                }
                else
                {
                    //CommentPanel.IsVisible = true;
                    //VoteButton.IsVisible = true;
                    LoginButton.IsVisible = false;
                    _myProfile = (UserProfile)Application.Current.Properties["user"];

                    if (_myProfile.IsAdmin.HasValue)
                    {
                        if (_myProfile.IsAdmin == true)
                        {
                            btnAdmin.IsVisible = true;
                        }
                    }

                    //Supercar mydata = await _myServices.GetLeaderboardById(Convert.ToInt32(entrySuperCarId.Text));

                    List<Vote> lstVote = (List<Vote>)lvComment.ItemsSource;
                    lstVote.RemoveAll(item => item == null);

                    var recVote = (from v in lstVote
                                     where  v.FirstName == _myProfile.FirstName
                                     select v).FirstOrDefault();

                    if (recVote != null)
                        CommentPanel.IsVisible = false;
                    else
                        CommentPanel.IsVisible = true;


                    /*var countVote = (from v in mydata.Votes
                                     where v.FirstName == _myProfile.FirstName
                                     select v).FirstOrDefault();

                    if(countVote!=null)
                        CommentPanel.IsVisible = false;
                    else
                        CommentPanel.IsVisible = true;*/

                }
            }
        }

        public DetailPage()
        {
            InitializeComponent();
            _myServices = new LeaderBoardServices();
            //BeginInit();
        }

        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        protected override void OnAppearing()
        {
            BeginInit();
        }

        private async void SubmitButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                VoteInputView inputVote = new VoteInputView
                {
                    comments = entryComment.Text,
                    supercarId = Convert.ToInt32(entrySuperCarId.Text),
                    userId = _myProfile.UserId
                };
                await _myServices.PostVote(inputVote);
                //var dataBaru = await _myServices.GetLeaderboardById(Convert.ToInt32(entrySuperCarId));
                //this.BindingContext = dataBaru;
                await DisplayAlert("Keterangan", "Vote Berhasil Ditambah", "OK");

                var data = await _myServices.GetLeaderboardById(Convert.ToInt32(entrySuperCarId.Text));
                data.Votes.RemoveAll(item => item == null);
                this.BindingContext = data;
                BeginInit();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Kesalahan", $"{ex.Message}", "OK");
            }
        }

       

        private async void BtnAdmin_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdminPage());
        }
    }
}