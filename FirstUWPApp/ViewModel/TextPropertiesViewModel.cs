using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FirstUWPApp.ViewModel
{
    class TextPropertiesViewModel : INotifyPropertyChanged
    {
        private string _text;
        private double _size;

        public TextPropertiesViewModel()
        {
            Text = "Ahoj světe";
            Size = 32;
        }

        public string Text { get { return _text; } set { _text = value; NotifyPropertyChanged(); } }
        public double Size { get { return _size; } set { _size = value; NotifyPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
