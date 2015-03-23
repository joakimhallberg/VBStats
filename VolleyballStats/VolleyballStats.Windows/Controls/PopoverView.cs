using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;

namespace VolleyballStats.Controls
{
    public sealed class PopoverView : ContentControl
    {
        public PopoverView()
        {
            this.DefaultStyleKey = typeof(PopoverView);
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        /// <summary>
        /// Measure the size of this control: make it cover the full App window
        /// </summary>
        protected override Size MeasureOverride(Size availableSize)
        {
            Rect bounds = Window.Current.Bounds;
            availableSize = new Size(bounds.Width, bounds.Height);
            base.MeasureOverride(availableSize);
            return availableSize;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            InvalidateMeasure();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            Window.Current.SizeChanged -= OnSizeChanged;
        }
    }
}
