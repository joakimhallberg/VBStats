using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class Game : ObservableObject
    {
        public Game()
        {
            this.Config = new GameConfiguration();
            this.Sets = new ItemObservableCollection<Set>();
        }

        private ItemObservableCollection<Set> _sets;
        private Player _opponent;
        public GameConfiguration Config { get; set; }

        public ItemObservableCollection<Set> Sets
        {
            get { return _sets; }
            set { this.Set(ref _sets, value, "Sets"); }
        }

        public Player Opponent
        {
            get 
            {
                if (this.Config != null)
                {
                    return this.Config.Opponent;
                }
                return null; 
            }
            //set { this.Set(ref _opponent, value); }
        }
    }
}
