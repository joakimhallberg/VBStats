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

        void PointsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SumSets(this._sets);
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

        public override void SumSets(ItemObservableCollection<Set> sets)
        {
            base.SumSets(sets);

            //this.ReasonLoose = 0;
            //this.ReasonWin = 0;
            //this.ServeCount = 0;
            //this.ServeGrade = 0;
            //this.ServeQuality = 0;
            //this.ServeReturned = 0;
            //this.ServeSuccess = 0;
            //this.ServeWon = 0;
            //foreach (var reason in Reasons)
            //{
            //    reason.Count = 0;
            //}
            //foreach (var set in this._sets)
            //{
            //    var stats = set.Us;
            //    if (!this.Us)
            //    {
            //        stats = set.Them;
            //    }
            //    this.ReasonLoose += stats.ReasonLoose;
            //    this.ReasonWin += stats.ReasonWin;
            //    this.ServeCount += stats.ServeCount;
            //    this.ServeGrade += stats.ServeGrade;
            //    this.ServeQuality += stats.ServeQuality;
            //    this.ServeReturned += stats.ServeReturned;
            //    this.ServeSuccess += stats.ServeSuccess;
            //    this.ServeWon += stats.ServeWon;
            //    foreach (var reason in stats.Reasons)
            //    {
            //        foreach (var item in this.Reasons)
            //        {
            //            if (item.Equals(reason))
            //            {
            //                item.Count += reason.Count;
            //                break;
            //            }
            //        }
            //    }
            //}
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
