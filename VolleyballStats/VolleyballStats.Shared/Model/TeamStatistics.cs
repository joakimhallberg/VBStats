using System;
using System.Collections.Generic;
using System.Text;

using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class TeamStatistics : Statistics
    {
        public TeamStatistics(bool us) : base()
        {
            this.Us = us;
        }

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
            return (point.Serving.HasValue && point.Serving.Value) ;
        }

        protected override bool IncludeCredit(Point point)
        {
            return base.IncludeCredit(point);
        }

        protected override bool IncludeReason(Point point)
        {
            if (point.Reason != null)
            {
                return ((point.Won == point.Reason.Win && Us)
                    || (!point.Won != point.Reason.Win && !Us));
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
