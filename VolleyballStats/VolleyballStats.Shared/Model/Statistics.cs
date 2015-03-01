﻿using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public abstract class Statistics : ObservableObject
    {
        private int _serveCount;
        private int _serveGrade;
        private int _serveQuality;
        private int _serveSuccess;
        private int _serveReturned;
        private int _serveWon;
        private int _reasonWin;
        private int _reasonLoose;

        private ItemObservableCollection<Reason> _reasons;

        public Statistics()
        {
            this.Reasons = new ItemObservableCollection<Reason>();
        }

        public int ReasonWin
        {
            get { return _reasonWin; }
            set { Set(ref _reasonWin, value); }
        }

        public int ReasonLoose
        {
            get { return _reasonLoose; }
            set { Set(ref _reasonLoose, value); }
        }

        public ItemObservableCollection<Reason> Reasons
        {
            get { return _reasons; }
            set { Set(ref _reasons, value); }
        }

        public int ServeReturned
        {
            get { return _serveReturned; }
            set { Set(ref _serveReturned, value); }
        }

        public int ServeSuccess
        {
            get { return _serveSuccess; }
            set { Set(ref _serveSuccess, value); }
        }

        public int ServeWon
        {
            get { return _serveWon; }
            set { Set(ref _serveWon, value); }
        }

        public int ServeQuality
        {
            get { return _serveQuality; }
            set { Set(ref _serveQuality, value); }
        }

        public int ServeGrade
        {
            get { return _serveGrade; }
            set { Set(ref _serveGrade, value); }
        }

        public int ServeCount
        {
            get { return _serveCount; }
            set { Set(ref _serveCount, value); }
        }

        protected virtual bool IncludeServe(Point point)
        {
            return false;
        }

        protected virtual bool IncludeCredit(Point point)
        {
            return false;
        }

        protected virtual bool IncludeReason(Point point)
        {
            return false;
        }

        public virtual bool AddPoint(Point point)
        {
            if (IncludeServe(point))
            {
                ServeCount += 1;
                ServeGrade += point.ServeGrade.Grade;
                if (point.ServeGrade.Grade > 3)
                {
                    ServeQuality += 1;
                }
                if (point.ServeGrade.Grade == 0)
                {
                    //point.Reason = 
                }
                else
                {
                    ServeSuccess += 1;
                }
                if (point.Won.HasValue && point.Won.Value)
                {
                    ServeWon += 1;
                }
            }

            if (IncludeReason(point))
            {
                bool found = false;
                Reason reason = new Reason();
                foreach (var item in this.Reasons)
                {
                    if (item.Equals(point.Reason))
                    {
                        found = true;
                        item.Count += 1;
                        reason = item;
                    }
                }
                if (!found)
                {
                    reason = point.Reason.Clone();
                    reason.Count = 1;
                    this.Reasons.Add(reason);
                }
                if (reason.Win.HasValue && reason.Win.Value)
                {
                    ReasonWin += 1;
                }
                else
                {
                    ReasonLoose += 1;
                }
                return true;
            }
            return true;
            
        }
    }
}