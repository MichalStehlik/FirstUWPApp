using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSelector.Models
{
    public class Teacher : Person
    {
        private string _shortname;
        public string Shortname { get { return _shortname; } set { _shortname = value; NotifyPropertyChanged(); } }
    }
}
