using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Text;
using GalaSoft.MvvmLight;
using VolleyballStats;

namespace VolleyballStats.Model
{
    public class Set : ObservableObject
    {
        private int _number;
        private int _won;
        private int _lost;
        private string _temp;
        private int _currentIndex;
        private Game game;
        private TeamStatistics _them;
        private TeamStatistics _us;

        public Set(Game currentGame)
        {
            this.Points = new ItemObservableCollection<Point>();
            Points.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(PointsChanged); ;
            _currentIndex = 0;
            game = currentGame;
            Them = new TeamStatistics(false);
            Us = new TeamStatistics(true);
        }

        void PointsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CalcScore();
        }

        public Point StartNewSet(Player server)
        {
            _currentIndex = 0;
            var point = Add(false, server);
            return point;
        }

        public Point MoveNext(Point initPoint, Player opponent)
        {
            _currentIndex += 1;
            if (_currentIndex > this.Points.Count-1)
            {
                if (initPoint == null)
                {
                    return Add(false, opponent);
                }
                if (initPoint.Won.HasValue && initPoint.Won.Value)
                {
                    if (initPoint.Serving.HasValue && initPoint.Serving.Value)
                    {
                        return Add(true, initPoint.Server);                    
                    }
                    return Add(true, null);
                }
                else
                {
                    return Add(!initPoint.Serving, opponent);                    
                }
            }
            return this.Points[_currentIndex];
        }

        public Point MovePrev()
        {
            if (CanMovePrev())
            {
                _currentIndex -= 1;
                return this.Points[_currentIndex];
            }
            return null;
        }

        public bool CanMovePrev()
        {
            return (_currentIndex > 0);
        }

        private Point Add(bool? serving, Player server)
        {
            CalcScore();
            Score = "";
            var CurrentPoint = new Point() { Serving = serving, Server = server, ServeGrade= new ServeGrade(){Grade=3}, Returned=true };
            this.Points.Add(CurrentPoint);
            return CurrentPoint;            
        }

        public TeamStatistics Them 
        {
            get { return _them; }
            set { Set(ref _them, value); }
        }

        public TeamStatistics Us
        {
            get { return _us; }
            set { Set(ref _us, value); }
        }

        public int Number
        {
            get { return _number; }
            set { Set(ref _number, value); }
        }

        public int PointsWon
        {
            get { return _won; }
            set { Set(ref _won, value); }
        }
        public int PointsLost
        {
            get { return _lost; }
            set { Set(ref _lost, value); }
        }

        public void CalcScore()
        {
            this.Them = new TeamStatistics(false);
            this.Us = new TeamStatistics(true);
            foreach (var player in this.game.Config.Players)
            {
                player.Stats = (new PlayerStatistics(player));
            }
            foreach (var point in this.Points)
            {
                Them.AddPoint(point);
                Us.AddPoint(point);
                foreach (var player in this.game.Config.Players)
                {
                    player.Stats.AddPoint(point);
                }
            }
        }

        public string Score
        {
            get { return PointsWon + " - " + PointsLost;}
            set { Set(ref _temp, Score); }
        }

        public string Export(int set)
        {
            string csv = "";
            foreach (var point in this.Points)
            {
                csv += point.ExportCSV(set) + Environment.NewLine;
            }
            csv += "" + Environment.NewLine;
            return csv;
        }

        public ObservableCollection<Point> Points { get; set; } 
    }
}
