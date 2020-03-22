using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TemplateSelector.Models;

namespace TemplateSelector.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();
        private int _selectedPersonIndex;
        private Person _selectedPerson;

        public MainViewModel()
        {
            Persons = new ObservableCollection<Person> 
            { 
                new Teacher {Firstname = "Michal", Lastname="Stehlík", Gender=Gender.Male, Shortname="St", Email="michal.stehlik@pslib.cz"},
                new Student {Firstname = "Adam", Lastname="Antoš", Gender=Gender.Male, Classname="P1", Email="adaanto020@pslib.cz"},
                new Student {Firstname = "Therese", Lastname="Ironside", Gender=Gender.Female, Classname="P1", Email="theiron020@pslib.cz"},
                new Person {Firstname = "Břetislav", Lastname="Borovička", Gender=Gender.Male, Email="bretislaf@borovickofi.name"}
            };
        }

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { _selectedPerson = value; NotifyPropertyChanged(); }
        }

        public int SelectedPersonIndex
        {
            get { return _selectedPersonIndex; }
            set { _selectedPersonIndex = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<Person> Persons 
        {
            get { return _persons; }
            set { _persons = value; NotifyPropertyChanged(); }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
