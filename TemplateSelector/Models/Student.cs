using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSelector.Models
{
    public class Student : Person
    {
        private string _classname;
        public string Classname { get { return _classname; } set { _classname = value; NotifyPropertyChanged(); } }
    }
}
