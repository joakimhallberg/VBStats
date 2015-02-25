using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace VolleyballStats
{
    public class BoolToOppositeBoolConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");
            else if (value is Nullable<bool>)
            {
                if (value == null)
                    return null;
                return !(bool)value;
            }
            else
            {
                if (value == null)
                    return false;
                return !(bool)value;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");
            else if (value is Nullable<bool>)
            {
                if (value == null)
                    return null;
                return !(bool)value;                
            }
            else
            {
                if (value == null)
                    return false;
                return !(bool)value;
            }

            return null;
        }

        #endregion
    } 
}
