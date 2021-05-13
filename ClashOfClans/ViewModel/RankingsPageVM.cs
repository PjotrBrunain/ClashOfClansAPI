using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashOfClans.Model;
using ClashOfClans.Repository;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ClashOfClans.ViewModel
{
    class RankingsPageVM : ViewModelBase
    {
        public RankingsPageVM()
        {
            var taskRes = CurrentClashOfClansRepository.GetCountryListAsync();
            taskRes.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
            {
                Countries = taskRes.Result;
                RaisePropertyChanged("Countries");
            });
            //var taskRes = CurrentClashOfClansRepository.GetRankingsListAsync(LocationId, Amount);
            //taskRes.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
            //{
            //    RankingsList = taskRes.Result;
            //    RaisePropertyChanged("RankingsList");
            //});
        }

        public List<Country> Countries { get; set; } = new List<Country>();

        public Country SelectedCountry { get; set; }

        public ClashOfClansRepository CurrentClashOfClansRepository { get; set; } = new ClashOfClansRepository();

        private List<UserInfo> _rankingsList = new List<UserInfo>();

        public List<UserInfo> RankingsList
        {
            get => _rankingsList;
            set => _rankingsList = value;
        }

        private UserInfo _selectedUserInfo;

        public UserInfo SelectedUserInfo
        {
            get => _selectedUserInfo;
            set
            {
                _selectedUserInfo = value;
            }
        }

        //public string Location { get; set; } = "Belgium";

        //public int LocationId { get; set; } = 32000029;

        private RelayCommand _getRankListCommand;

        public RelayCommand GetRankListCommand => _getRankListCommand ?? (_getRankListCommand = new RelayCommand(() =>
        {
            var taskRes = CurrentClashOfClansRepository.GetRankingsListAsync(SelectedCountry.Id, Amount);
            taskRes.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
            {
                RankingsList = taskRes.Result;
                RaisePropertyChanged("RankingsList");
            });
        }));

        public int Amount { get; set; } = 50;
    }
}
