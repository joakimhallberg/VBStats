using System;
using System.Collections.Generic;
using System.Text;

namespace VolleyballStats.Model
{
    public class BindableReason
    {
        public BindableReason(string name, IList<Reason> reasons)
        {
            Name = name;
            Reasons = reasons;
        }

        public string  Name { get; set; }
        public IList<Reason> Reasons { get; set; }

        private int? GetReason(int column)
        {
            if (column <= Reasons.Count)
            {
                return Reasons[column].Count;
            }
            return null;
        }

        public string GetReasonName(int column)
        {
            if (column < Reasons.Count)
            {
                return Reasons[column].DisplayName;
            }
            return string.Empty;            
        }

        public int Reason0
        { 
             get{ return GetReason(0).Value; }
        }

        public int Reason1
        { 
             get{ return GetReason(1).Value; }
        }

        public int? Reason2
        { 
             get{ return GetReason(2); }
        }

        public int? Reason3
        { 
             get{ return GetReason(3); }
        }

        public int? Reason4
        { 
             get{ return GetReason(4); }
        }

        public int? Reason5
        { 
             get{ return GetReason(5); }
        }

        public int? Reason6
        { 
             get{ return GetReason(6); }
        }

        public int? Reason7
        { 
             get{ return GetReason(7); }
        }

        public int? Reason8
        { 
             get{ return GetReason(8); }
        }

        public int? Reason9
        { 
             get{ return GetReason(9); }
        }

        public int? Reason10
        { 
             get{ return GetReason(10); }
        }

        public int? Reason11
        { 
             get{  return GetReason(11); }
        }

        public int? Reason12
        { 
             get{ return GetReason(12); }
        }

        public int? Reason13
        { 
             get{ return GetReason(13); }
        }

        public int? Reason14
        { 
            get{ return GetReason(14); }
        }

        public int? Reason15
        { 
             get{ return GetReason(15); }
        }
    }
}
