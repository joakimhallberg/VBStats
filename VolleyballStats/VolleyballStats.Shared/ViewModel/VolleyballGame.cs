using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using VolleyballStats.Model;

namespace VolleyballStats
{
    public class VolleyballGame : ViewModelBase //ObservableObject
    {
        public VolleyballGame()
        {
            this.Config = new GameConfiguration();
            this.NextPoint = new RelayCommand(NextPointChangeEvent, CanMoveNextPoint);

            this.PrevPoint = new RelayCommand(PrevPointChangeEvent, CanMovePrevPoint);
            this.NewSet = new RelayCommand(NewSetEvent);
            this.NewGame = new RelayCommand(NewGameEvent);
            this.ExportGame = new RelayCommand(ExportGameEvent);
        }

        public RelayCommand NextPoint { get; private set; }
        //public DelegateCommand NextPoint { get; set; }
        public RelayCommand PrevPoint { get; set; }
        public RelayCommand NewSet { get; set; }
        public RelayCommand NewGame { get; set; }
        public RelayCommand ExportGame { get; set; }

        public GameConfiguration Config { get; set; }

        private bool CanMoveNextPoint()
        {
            //return true;
            if (CurrentPoint.Won.HasValue && CurrentPoint.Server != null)
            {
                return true;
            }
            return false;
        }

        private bool CanMovePrevPoint()
        {
            //return true;
            if (this.CurrentSet.CanMovePrev())
            {
                return true;
            }
            return false;
        }

        private Game _game;

        private Set _currentSet;
        private Point _currentPoint;

        public Game Game
        {
            get { return _game; }
            set { Set(ref _game, value); }
        }

        public void ExportGameEvent()
        { 
            string csv = "";
            for (int i = 0; i < this.Game.Sets.Count; i++)
            {
                csv += this.Game.Sets[i].Export(i + 1);
            }
            csv += "" + Environment.NewLine;
            CopyTextToClipboard(csv);
        }

        public void CopyTextToClipboard(string textToCopy)
        {
            var dataPackage = new DataPackage();

            dataPackage.SetText(textToCopy);
            Clipboard.SetContent(dataPackage);
        }


        public void NewGameEvent()
        {
            //this.Game = new Game();
            InitGame();
        }

        public void NextPointChangeEvent()
        {
            MoveNext();
        }

        public void PrevPointChangeEvent()
        {
            //this.CurrentPoint = StartNewPoint();
            MovePrev();
        }

        public void NewSetEvent()
        {
            //this.CurrentPoint = StartNewPoint();
            this.CurrentSet.CalcScore();
            this.StartNewSet();
        }

        public void InitGame()
        {
            this.Game = new Game();
            Game.Config.Init();
            StartNewSet();
            //this.CurrentPoint = StartNewPoint(true, _opponent);
        }

        public Set StartNewSet()
        {
            CurrentSet = new Set(this.Game) { Number = this.Game.Sets.Count + 1 };
            this.Game.Sets.Add(CurrentSet);
            this.CurrentPoint = CurrentSet.StartNewSet(_game.Opponent);
            return CurrentSet;
        }

        public void MoveNext()
        {
            this.CurrentPoint = CurrentSet.MoveNext(this.CurrentPoint, Game.Opponent);
        }

        public void MovePrev()
        {
            CurrentPoint = CurrentSet.MovePrev();
        }

        public Point CurrentPoint 
        {
            get { return _currentPoint; }
            set {
                Set(ref _currentPoint, value);
                _currentPoint.PropertyChanged += _currentPoint_PropertyChanged;
            } 
        }

        private void _currentPoint_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Set(ref _currentPoint, _currentPoint);
            NextPoint.RaiseCanExecuteChanged();
            PrevPoint.RaiseCanExecuteChanged();
        }

        public Set CurrentSet
        {
            get { return _currentSet; }
            set { Set(ref _currentSet, value); }
        }
    }
}
