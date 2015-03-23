﻿using System;
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
            this.Config.Init();
            Date = DateTime.Now.Date;
            this.Sets = new ItemObservableCollection<Set>();
            this.OnCourtPlayers = new ItemObservableCollection<Player>();
            Sets.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(PointsChanged);
            _playerStats = new ObservableCollection<Player>();
            _us = new TeamStatistics(true, Config.CloneReasons(), this.Sets);
            _them = new TeamStatistics(false, Config.CloneReasons(), this.Sets);
            this.LineUp = new LineUpHelper(this.Config);
        }


        private string _opposingTeam;
        private string _tournament;
        private DateTime? _date;
        private string _name;
        private LineUpHelper _lineUp;

        private ObservableCollection<Player> _playerStats;
        private ItemObservableCollection<Player> _onCourtPlayers;
        private TeamStatistics _us;
        private TeamStatistics _them;

        private ItemObservableCollection<Set> _sets;
        private Player _opponent;
        public GameConfiguration Config { get; set; }

        public void Clear()
        {
            _us.Clear();
            _them.Clear();
            foreach (var player in _playerStats)
            {
                player.Stats.Clear();
            }
            Date = DateTime.Now.Date;
            this.Sets.Clear();
        }

        public Set StartNewSet()
        {
            var CurrentSet = new Set(this) { Number = this.Sets.Count + 1 };
            this.Sets.Add(CurrentSet);
            //this.SetupStartingPlayers();
            //this.Servers.Clear();
            //this.Servers.Add(Game.Config.Opponent);
            //foreach (var player in Game.OnCourtPlayers)
            //{
            //    this.Servers.Add(player);
            //}
            return CurrentSet;
        }

        public void SetupStartingPlayers()
        {
            this.OnCourtPlayers.Clear();
            foreach (var player in this.Config.Players)
            {
                this.OnCourtPlayers.Add(player);
            }
        }

        private void PointsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RecalcStats();
        }

        public void RecalcStats()
        {
            Us.SumSets(this.Sets);
            Them.SumSets(Sets);        
        }

        public LineUpHelper LineUp
        {
            get { return _lineUp; }
            set { this.Set(ref _lineUp, value); }
        }

        public string Name
        {
            get { return _name; }
            set { this.Set(ref _name, value); }
        }

        public DateTime? Date
        {
            get { return _date; }
            set { this.Set(ref _date, value); }
        }

        public string Tournament
        {
            get { return _tournament; }
            set { this.Set(ref _tournament, value); }
        }

        public string OpposingTeam
        {
            get { return _opposingTeam; }
            set { this.Set(ref _opposingTeam, value); }
        }

        public ObservableCollection<Player> PlayerStats
        {
            get { return _playerStats; }
            set { Set(ref _playerStats, value); }
        }

        public ItemObservableCollection<Player> OnCourtPlayers
        {
            get { return _onCourtPlayers; }
            set { Set(ref _onCourtPlayers, value); }
        }

        public TeamStatistics Them
        {
            get { return _them; }
            set { Set(ref _them, value); }
        }

        public TeamStatistics Us
        {
            get { return _us; }
            set { Set(ref _us, value); }
        }

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
