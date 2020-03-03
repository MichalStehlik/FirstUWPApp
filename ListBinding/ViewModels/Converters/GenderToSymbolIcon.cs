using ListBinding.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace ListBinding.ViewModels.Converters
{
    class GenderToSymbolIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Gender)
            {
                switch (value)
                {
                    case Gender.Female: { return Symbol.Contact2; }
                    case Gender.Male: { return Symbol.Contact; }
                    case Gender.Other: { return Symbol.OtherUser; }
                    default: { return Symbol.Help; }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
