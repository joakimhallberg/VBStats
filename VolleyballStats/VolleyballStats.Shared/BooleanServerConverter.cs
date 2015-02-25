using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;


namespace VolleyballStats
{
    public class BooleanIntServerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int && parameter is Player)
            {
                if ((int)value == -1)
                return true; 
            }
            return false;
            //return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

    public class BooleanServerConverter : DependencyObject, IValueConverter
    {

        public VolleyballGame CurrentViewModel
        {
            get { return (VolleyballGame)GetValue(CurrentViewModelProperty); }
            set { SetValue(CurrentViewModelProperty, value); }
        }

        public static readonly DependencyProperty CurrentViewModelProperty =
            DependencyProperty.Register("CurrentViewModel",
                                        typeof(VolleyballGame),
                                        typeof(BooleanServerConverter),
                                        new PropertyMetadata(null));


        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (CurrentViewModel != null && value is int && CurrentViewModel.CurrentPoint.Server == value)
            {
                return true; 
            }
            return false;
            //return (value is bool && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is bool)
            {
                if ((bool)value)
                {
                    return parameter;
                }
                else
                {
                    return null;
                }
            }
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }

    }
}
