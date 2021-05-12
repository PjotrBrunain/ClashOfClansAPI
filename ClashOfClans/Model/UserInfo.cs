using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfClans.Model
{
    public class UserInfo
    {
        public String Tag { get; set; }
        public String UserName { get; set; }
        public int TownHallLevel { get; set; }
        public int ExpLevel { get; set; }
        public int Trophies { get; set; }
        public int BestTrophies { get; set; }
        public int WarStars { get; set; }
        public int AttackWins { get; set; }
        public int DefenseWins { get; set; }
        public int BuilderHallLevel { get; set; }
        public int VersusTrophies { get; set; }
        public int BestVersusTrophies { get; set; }
        public int VersusBattleWins { get; set; }
        public String Role { get; set; }
        public int Donations { get; set; }
        public int DonationsReceived { get; set; }
        public String ClanName { get; set; }
        public String ClanTag { get; set; }
    }
}
