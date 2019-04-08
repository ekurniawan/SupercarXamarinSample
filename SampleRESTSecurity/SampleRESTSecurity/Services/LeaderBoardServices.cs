using Newtonsoft.Json;
using SampleRESTSecurity.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SampleRESTSecurity.Services
{
    public class LeaderBoardServices 
    {
        private string hostUrl = @"http://hack-yourself-first.com/";
        private HttpClient _client;
        public LeaderBoardServices()
        {
            _client = new HttpClient();
        }

        public async Task<Supercar> GetLeaderboardById(int supercarId)
        {
            Supercar leaderBoard = new Supercar();
            var myUri = new Uri(hostUrl + $"api/supercar/{supercarId}");
            try
            {
                var response = await _client.GetAsync(myUri);
                if (response.IsSuccessStatusCode)
                {
                    var context = await response.Content.ReadAsStringAsync();
                    leaderBoard = JsonConvert.DeserializeObject<Supercar>(context);
                }
                return leaderBoard;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<UserProfile>> GetAllUser()
        {
            List<UserProfile> lstUserProfile = new List<UserProfile>();
            var myUri = new Uri(hostUrl + "api/admin/users");
            try
            {
                var response = await _client.GetAsync(myUri);
                if (response.IsSuccessStatusCode)
                {
                    var context = await response.Content.ReadAsStringAsync();
                    lstUserProfile = JsonConvert.DeserializeObject<List<UserProfile>>(context);
                }
                return lstUserProfile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Leaderboard>> GetLeaderboards()
        {
            List<Leaderboard> listLeaderboard = new List<Leaderboard>();
            var myUri = new Uri(hostUrl + "api/supercar/leaderboard?limitResults=10");
            try
            {
                var response = await _client.GetAsync(myUri);
                if (response.IsSuccessStatusCode)
                {
                    var context = await response.Content.ReadAsStringAsync();
                    listLeaderboard = JsonConvert.DeserializeObject<List<Leaderboard>>(context);
                }
                return listLeaderboard;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task PostVote(VoteInputView inputVote)
        {
            var uriPost = new Uri(hostUrl + "api/Vote");
            try
            {
                var jsonData = JsonConvert.SerializeObject(inputVote);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "PhoneApp");
                HttpResponseMessage response = await _client.PostAsync(uriPost, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Gagal menambah data: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<UserProfile> LoginUser(UserLoginView loginParam)
        {
            UserProfile userProfile = new UserProfile();
            var uriPost = new Uri(hostUrl + "api/login");
            try
            {
                var jsonData = JsonConvert.SerializeObject(loginParam);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uriPost, content);
                
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"Gagal menambah data: {response.StatusCode}");
                }
                else
                {
                    var resContent = await response.Content.ReadAsStringAsync();
                    userProfile = JsonConvert.DeserializeObject<UserProfile>(resContent);
                    return userProfile;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
