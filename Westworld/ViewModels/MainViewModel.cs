using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Westworld.Models;

namespace Westworld.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Article> _articles = new ObservableCollection<Article>();
        private int _selectedArticleIndex;
        private Article _selectedArticle;
        public MainViewModel ()
        {
            Articles = new ObservableCollection<Models.Article>();
            Artist hopkins = new Artist("Anthony", "Hopkins", Gender.Male, Profession.Actor, new Uri("https://www.imdb.com/name/nm0000164/"));
            Artist harris = new Artist("Ed", "Harris", Gender.Male, Profession.Actor);
            Artist newton = new Artist("Thandie", "Newton", Gender.Female, Profession.Actor);
            Artist wood = new Artist("Evan Rachel", "Wood", Gender.Female, Profession.Actor, new Uri("https://www.imdb.com/name/nm0939697/"));
            Artist simpson = new Artist("Jimmi", "Simpson", Gender.Male, Profession.Actor);
            Artist wright = new Artist("Jeffrey", "Wright", Gender.Male, Profession.Actor);
            Artist nolan = new Artist("Jonathan", "Nolan", Gender.Male, Profession.Director);
            Articles.Add(hopkins);
            Articles.Add(harris);
            Articles.Add(newton);
            Articles.Add(wood);
            Articles.Add(simpson);
            Articles.Add(wright);
            Articles.Add(nolan);
            Articles.Add(new Character("Dr. Robert", "Ford", Gender.Male, hopkins));
            Articles.Add(new Character("", "Muž v černém", Gender.Male, harris));
            Articles.Add(new Character("Dolores", "Abernathy", Gender.Female, wood, new Uri("https://www.imdb.com/character/ch0486391/")));
            Articles.Add(new Character("Maeve", "Millay", Gender.Female, newton));
            Articles.Add(new Character("Bernard", "Lowe", Gender.Male, wright));
            Articles.Add(new Character("William", "", Gender.Male, simpson));
            Articles.Add(new Episode("The Original", 1, 1, new Uri("https://www.imdb.com/title/tt4227538/")));
            Articles.Add(new Episode("Chestnut", 1, 2, new Uri("https://www.imdb.com/title/tt4562758/")));
            Articles.Add(new Episode("The Stray", 1, 3));
            Articles.Add(new Episode("Dissonance Theory", 1, 4));
            Articles.Add(new Episode("Contrapasso", 1, 5));
            Articles.Add(new Episode("The Adversary", 1, 6));
            Articles.Add(new Episode("Trompe L'Oeil", 1, 7));
            Articles.Add(new Episode("Trace Decay", 1, 8));
            Articles.Add(new Episode("The Well-Tempered Clavier", 1, 9));
            Articles.Add(new Episode("The Bicameral Mind", 1, 10));
        }

        public Article SelectedArticle {
            get { return _selectedArticle; }
            set { _selectedArticle = value; NotifyPropertyChanged(); } 
        }

        public int SelectedArticleIndex
        {
            get { return _selectedArticleIndex; }
            set { _selectedArticleIndex = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<Article> Articles { get; set; }

        

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
