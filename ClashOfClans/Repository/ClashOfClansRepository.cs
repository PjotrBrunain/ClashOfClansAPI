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
        private string _apiToken;

        public async Task<UserInfo> GetUserInfoAsync(string userTag)
        {
            String endpoint = $"https://api.clashofclans.com/v1/players/{userTag}";
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
                        DonationsReceived = jObject.SelectToken("donationsReceived").ToObject<int>(),
                        ClanName = jObject.SelectToken("clan").SelectToken("name").ToString(),
                        ClanTag = jObject.SelectToken("clan").SelectToken("tag").ToString()
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
    }
}
