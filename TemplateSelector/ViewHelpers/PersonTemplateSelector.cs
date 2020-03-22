using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TemplateSelector.ViewHelpers
{
    class PersonTemplateSelector : DataTemplateSelector
    {
        protected override DataTemplate SelectTemplateCore(object item, DependencyObject dependency)
        {
            if (item is Models.Teacher)
                return TeacherTemplate;
            if (item is Models.Student)
                return StudentTemplate;
            return PersonTemplate;
        }

        public DataTemplate TeacherTemplate { get; set; }
        public DataTemplate StudentTemplate { get; set; }
        public DataTemplate PersonTemplate { get; set; }
    }
}
