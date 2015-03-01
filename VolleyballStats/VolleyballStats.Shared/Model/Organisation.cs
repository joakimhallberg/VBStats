using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class Organisation : ObservableObject
    {
        private int? _id;
        private string _name;
        private ItemObservableCollection<Team> _teams;

        public int? Id
        {
            get { return this._id; }
            set { this.Set(ref _id, value); }
        }

        public string Name
        {
            get { return this._name; }
            set { this.Set(ref _name, value); }
        }

        public ItemObservableCollection<Team> Teams
        {
            get { return this._teams; }
            set { this.Set(ref _teams, value); }
        }

    }
}
