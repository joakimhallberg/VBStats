using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class LineUpHelper : ObservableObject
    {
        public LineUpHelper(GameConfiguration config)
        {
            this._allPlayers = new ObservableCollection<Player>();
            this.OnCourtPlayers = new ItemObservableCollection<Player>();
            this.OnCourtServers = new ItemObservableCollection<Player>();
            this.Players = new Player[6];
            this.Available = new ItemObservableCollection<Player>();
            Init(config);
        }

        public void Init(GameConfiguration config)
        {
            this._config = config;
            this._allPlayers = config.Players;
            foreach (var item in this._config.Players)
            {
                if (!OnCourtPlayers.Contains(item))
                {
                    this.Available.Add(item);
                }
            }            
        }

        private GameConfiguration _config;
        private ObservableCollection<Player> _allPlayers;
        private ItemObservableCollection<Player> _onCourtPlayers;
        private ItemObservableCollection<Player> _onCourtServers;
        private ItemObservableCollection<Player> _available;

        private Player[] Players { get; set;}

        public ItemObservableCollection<Player> Available
        {
            get { return this._available; }
            set
            {
                this.Set(ref _available, value);
            }
        }

        public ItemObservableCollection<Player> OnCourtPlayers
        {
            get { return this._onCourtPlayers; }
            set
            {
                this.Set(ref _onCourtPlayers, value);
            }
        }

        public ItemObservableCollection<Player> OnCourtServers
        {
            get { return this._onCourtServers; }
            set
            {
                this.Set(ref _onCourtServers, value);
            }
        }

        public void SubPlayer(Player player, int position)
        {
            if (position > 0 && position < 7)
            { 
                if (Available.Contains(player))
                {
                    Available.Remove(player);
                }
                bool found = true;
                for (int i = 0; i < 6; i++)
                {
                    if (player.Equals(Players[i]))
                    {
                        Players[i] = null;
                        break;
                    }
                }
                if (Players[position -1] != null)
                {
                    Available.Add(Players[position-1]);
                }
                Players[position-1] = player;
                this.SynchPlayers();
            }            
        }

        public void Rotate()
        {
            var player = this.Players[0];
            for (int i = 0; i < 5; i++)
            {
                Players[i] = Players[i + 1];
            }
            this.Players[5] = player;
            this.SynchPlayers();
        }

        private void SynchPlayers()
        {
            this.OnCourtServers.Clear();
            this.OnCourtServers.Add(_config.Opponent);
            this.OnCourtPlayers.Clear();
            for (int i = 0; i < 6; i++)
            {
                if (Players[i] != null)
                {
                    this.OnCourtPlayers.Add(Players[i]);
                    this.OnCourtServers.Add(Players[i]);
                }
            }            
        }

        public ItemObservableCollection<Player> CloneOnCourtPlayers()
        {
            var list = new ItemObservableCollection<Player>();
            foreach (var item in this._onCourtPlayers)
            {
                list.Add(item.Clone());
            }
            return list;
        }
    }
}
