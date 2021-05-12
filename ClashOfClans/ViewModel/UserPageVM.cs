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
    class UserPageVM : ViewModelBase
    {
        private UserInfo _currentUserInfo = new UserInfo();

        public UserInfo CurrentUserInfo
        {
            get => _currentUserInfo;
            set
            {
                _currentUserInfo = value;
                RaisePropertyChanged();
            }
        }

        private ClashOfClansRepository _currentClashOfClansRepository = new ClashOfClansRepository();

        public ClashOfClansRepository CurrentClashOfClansRepository
        {
            get => _currentClashOfClansRepository;
            set => _currentClashOfClansRepository = value;
        }

        //private RelayCommand _getUserInfoCommand;

        //public RelayCommand GetUserInfoCommand => _getUserInfoCommand ?? (_getUserInfoCommand =
        //    new RelayCommand(() => CurrentClashOfClansRepository.GetUserInfoAsync(CurrentUserInfo.Tag)));

        public void GetUserInfo(string tag)
        {
            var taskRes = CurrentClashOfClansRepository.GetUserInfoAsync(tag);
            taskRes.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
            {
                CurrentUserInfo = taskRes.Result;
                RaisePropertyChanged("CurrentUserInfo");
            });
        }
    }
}
