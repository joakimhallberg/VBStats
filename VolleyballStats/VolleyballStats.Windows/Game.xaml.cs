using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VolleyballStats
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        //public class VolleyBallGame
        //{
        //    public VolleyBallGame()
        //    {
        //        this.Players = new List<Player>();
        //        this.Servers = new List<Player>();
        //        this.ServeGrades = new List<ServeGrade>();
        //        this.PlayerFaults = new List<PlayerFault>();
        //        this.WinReasons = new List<WinReason>();
        //        this.LooseReasons = new List<LooseReason>();
        //    }
        //    public List<Player> Players {get; set;}
        //    public List<Player> Servers { get; set; }
        //    public List<ServeGrade> ServeGrades{ get; set; }
        //    public List<PlayerFault> PlayerFaults{ get; set; }
        //    public List<WinReason> WinReasons{ get; set; }
        //    public List<LooseReason> LooseReasons{ get; set; }
        //}

        public VolleyballGame ViewModel {get; set;}

        public Game()
        {
            this.InitializeComponent();
            if (this.DataContext != null)
            {
            this.ViewModel = (VolleyballGame)this.DataContext;

            //ViewModel = new VolleyballGame();
            ViewModel.Players.Add(new Player(){Number=2, Name="Elena"});
            ViewModel.Players.Add(new Player(){Number=13, Name="Sydney"});
            ViewModel.Players.Add(new Player(){Number=18, Name="Hailey"});
            ViewModel.Players.Add(new Player(){Number=22, Name="Keilani"});
            ViewModel.Players.Add(new Player(){Number=25, Name="Torrey"});
            ViewModel.Players.Add(new Player(){Number=35, Name="Miraya"});
            ViewModel.Players.Add(new Player(){Number=40, Name="Sarah"});
            ViewModel.Players.Add(new Player(){Number=89, Name="Maura"});
            ViewModel.Players.Add(new Player(){Number=91, Name="Sadie"});
            ViewModel.Players.Add(new Player(){Number=98, Name="Emma"});
            ViewModel.Servers.AddRange(ViewModel.Players);

            ViewModel.ServeGrades.Add(new ServeGrade(){Grade=0});
            ViewModel.ServeGrades.Add(new ServeGrade(){Grade=1});
            ViewModel.ServeGrades.Add(new ServeGrade(){Grade=2});
            ViewModel.ServeGrades.Add(new ServeGrade(){Grade=3});
            ViewModel.ServeGrades.Add(new ServeGrade(){Grade=4});
            ViewModel.ServeGrades.Add(new ServeGrade() { Grade = 5 });

            ViewModel.WinReasons.Add(new Reason(){ Name="Ace", Win=true, ServeReturned=false});
            ViewModel.WinReasons.Add(new Reason() { Name = "Spike", Win = true });
            ViewModel.WinReasons.Add(new Reason() { Name = "Tip", Win = true });
            ViewModel.WinReasons.Add(new Reason() { Name = "Dump", Win = true });
            ViewModel.WinReasons.Add(new Reason() { Name = "Block", Win = true });
            ViewModel.WinReasons.Add(new Reason() { Name = "Down Ball", Win = true });
            ViewModel.LooseReasons.Add(new Reason() { Name = "Receive Error", Win = false, ServeReturned = false });
            ViewModel.LooseReasons.Add(new Reason() { Name = "Attack Error", Win = false });
            ViewModel.LooseReasons.Add(new Reason() { Name = "Set Error", Win = false });
            ViewModel.LooseReasons.Add(new Reason() { Name = "Ball Handling", Win = false });
            ViewModel.LooseReasons.Add(new Reason() { Name = "Free ball", Win = false });
            ViewModel.LooseReasons.Add(new Reason() { Name = "Fault", Win = false });

            //ViewModel.LooseReasons.Add(new Reason() { Name = "Ace", Win = true, ServeReturned = false });
            //ViewModel.LooseReasons.Add(new Reason() { Name = "Spike", Win = true });
            //ViewModel.LooseReasons.Add(new Reason() { Name = "Tip", Win = true });
            //ViewModel.LooseReasons.Add(new Reason() { Name = "Dump", Win = true });
            //ViewModel.LooseReasons.Add(new Reason() { Name = "Block", Win = true });
            //ViewModel.LooseReasons.Add(new Reason() { Name = "Down Ball", Win = true });
            //ViewModel.WinReasons.Add(new Reason() { Name = "Receive Error", Win = false, ServeReturned = false });
            //ViewModel.WinReasons.Add(new Reason() { Name = "Attack Error", Win = false });
            //ViewModel.WinReasons.Add(new Reason() { Name = "Set Error", Win = false });
            //ViewModel.WinReasons.Add(new Reason() { Name = "Ball Handling", Win = false });
            //ViewModel.WinReasons.Add(new Reason() { Name = "Free ball", Win = false });
            //ViewModel.WinReasons.Add(new Reason() { Name = "Fault", Win = false });
            
            ViewModel.PlayerFaults.Add(new PlayerFault(){ Name="Net"});
            ViewModel.PlayerFaults.Add(new PlayerFault(){ Name="Ball"});
            ViewModel.PlayerFaults.Add(new PlayerFault(){ Name="Under"});
            ViewModel.PlayerFaults.Add(new PlayerFault(){ Name="Over Net"});
            ViewModel.PlayerFaults.Add(new PlayerFault(){ Name="Foot"});
            ViewModel.PlayerFaults.Add(new PlayerFault(){ Name="Rotation"});
            ViewModel.PlayerFaults.Add(new PlayerFault(){ Name="Other"});

            //ViewModel.Sets.Add(new Set() { Number= ViewModel.Sets.Count + 1 });

            ViewModel.InitGame();
            }

            //this.DataContext = ViewModel;
        }



        //public class Player
        //{
        //    public int? Number { get; set; }
        //    public string Name { get; set; }
        //    public string DisplayName 
        //    {
        //        get 
        //        {
        //            string value = Name;
        //            if (Number.HasValue)
        //            {
        //                value = Number.Value.ToString() + " " + value;
        //            }
        //            return value;
        //        }
        //    }
        //}

        //public class ServeGrade
        //{
        //    public int Grade { get; set; }
        //    public string Name { get; set; }
        //    public string DisplayName
        //    {
        //        get
        //        {
        //            string value = Grade.ToString();
        //            if (!string.IsNullOrEmpty(Name))
        //            {
        //                value = value + "-" + Name;
        //            }
        //            return value;
        //        }
        //    }
        //}

        //public class WinReason
        //{
        //    public int? Value { get; set; }
        //    public string Name { get; set; }
        //    public string DisplayName
        //    {
        //        get
        //        {
        //            string value = Name;
        //            if (Value.HasValue)
        //            {
        //                value = Value.Value.ToString() + " " + value;
        //            }
        //            return value;
        //        }
        //    }
        //}

        //public class LooseReason
        //{ 
        //    public int? Value { get; set; }
        //    public string Name { get; set; }
        //    public string DisplayName
        //    {
        //        get
        //        {
        //            string value = Name;
        //            if (Value.HasValue)
        //            {
        //                value = Value.Value.ToString() + " " + value;
        //            }
        //            return value;
        //        }
        //    }
        //}

        //public class PlayerFault
        //{
        //    public int? Value { get; set; }
        //    public string Name { get; set; }
        //    public string DisplayName
        //    {
        //        get
        //        {
        //            string value = Name;
        //            if (Value.HasValue)
        //            {
        //                value = Value.Value.ToString() + " " + value;
        //            }
        //            return value;
        //        }
        //    }            
        //}

        private void Page_Load(object sender, RoutedEventArgs e)
        {
        }

        private void toggle_Checked(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            { 
            }
        }

        //private void server_Checked(object sender, RoutedEventArgs e)
        //{
        //    //int childAmount = VisualTreeHelper.GetChildrenCount((sender as ToggleButton).Parent);
        //    //ToggleButton tb;
        //    //for (int i = 0; i < childAmount; i++)
        //    //{
        //    //    tb = null;
        //    //    tb = VisualTreeHelper.GetChild((sender as ToggleButton).Parent, i) as ToggleButton;

        //    //    if (tb != null)
        //    //        tb.IsChecked = false;
        //    //}

        //    //(sender as ToggleButton).IsChecked = true;

        //    var button = (ToggleButton)sender;
        //    if (button.IsChecked != null)
        //    { // Not sure why checked can be null but that's fine, ignore it
        //        bool notChecked = (!(bool)button.IsChecked);
        //        if (notChecked)
        //        { // I guess this means the click would uncheck it
        //            button.IsChecked = true;
        //        }
        //    }


        //    //foreach (ListViewItem server in this.serverList.Items)
        //    //{
        //    //    if (server == e.OriginalSource)
        //    //    {
        //    //        return ;
        //    //    }
        //    //}
        //}

        private void next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void prev_Click(object sender, RoutedEventArgs e)
        {

        }

        private void set_Click(object sender, RoutedEventArgs e)
        {

        }

        private void loose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (loose.SelectedItem != null)
            {
                var selectItem = loose.SelectedItem;
                win.SelectedItem = null;
                loose.SelectedItem = selectItem;
            }
        }

        private void win_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (win.SelectedItem != null)
            {
                var selectItem = win.SelectedItem;
                loose.SelectedItem = null;
                win.SelectedItem = selectItem;
            }
        }
    }
}
