using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    [System.Runtime.Serialization.DataContract]
    public class PlayerStatistics : Statistics
    {
        public PlayerStatistics(Player player, ItemObservableCollection<Reason> reasons)
            : base()
        {
            this.Player = player;
            this.Reasons = reasons;// new ItemObservableCollection<Reason>();
        }

        public PlayerStatistics(Player player, ItemObservableCollection<Reason> reasons, ItemObservableCollection<Set> sets)
            : this(player, reasons)
        {
            this._sets = sets;
            this._sets.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(PointsChanged);
        }

        void PointsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SumSets(this._sets);
        }


        [System.Runtime.Serialization.DataMember]
        private Player Player { get; set; }
        private int _pointsPlayed;
        private int _pointWon;
        private int _pointLost;

        public int PointsWon
        {
            get { return this._pointWon; }
            set {  Set(ref _pointWon, value); }
        }

        public int PointsLost
        {
            get { return this._pointLost; }
            set { Set( ref this._pointLost, value); }
        }

        public int PointsPlayed
        {
            get { return this._pointsPlayed; }
            set { Set( ref this._pointsPlayed, value); }
        }

        public string Score
        {
            get { return this.PointsWon.ToString() + " - " + this.PointsLost.ToString(); } 
        }

        public string PlayerName
        {
            get { return this.Player.DisplayName; }
        }

        private ItemObservableCollection<Set> _sets { get; set; }
        //[System.Runtime.Serialization.DataMember]
        //private ItemObservableCollection<Reason> _reasons;
        
        protected override bool IncludeServe(Point point)
        {
            if (this.Player == null || point.Server == null)
            {
                return false;
            }
            return point.Server.Equals(this.Player);
        }

        protected override bool IncludeReason(Point point)
        {
            if (this.Player == null)
            {
                return false;
            }
            if (point.Credit == null)
                return false;
            return point.Credit.Equals(this.Player);
        }

        protected override void AdditionalFieldsToCalc(Statistics stats)
        {
            base.AdditionalFieldsToCalc(stats);
        }

        public override bool AddPoint(Point point)
        {
            if (point.Players != null && point.Players.Contains(this.Player))
            {
                this.PointsPlayed += 1;
                if (point.Won.HasValue)
                {
                    if (point.Won.Value)
                    {
                        this.PointsWon += 1;
                    }
                    else
                    {
                        this.PointsLost += 1;
                    }
                }
            }
 	        return base.AddPoint(point);
        }

        public override void ResetStats()
        {
 	        base.ResetStats();
            this.PointsPlayed = 0;
            this.PointsWon = 0;
            this.PointsLost = 0;
        }

        protected override Statistics SetStatistics(Set set, Player player)
        {
            if (player != null)
            {
                return player.Stats;
            }
            return base.SetStatistics(set, player);
        }

        public override void SumSets(ItemObservableCollection<Set> sets)
        {
            this.Clear();
            foreach (var set in sets)
            {
                foreach (var item in set.PlayerStats)
                {
                    if (item.Stats.Player.Equals(this.Player))
                    {
                        this.Add(item.Stats);
                    }
                }
            }
            //base.SumSets(sets);
        }

        protected override void Add(Statistics stats)
        {
            base.Add(stats);
            var p = stats as PlayerStatistics;
            if (p != null)
            {
                this.PointsLost = p.PointsLost;
                this.PointsPlayed = p.PointsPlayed;
                this.PointsWon = p.PointsWon;
            }
        }
    }
}
