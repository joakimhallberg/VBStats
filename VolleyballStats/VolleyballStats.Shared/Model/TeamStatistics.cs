using System;
using System.Collections.Generic;
using System.Text;

using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class TeamStatistics : Statistics
    {
        public TeamStatistics(bool us, ItemObservableCollection<Reason> reasons) : base()
        {
            this.Us = us;
            this.Reasons = reasons;
        }

        public TeamStatistics(bool us, ItemObservableCollection<Reason> reasons, ItemObservableCollection<Set> sets)
            : this(us, reasons)
        {
            this._sets = sets;
            sets.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(PointsChanged);
        }

        bool calculating;
        void PointsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (!calculating)
            {
                calculating = true;
                SumSets(this._sets);
                calculating = false;
            }
        }

        protected override void AdditionalFieldsToCalc(Statistics stats)
        {
            base.AdditionalFieldsToCalc(stats);
        }

        protected override Statistics SetStatistics(Set set, Player player)
        {
            var stats = set.Us;
            if (!this.Us)
            {
                stats = set.Them;
            }
            return stats;
        }

        public override void ResetStats()
        {
            base.ResetStats();
            this.Won = 0;
        }

        private ItemObservableCollection<Set> _sets { get; set; }
        private int _won;
        public int Won
        {
            get { return this._won; }
            set { Set(ref _won, value); }
        }

        private bool _us;

        public bool Us
        {
            get { return this._us; }
            set { Set(ref _us, value); }
        }

        protected override bool IncludeServe(Point point)
        {
            return (point.Serving.HasValue && point.Serving.Value && point.Won.HasValue) ;
        }

        protected override bool IncludeCredit(Point point)
        {
            return base.IncludeCredit(point);
        }

        protected override bool IncludeReason(Point point)
        {
            if (point.Reason != null)
            {
                return (Us && (point.Won == point.Reason.Win)
                    || (point.Won != point.Reason.Win && !Us));
            }
            return false;
        }

        public override bool AddPoint(Point point)
        {
            if (point.Won.HasValue)
            {
                if (Us && point.Won.Value)
                {
                    Won += 1;
                }
                else if (!Us && !point.Won.Value)
                {
                    Won += 1;
                }
            }

            return base.AddPoint(point);
        }
    }
}
