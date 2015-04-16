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
    public sealed partial class ChartControl : UserControl
    {
        private Popup popup;
        public VolleyballGameViewModel ViewModel {get; set;}

        public ChartControl(Popup popup)
        {
            if (popup == null) throw new ArgumentNullException("popup");
            this.popup = popup;

            this.InitializeComponent();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.popup.IsOpen = false;
        }
    }
}
