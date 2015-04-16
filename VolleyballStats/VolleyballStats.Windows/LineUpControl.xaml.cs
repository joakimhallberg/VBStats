using System;
using System.Collections.Generic;
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

using VolleyballStats.ViewModel;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace VolleyballStats
{
    public sealed partial class LineUpControl : UserControl
    {
        private Popup popup;
        public VolleyballGameViewModel ViewModel {get; set;}
        
        public LineUpControl(Popup popup)
        {
            if (popup == null) throw new ArgumentNullException("popup");
            this.popup = popup;

            this.InitializeComponent();

            if (this.DataContext != null)
            {
                this.ViewModel = (VolleyballGameViewModel)this.DataContext;
            }
            //var bounds = Window.Current.Bounds;
            //this.RootPanel.Width = bounds.Width;
            //this.RootPanel.Height = bounds.Height;
        }

        public event EventHandler CloseRequested;

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.popup.IsOpen = false;
            ViewModel.LineUpChanged();
            //if (this.CloseRequested != null)
            //{
            //    // pass null arguments to indicate unsuccessful close
            //    this.CloseRequested(this, null);
            //}
        }

        private void players_DragItemsStarting(object sender, DragItemsStartingEventArgs e)
        {
            foreach (var item in e.Items)
            {
                // If you want to drop only a subset of the dragged items,
                // then make each key unique. 
                e.Data.Properties.Add("VolleyballStats.Player", item);
            }
        }

        private void lineUp_Drop(object sender, DragEventArgs e)
        {
            var dpView = e.Data.GetView();
            foreach (var prop in dpView.Properties)
            {
                var player = prop.Value as VolleyballStats.Model.Player;
                var i = ViewModel.Game.LineUp.OnCourtPlayers.Count;
                if (i == 6)
                {
                    i = 5;
                }
                //ViewModel.Game.LineUp.SubPlayer(player, i +1);
            }

        }

        private void player_Drop(object sender, DragEventArgs e)
        {
            var dpView = e.Data.GetView();
            var txt = sender as TextBlock;
            if (txt != null)
            {
                foreach (var prop in dpView.Properties)
                {

                    var player = prop.Value as VolleyballStats.Model.Player;
                    //lineUp.Items.Add(player);
                    txt.DataContext = player;
                    txt.Text = player.DisplayName;
                    ViewModel.Game.LineUp.SubPlayer(player, int.Parse((string)txt.Tag));

                    //var i = ViewModel.Game.LineUp.OnCourtPlayers.Count;
                    //if (i == 6)
                    //{
                    //    i = 5;
                    //}
                    //ViewModel.Game.LineUp.SubPlayer(player, i + 1);
                    //ViewModel.Game.OnCourtPlayers.Add(player);

                }
            }
        }

    }
}
