using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClashOfClans.Model;
using GalaSoft.MvvmLight;

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
    }
}
