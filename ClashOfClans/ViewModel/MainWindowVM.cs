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
                                                          string tag = (MainPage.DataContext as RankingsPageVM)
                                                              .SelectedUserInfo.Tag;
                                                          (MainPage.DataContext as UserPageVM)
                                                              .GetUserInfo(tag);
                                                      })));
    }
}
