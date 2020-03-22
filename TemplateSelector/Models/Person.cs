using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TemplateSelector.Models
{
    public class Person : INotifyPropertyChanged
    {
        protected string _firstname;
        protected string _lastname;
        protected Gender _gender;
        protected string _email;
        public string Firstname { get { return _firstname; } set { _firstname = value; NotifyPropertyChanged(); } }
        public string Lastname { get { return _lastname; } set { _lastname = value; NotifyPropertyChanged(); } }
        public string Email { get { return _email; } set { _email = value; NotifyPropertyChanged(); } }
        public Gender Gender { get { return _gender; } set { _gender = value; NotifyPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return Firstname + " " + Lastname;
        }

        protected void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
