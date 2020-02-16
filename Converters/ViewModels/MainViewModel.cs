using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Converters.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private int _nameIndex;

        public MainViewModel()
        {
            NameIndex = 3;
        }

        public int NameIndex 
        { 
            get
            {
                return _nameIndex;
            }
            set
            {
                _nameIndex = value;
                NotifyPropertyChanged();
            }
        }
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
