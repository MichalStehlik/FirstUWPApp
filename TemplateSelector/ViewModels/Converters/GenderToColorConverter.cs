using System;
using TemplateSelector.Models;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace TemplateSelector.ViewModels.Converters
{
    class GenderToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Gender)
            {
                Gender gen = (Gender)value;
                if (gen == Gender.Male)
                {
                    return new SolidColorBrush(Man);
                }
                return new SolidColorBrush(Woman);

            }
            else
                return default;
        }

        public Color Man { get; set; }
        public Color Woman { get; set; }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
