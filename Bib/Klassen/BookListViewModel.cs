using Biblio_test;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bib.Klassen
{
    class BookListViewModel
    {
        public ICommand AddBoookCommand => new Command(AddBook);

        public ICommand RemoveBookCommand => new Command(RemoveBook);

        public ObservableCollection<Buch> Books { get; set; }

        public Buch SelectedBook { get; set; }

        public BookListViewModel()
        {
            Books = new ObservableCollection<Buch>()
            {
                new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
                new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
                new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
                new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
                new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
                new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
                new Buch("Python Programmierung",2020504,"Hanser",1,3,"https://i.imgur.com/jqmxRKx.png","Brend Klein",2,"Fachbuch")
            };
        }

        public void AddBook(object obj)
        {
            Books.Add((Buch)obj);
        }

        public void RemoveBook()
        {

            Books.Remove(SelectedBook);
        }
    }
}
