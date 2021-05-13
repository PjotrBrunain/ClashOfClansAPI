using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClashOfClans.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClashOfClans.Repository
{
    class ClashOfClansRepository
    {
        private string _apiToken = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzUxMiIsImtpZCI6IjI4YTMxOGY3LTAwMDAtYTFlYi03ZmExLTJjNzQzM2M2Y2NhNSJ9.eyJpc3MiOiJzdXBlcmNlbGwiLCJhdWQiOiJzdXBlcmNlbGw6Z2FtZWFwaSIsImp0aSI6Ijc5NzYxMjFmLTIzZTItNGJjYi1hMmY0LTExODk5M2UzNWRiYiIsImlhdCI6MTYyMDg1NzI1Niwic3ViIjoiZGV2ZWxvcGVyLzczNjAxZDMzLWYwYTktOWZkNi03MzhkLTNkNTZjZDI4ZDZiZiIsInNjb3BlcyI6WyJjbGFzaCJdLCJsaW1pdHMiOlt7InRpZXIiOiJkZXZlbG9wZXIvc2lsdmVyIiwidHlwZSI6InRocm90dGxpbmcifSx7ImNpZHJzIjpbIjYyLjIzNS4xOTguMTU2Il0sInR5cGUiOiJjbGllbnQifV19.RuyE3FPVyVsLOP-CHR9XTbKLSRntRegQulsePlgjn8z8SKMmPeXcOleWUlBMI_7qisJ0lHAPj3VtNKx5EcIZgQ";

        public string ApiToken
        {
            get => _apiToken;
            set => _apiToken = value;
        }
        public async Task<UserInfo> GetUserInfoAsync(string userTag)
        {
            String endpoint = $"https://api.clashofclans.com/v1/players/%23{userTag}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string contentType = "application/json";
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);

                    var response = await client.GetAsync(endpoint);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new HttpRequestException(response.ReasonPhrase);
                    }

                    string json = await response.Content.ReadAsStringAsync();

                    JObject jObject = JsonConvert.DeserializeObject<JObject>(json);

                    UserInfo userInfo = new UserInfo()
                    {
                        Tag = jObject.SelectToken("tag").ToString(),
                        UserName = jObject.SelectToken("name").ToString(),
                        TownHallLevel = jObject.SelectToken("townHallLevel").ToObject<int>(),
                        ExpLevel = jObject.SelectToken("expLevel").ToObject<int>(),
                        Trophies = jObject.SelectToken("trophies").ToObject<int>(),
                        BestTrophies = jObject.SelectToken("bestTrophies").ToObject<int>(),
                        WarStars = jObject.SelectToken("warStars").ToObject<int>(),
                        AttackWins = jObject.SelectToken("attackWins").ToObject<int>(),
                        DefenseWins = jObject.SelectToken("defenseWins").ToObject<int>(),
                        BuilderHallLevel = jObject.SelectToken("builderHallLevel").ToObject<int>(),
                        VersusTrophies = jObject.SelectToken("versusTrophies").ToObject<int>(),
                        BestVersusTrophies = jObject.SelectToken("bestVersusTrophies").ToObject<int>(),
                        VersusBattleWins = jObject.SelectToken("versusBattleWins").ToObject<int>(),
                        Role = jObject.SelectToken("role").ToString(),
                        Donations = jObject.SelectToken("donations").ToObject<int>(),
                        DonationsReceived = jObject.SelectToken("donationsReceived").ToObject<int>()
                        //ClanName = jObject.SelectToken("clan").SelectToken("name").ToString(),
                        //ClanTag = jObject.SelectToken("clan").SelectToken("tag").ToString()
                    };

                    return userInfo;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task<List<Country>> GetCountryListAsync()
        {
            string endPoint = $"https://api.clashofclans.com/v1/locations";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string contentType = "application/json";
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);

                    var response = await client.GetAsync(endPoint);
                    if (!response.IsSuccessStatusCode) throw new HttpRequestException(response.ReasonPhrase);

                    string json = await response.Content.ReadAsStringAsync();
                    JObject jObject = JsonConvert.DeserializeObject<JObject>(json);

                    List<Country> countries = new List<Country>();

                    foreach (var jObject1 in jObject.SelectToken("items").ToObject<List<JObject>>())
                    {
                        if (jObject1.SelectToken("isCountry").ToObject<bool>())
                        {
                            countries.Add(new Country(){Id = jObject1.SelectToken("id").ToObject<int>(), name = jObject1.SelectToken("name").ToString()});
                        }
                    }

                    return countries;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task<List<UserInfo>> GetRankingsListAsync(int locationId, int amount)
        {
            string endPoint = $"https://api.clashofclans.com/v1/locations/{locationId}/rankings/players?limit={amount}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string contentType = "application/json";
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);

                    var response = await client.GetAsync(endPoint);

                    if (!response.IsSuccessStatusCode) throw new HttpRequestException(response.ReasonPhrase);

                    string json = await response.Content.ReadAsStringAsync();

                    JObject mainJObject = JsonConvert.DeserializeObject<JObject>(json);

                    List<JObject> jObjects = mainJObject.SelectToken("items").ToObject<List<JObject>>();

                    List<UserInfo> userList = new List<UserInfo>();

                    foreach (JObject jObject in jObjects)
                    {
                        UserInfo user = new UserInfo()
                        {
                            Tag = jObject.SelectToken("tag").ToString(),
                            UserName = jObject.SelectToken("name").ToString(),
                            Trophies = jObject.SelectToken("trophies").ToObject<int>()
                        };

                        if (jObject.ContainsKey("clan"))
                        {
                            user.ClanName = jObject.SelectToken("clan").SelectToken("name").ToString();
                        }
                        userList.Add(user);
                    }

                    return userList;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
