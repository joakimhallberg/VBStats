using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;

using VolleyballStats.Model;

namespace VolleyballStats.ViewModel
{
    public class LandingViewModel : ViewModelBase
    {
        private  INavigationService _navigationService;

        public LandingViewModel()//(INavigationService navigationService)
        {
            //this._navigationService = navigationService;
            this.NewGame = new RelayCommand(NewGameEvent);
        }

        public RelayCommand NewGame { get; set; }

        public void NewGameEvent()
        {
            SimpleIoc.Default.GetInstance<INavigationService>().Navigate(typeof(GameSetup));
        }

    }
}
