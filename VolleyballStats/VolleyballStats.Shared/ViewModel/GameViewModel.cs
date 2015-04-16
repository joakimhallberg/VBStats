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
using GalaSoft.MvvmLight.Messaging;

using VolleyballStats.Model;

namespace VolleyballStats.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        private  INavigationService _navigationService;

        public GameViewModel()//(INavigationService navigationService)
        {
            //this._navigationService = navigationService;
            this.GoToGame = new RelayCommand(GoToGameEvent);
            this.Game = new Game();
            RegisterMessenger();
        }

        public Game GetGame()
        {
            return this.Game;
        }

        private Game _game;

        public Game Game
        {
            get { return _game; }
            set { Set(ref _game, value); }
        }

        private LinksInitMessage _linksInitItem;
        public RelayCommand GoToGame { get; set; }

        public void GoToGameEvent()
        {
            Messenger.Default.Send<LinksInitMessage>(new LinksInitMessage(Game));

            // Store article incase Links requests it
            this._linksInitItem = new LinksInitMessage(Game);

            //(new ViewModelLocator()).VolleyballGame.Game = this.Game;
            //SimpleIoc.Current.GetInstance<VolleyballGameViewModel>().Game = this.Game;
            SimpleIoc.Default.GetInstance<INavigationService>().Navigate(typeof(GameView));
        }

        private void RegisterMessenger()
        {
            //Register any messages
            Messenger.Default.Register<LinksInitMessage>(this, msg =>
            {
                if (this._linksInitItem != null)
                {
                    msg.Callback(this.Game);
                    this._linksInitItem = null;
                }
            });
        }
    }
}
