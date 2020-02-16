using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Converters.ViewModels.Converters
{
    class IntToStringName : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is int)
            {
                int cislo = ((int)value) % 7;
                switch (cislo)
                {
                    case 0: { return "Alice"; }
                    case 1: { return "Betty"; }
                    case 2: { return "Caroline"; }
                    case 3: { return "Danielle"; }
                    case 4: { return "Emilia"; }
                    case 5: { return "Felicity"; }
                    case 6: { return "Gabriella"; }
                }
            }
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is string && targetType == typeof(string))
            {
                switch (value)
                {
                    case "Betty": { return 1; }
                    case "Caroline": { return 2; }
                    case "Danielle": { return 3; }
                    case "Emilia": { return 4; }
                    case "Felicity": { return 5; }
                    case "Gabriella": { return 6; }
                    default: return "Alice";
                }
            }
            return value.ToString();
        }
    }
}
