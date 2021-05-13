using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashOfClans.Model;
using GalaSoft.MvvmLight;

namespace ClashOfClans.ViewModel
{
    class RankingsPageVM : ViewModelBase
    {
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
            set => _selectedUserInfo = value;
        }
    }
}
