using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace Converters.ViewModels.Converters
{
    class IntToPortrait : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int)
            {
                int cislo = ((int)value) % 7;
                switch (cislo)
                {
                    case 0: { return Portrait1; }
                    case 1: { return Portrait2; }
                    case 2: { return Portrait3; }
                    case 3: { return Portrait4; }
                    case 4: { return Portrait5; }
                    case 5: { return Portrait6; }
                    case 6: { return Portrait7; }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        public BitmapImage Portrait1 { get; set; }
        public BitmapImage Portrait2 { get; set; }
        public BitmapImage Portrait3 { get; set; }
        public BitmapImage Portrait4 { get; set; }
        public BitmapImage Portrait5 { get; set; }
        public BitmapImage Portrait6 { get; set; }
        public BitmapImage Portrait7 { get; set; }
    }
}
