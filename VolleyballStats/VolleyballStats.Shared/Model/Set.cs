using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats
{
    public class Set : ObservableObject
    {
        private int _number;
        private int _won;
        private int _lost;
        private string _temp;
        private int _currentIndex;

        public Set()
        {
            this.Points = new ItemObservableCollection<Point>();
            Points.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(PointsChanged); ;
            _currentIndex = 0;
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
                if (initPoint.Won)
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
            if (_currentIndex > 0)
            {
                _currentIndex -= 1;
                return this.Points[_currentIndex];
            }
            return null;
        }

        private Point Add(bool? serving, Player server)
        {
            var CurrentPoint = new Point() { Serving = serving, Server = server, ServeGrade= new ServeGrade(){Grade=3}, Returned=true };
            this.Points.Add(CurrentPoint);
            CalcScore();
            Score = "";
            return CurrentPoint;            
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
            PointsWon = 0;
            PointsLost = 0;
            foreach (var point in this.Points)
            {
                if (point.Won)
                {
                    PointsWon += 1;
                }
                else
                {
                    PointsLost += 1;
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
