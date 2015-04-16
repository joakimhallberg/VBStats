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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace VolleyballStats
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatsView : Page
    {
        public VolleyballGameViewModel ViewModel { get; set; }

        public StatsView()
        {
            this.InitializeComponent();
            if (this.DataContext != null)
            {
                this.ViewModel = (VolleyballGameViewModel)this.DataContext;
            }

            this.ViewModel.Game.PrepPlayerStats();

            reasonStats.HorizontalAlignment = HorizontalAlignment.Left;
            reasonStats.VerticalAlignment = VerticalAlignment.Top;


            var column = new ColumnDefinition();
            column.Width = new GridLength(1, GridUnitType.Star);
            this.reasonStats.ColumnDefinitions.Add(column);
            column = new ColumnDefinition();
            column.Width = new GridLength(1, GridUnitType.Star);
            this.reasonStats.ColumnDefinitions.Add(column);
            this.reasonStats.RowDefinitions.Add(NewRowDefinition());  
            this.reasonStats.RowDefinitions.Add(NewRowDefinition());
            GetTextBlock("Player", 0, 1, true, null);
            int ColumnCount = 1;
            foreach ( var reason in ViewModel.Game.PlayerReasonStatBindList[0].Reasons)
            {
                column = new ColumnDefinition();
                column.Width =  new GridLength(1, GridUnitType.Star);
                this.reasonStats.ColumnDefinitions.Add(column);
                GetTextBlock(reason.Name, ColumnCount, 1, true, null);

                ColumnCount += 1;
            }

            //int ColumnCount = 1;
            int RowCount = 2;
            foreach (var player in ViewModel.Game.PlayerReasonStatBindList)
            {
                this.reasonStats.RowDefinitions.Add(NewRowDefinition());

                ColumnCount = 1;
                GetTextBlock(player.Name, 0, RowCount, true, null);
                foreach (var stat in player.Reasons)
                {
                    GetTextBlock(stat.Count.ToString(), ColumnCount, RowCount, true, stat.Win);
                    ColumnCount += 1;
                }
                RowCount += 1;
            }
        }

        private TextBlock GetTextBlock(string text, int column, int row, bool header, bool? win)
        {
            TextBlock txt4 = new TextBlock();
            Grid.SetColumn(txt4, column);
            Grid.SetRow(txt4, row);
            txt4.Text = text;
            txt4.FontSize = 18;
            if (header)
            {
                txt4.FontWeight = Windows.UI.Text.FontWeights.Bold;
            }
            if (win.HasValue)
            {
                if (win.Value)
                {
                    //txt4.f
                }
            }
            if (column > 0)
            {
                txt4.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
            }
            reasonStats.Children.Add(txt4);
            return txt4;
        }

        private RowDefinition NewRowDefinition()
        {
            var row = new RowDefinition();
            row.Height = new GridLength(1, GridUnitType.Star);
            return row;
        }


        private MyToolkit.Controls.DataGridColumnBase AddColumn(string name, string bindingPath)
        {
            var col = new MyToolkit.Controls.DataGridTextColumn();
            col.Header = name;
            col.CanSort = true;
            col.Binding = new Binding();
            col.Binding.Mode = BindingMode.OneWay;
            col.Binding.Path = new PropertyPath(bindingPath);
            return col;
        }

        //private ColumnDefinition CreateColumn(string value)
        //{
        //    var col = new ColumnDefinition();
        //    col.
        //}
    }
}
