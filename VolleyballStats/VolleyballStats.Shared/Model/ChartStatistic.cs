using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class ChartStatistic : ObservableObject
    {
        public ChartStatistic(TeamStatistics stats, string name, bool win)
        {
            _stats = stats;
            _name = name;
            _win = win;
            _stats.PropertyChanged += _statsPropertyChanged;
        }

        private void _statsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Value = point + 1;
        }

        private TeamStatistics _stats;
        private string _name;
        private bool _win;
        private int point;

        public string Name {
            get { return _name; }
            set { Set(ref _name, value); }
        }

        public int Value
        {
            get {
                if (_win)
                {
                    return _stats.ReasonWin;
                }
                else
                {
                    return _stats.ReasonLoose;
                }
            }
            set { Set(ref point, value); }
        }
    }
}
