using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;
using System.ComponentModel;
using System.Text;
using GalaSoft.MvvmLight;

namespace VolleyballStats.Model
{
    public class GameConfiguration : ObservableObject
    {
        public ObservableCollection<Player> Players { get; set; }
        public ObservableCollection<Player> Servers { get; set; }
        public ObservableCollection<ServeGrade> ServeGrades { get; set; }
        public ObservableCollection<PlayerFault> PlayerFaults { get; set; }
        public ObservableCollection<Reason> WinReasons { get; set; }
        public ObservableCollection<Reason> LooseReasons { get; set; }
        public Player Opponent;

        public GameConfiguration()
        {
            this.Players = new ObservableCollection<Player>();
            this.Servers = new ObservableCollection<Player>();
            this.Opponent = new Player() { Name = "Them", Number = -1 };
            this.Servers.Add(this.Opponent);
            this.ServeGrades = new ObservableCollection<ServeGrade>();
            this.PlayerFaults = new ObservableCollection<PlayerFault>();
            this.WinReasons = new ObservableCollection<Reason>();
            this.LooseReasons = new ObservableCollection<Reason>();        
        }

        public void Init()
        {
            this.Players.Add(new Player() { Number = 2, Name = "Elena" });
            this.Players.Add(new Player() { Number = 13, Name = "Sydney" });
            this.Players.Add(new Player() { Number = 18, Name = "Hailey" });
            this.Players.Add(new Player() { Number = 22, Name = "Keilani" });
            this.Players.Add(new Player() { Number = 25, Name = "Torrey" });
            this.Players.Add(new Player() { Number = 35, Name = "Miraya" });
            this.Players.Add(new Player() { Number = 40, Name = "Sarah" });
            this.Players.Add(new Player() { Number = 89, Name = "Maura" });
            this.Players.Add(new Player() { Number = 91, Name = "Sadie" });
            this.Players.Add(new Player() { Number = 98, Name = "Emma" });
            foreach (var player in this.Players)
            {
                this.Servers.Add(player);
            }

            var missedServ = new ServeGrade() { Grade = 0 };
            this.ServeGrades.Add(missedServ);
            this.ServeGrades.Add(new ServeGrade() { Grade = 1 });
            this.ServeGrades.Add(new ServeGrade() { Grade = 2 });
            this.ServeGrades.Add(new ServeGrade() { Grade = 3 });
            this.ServeGrades.Add(new ServeGrade() { Grade = 4 });
            this.ServeGrades.Add(new ServeGrade() { Grade = 5 });

            this.WinReasons = Reason.DefaultWinReasons();
            this.LooseReasons = Reason.DefaultLooseReasons();
            
            this.PlayerFaults.Add(new PlayerFault() { Name = "Net" });
            this.PlayerFaults.Add(new PlayerFault() { Name = "Ball" });
            this.PlayerFaults.Add(new PlayerFault() { Name = "Under" });
            this.PlayerFaults.Add(new PlayerFault() { Name = "Over Net" });
            this.PlayerFaults.Add(new PlayerFault() { Name = "Foot" });
            this.PlayerFaults.Add(new PlayerFault() { Name = "Rotation" });
            this.PlayerFaults.Add(new PlayerFault() { Name = "Other" });
        }
    }
}
