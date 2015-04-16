using System;
using System.Collections.Generic;
using System.Text;

namespace VolleyballStats.Model
{
    public class ServeGrade
    {
        public int Grade { get; set; }
        public string Name { get; set; }
        public string DisplayName
        {
            get
            {
                string value = Grade.ToString();
                if (!string.IsNullOrEmpty(Name))
                {
                    value = value + "-" + Name;
                }
                return value;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is ServeGrade)
                return (Grade == ((ServeGrade)obj).Grade);
            return false;
        }
    }
}
