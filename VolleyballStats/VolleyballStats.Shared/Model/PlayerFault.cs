using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class PlayerFault : ObservableObject
    {
        public int? Value { get; set; }
        public string Name { get; set; }
        private int _count;

        public int Count
        {
            get { return this._count; }
            set { Set(ref _count, value); }
        }

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
