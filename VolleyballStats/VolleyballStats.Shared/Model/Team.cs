using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Text;
using GalaSoft.MvvmLight;


namespace VolleyballStats
{
    public class Team : ObservableObject
    {
        private string _name;
        private Model.GameConfiguration _config;

        public string Name
        {
            get {return this._name;}
            set { Set(ref _name, value);}
        }

        public Model.GameConfiguration DefaultConfig
        {
            get { return this._config; }
            set { Set(ref _config, value); }
        }

    }
}
