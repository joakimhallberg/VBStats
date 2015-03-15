using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class Player :ObservableObject
    {
        public int? Number { get; set; }
        public string Name { get; set; }
        public string DisplayName
        {
            get
            {
                string value = Name;
                if (Number.HasValue && Number.Value >= 0)
                {
                    value = Number.Value.ToString() + " " + value;
                }
                return value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Player)
                return (Name == ((Player)obj).Name);
            else
                return false;
        }

        private PlayerStatistics _stats;
        public PlayerStatistics Stats
        {
            get { return this._stats; }
            set { this.Set(ref _stats, value); }
        }
    }
}
