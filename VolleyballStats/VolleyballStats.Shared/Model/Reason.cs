using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class Reason : ObservableObject
    {
        public int? Value { get; set; }
        public string Name { get; set; }
        public bool? Win { get; set; }
        public bool? ServeReturned { get; set; }
        public bool? ReceiveError { get; set; }
        //public bool ServeError {  get; set;}
        private int _count;
        public ServeGrade Grade { set; get; }

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

        public Reason Clone()
        {
            var item = new Reason();
            item.Count = 0;
            item.Name = this.Name;
            item.ServeReturned = this.ServeReturned;
            item.Value = this.Value;
            item.Win = this.Win;
            return item;
        }

        public override bool Equals(object obj)
        {
            if (obj is Reason)
                return (Name == ((Reason)obj).Name);
            return false;
        }

        public static ObservableCollection<Reason> DefaultWinReasons()
        {
            var list = new ObservableCollection<Reason>();

            list.Add(new Reason() { Name = "Ace", Win = true, ServeReturned = false });
            list.Add(new Reason() { Name = "Spike", Win = true });
            list.Add(new Reason() { Name = "Tip", Win = true });
            list.Add(new Reason() { Name = "Dump", Win = true });
            list.Add(new Reason() { Name = "Block", Win = true });
            list.Add(new Reason() { Name = "Down Ball", Win = true });
            return list;
        }


        public static ObservableCollection<Reason> DefaultLooseReasons()
        {
            var list = new ObservableCollection<Reason>();
            list.Add(new Reason() { Name = "Serve Error", Win = false, ServeReturned = false, Grade = new ServeGrade() { Grade= 0} });
            list.Add(new Reason() { Name = "Receive Error", Win = false, ServeReturned = false, ReceiveError=true });
            list.Add(new Reason() { Name = "Attack Error", Win = false });
            list.Add(new Reason() { Name = "Set Error", Win = false });
            list.Add(new Reason() { Name = "Ball Handling", Win = false });
            list.Add(new Reason() { Name = "Free ball", Win = false });
            list.Add(new Reason() { Name = "Fault", Win = false });
            return list;
        }
    }
}
