using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Input;
using Windows.ApplicationModel.DataTransfer;
using GalaSoft.MvvmLight;

namespace VolleyballStats
{
    public class VolleyballGame : ViewModelBase //ObservableObject
    {
        public VolleyballGame()
        {
            this.Players = new List<Player>();
            this.Servers = new List<Player>();
            this.Opponent = new Player() { Name = "Them", Number=-1 };
            this.Servers.Add(this.Opponent);
            this.ServeGrades = new List<ServeGrade>();
            this.PlayerFaults = new List<PlayerFault>();
            this.WinReasons = new List<Reason>();
            this.LooseReasons = new List<Reason>();

            this.Sets = new ItemObservableCollection<Set>();
            this.NextPoint = new DelegateCommand(NextPointChangeEvent);
            this.PrevPoint = new DelegateCommand(PrevPointChangeEvent);
            this.NewSet = new DelegateCommand(NewSetEvent);
            this.NewGame = new DelegateCommand(NewGameEvent);
            this.ExportGame = new DelegateCommand(ExportGameEvent);
        }

        public List<Player> Players {get; set;}
        public List<Player> Servers { get; set; }
        public List<ServeGrade> ServeGrades{ get; set; }
        public List<PlayerFault> PlayerFaults{ get; set; }
        public List<Reason> WinReasons{ get; set; }
        public List<Reason> LooseReasons{ get; set; }

        private Set _currentSet;
        private Point _currentPoint;
        private Player _opponent;
        //private string 

        private ItemObservableCollection<Set> _sets;

        public ItemObservableCollection<Set> Sets
        {
            get { return _sets;}
            set { this.Set(ref _sets, value, "Sets"); }
        }

        public ICommand NextPoint { get; set; }
        public ICommand PrevPoint { get; set; }
        public ICommand NewSet { get; set; }
        public ICommand NewGame { get; set; }
        public ICommand ExportGame { get; set; }

        public void ExportGameEvent()
        { 
            string csv = "";
            for (int i = 0; i < this.Sets.Count; i++)
            {
                csv += this.Sets[i].Export(i+1);
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
            this.Sets = new ItemObservableCollection<Set>();
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
        
        
        private void OnSetsChanged(object sender, ItemPropertyChangedEventArgs<Set> e)
        {
            this.Set(ref _sets, Sets, "Sets");
        }

        private void OnSetsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void InitGame()
        {
            StartNewSet();
            //this.CurrentPoint = StartNewPoint(true, _opponent);
        }

        public Set StartNewSet()
        {
            CurrentSet = new Set() { Number = this.Sets.Count + 1 };
            this.Sets.Add(CurrentSet);
            this.CurrentPoint = CurrentSet.StartNewSet(_opponent);
            return CurrentSet;
        }

        //public Point StartNewPoint(bool? serving, Player player)
        //{
        //    return this.CurrentSet.Add(serving, player);
        //}

        public void MoveNext()
        {
            this.CurrentPoint = CurrentSet.MoveNext(this.CurrentPoint, _opponent);
        }

        public void MovePrev()
        {
            CurrentPoint = CurrentSet.MovePrev();
        }

        //public Point StartNewPoint()
        //{
        //    if (CurrentPoint != null )
        //    {
        //        if (CurrentPoint.Serving.HasValue)
        //        {
        //            if (CurrentPoint.Win != null)
        //            {
        //                return StartNewPoint(CurrentPoint.Serving, CurrentPoint.Server);
        //            }
        //            else
        //            {
        //                return StartNewPoint(!CurrentPoint.Serving.Value, _opponent);
        //                //return StartNewPoint(!CurrentPoint.Serving.Value, Opponent);
        //            }
        //        }
        //    }
        //    return StartNewPoint(false, _opponent); 
        //}

        public Player Opponent 
        {
            get { return _opponent; }
            set { Set(ref _opponent, value); } 
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
        }

        public Set CurrentSet
        {
            get { return _currentSet; }
            set { Set(ref _currentSet, value); }
        }
    }
        public class Player
        {
            public int? Number { get; set; }
            public string Name { get; set; }
            public string DisplayName 
            {
                get 
                {
                    string value = Name;
                    if (Number.HasValue && Number.Value>=0)
                    {
                        value = Number.Value.ToString() + " " + value;
                    }
                    return value;
                }
            }

            public override bool Equals(object obj)
            {
                return (Name == ((Player)obj).Name );
            }
        }

        public class ServeGrade
        {
            public int Grade { get; set; }
            public string Name { get; set; }
            public string DisplayName
            {
                get
                {
                    string value = Grade.ToString();
                    if (!string.IsNullOrEmpty(Name))
                    {
                        value = value + "-" + Name;
                    }
                    return value;
                }
            }

            public override bool Equals(object obj)
            {
                return (Grade == ((ServeGrade)obj).Grade);
            }
        }

        public class Reason
        {
            public int? Value { get; set; }
            public string Name { get; set; }
            public bool? Win { get; set; }
            public bool? ServeReturned { get; set; }

            public string DisplayName
            {
                get
                {
                    string value = Name;
                    if (Value.HasValue)
                    {
                        value = Value.Value.ToString() + " " + value;
                    }
                    return value;
                }
            }

            public override bool Equals(object obj)
            {
                return (Name == ((Reason)obj).Name);
            }
        }

        //public class LooseReason
        //{ 
        //    public int? Value { get; set; }
        //    public string Name { get; set; }
        //    public bool Us { get; set;}
        //    public string DisplayName
        //    {
        //        get
        //        {
        //            string value = Name;
        //            if (Value.HasValue)
        //            {
        //                value = Value.Value.ToString() + " " + value;
        //            }
        //            return value;
        //        }
        //    }

        //    public override bool Equals(object obj)
        //    {
        //        return (Name == ((LooseReason)obj).Name);
        //    }
        //}

        public class PlayerFault
        {
            public int? Value { get; set; }
            public string Name { get; set; }
            public string DisplayName
            {
                get
                {
                    string value = Name;
                    if (Value.HasValue)
                    {
                        value = Value.Value.ToString() + " " + value;
                    }
                    return value;
                }
            }

            public override bool Equals(object obj)
            {
                return (Name == ((PlayerFault)obj).Name);
            }     
        }
}
