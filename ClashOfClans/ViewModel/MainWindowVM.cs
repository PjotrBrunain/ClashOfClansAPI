using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using ClashOfClans.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ClashOfClans.ViewModel
{
    class MainWindowVM : ViewModelBase
    {
        public string Tag { get; set; }
        public MainWindowVM()
        {
            CurrentPage = MainPage;
            //Tag = (MainPage.DataContext as RankingsPageVM).SelectedUserInfo.Tag;
        }

        private RankingsPage _mainPage = new RankingsPage();

        public RankingsPage MainPage
        {
            get => _mainPage;
            set => _mainPage = value;
        }

        private UserPage _userDetailPage = new UserPage();

        public UserPage UserDetailPage
        {
            get => _userDetailPage;
            set => _userDetailPage = value;
        }

        private Page _currentPage;

        public Page CurrentPage
        {
            get => _currentPage;
            set => _currentPage = value;
        }

        private RelayCommand _getUserInfoCommand;

        public RelayCommand GetUserInfoCommand => _getUserInfoCommand ??
                                                  (_getUserInfoCommand =
                                                      new RelayCommand((() =>
                                                      {
                                                          if (CurrentPage is RankingsPage)
                                                          {
                                                              if ((MainPage.DataContext as RankingsPageVM)
                                                                  .SelectedUserInfo != null)
                                                              {
                                                                  string tag = (MainPage.DataContext as RankingsPageVM)
                                                                      .SelectedUserInfo.Tag;
                                                                  if (!String.IsNullOrWhiteSpace(tag))
                                                                  {
                                                                      tag = tag.Remove(0, 1);
                                                                      (UserDetailPage.DataContext as UserPageVM)
                                                                          .GetUserInfo(tag);
                                                                      CurrentPage = UserDetailPage;
                                                                  }
                                                              }
                                                          }
                                                          RaisePropertyChanged("CurrentPage");
                                                      })));

        private RelayCommand _backCommand;

        public RelayCommand BackCommand => _backCommand ?? (_backCommand = new RelayCommand(() =>
        {
            if (CurrentPage is UserPage)
            {
                CurrentPage = MainPage;
            }
            RaisePropertyChanged("CurrentPage");
        }));
    }
}
