using Biblio_test;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bib.Klassen
{
    class BookListViewModel
    {
        //public ICommand AddBoookCommand => new Command(AddBook);

        //public void AddBook(object obj)
        //{
        //    Books.Add((Buch)obj);
        //}

        public static void AddBook(Buch buch)
        {
            SqlConnection sqlConnection = DataBase.Connection();

            string query = String.Format(@"Insert Into Book(Title, Isbn, Verlag, Verfuegbar, Anzahl, B_foto, Autor, Exemplarnr, Kategorie) 
                                        Values('{0}', {1}, '{2}', {3}, {4}, '' ,'{5}', {6}, '{7}')
                                        Update NumberOverview
                                        Set
                                        NumberOverview.AnzahlBook += {4}", 
                                        buch.Titel, buch.Isbn, buch.Verlag, buch.Verfuegbar, buch.Anzahl, 
                                        buch.Autor, buch.Exemplarnr, buch.Kategorie);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.CommandTimeout = 60;

            sqlCommand.ExecuteNonQuery();
        }

        public static void UpdateBook(Buch buch, Buch selectedBook)
        {
            SqlConnection sqlConnection = DataBase.Connection();

            if(selectedBook.Anzahl != buch.Anzahl)
            {
                if(selectedBook.Anzahl > buch.Anzahl)
                {
                    selectedBook.Verfuegbar -= selectedBook.Anzahl - buch.Anzahl;
                    int fewerBooks = selectedBook.Anzahl - buch.Anzahl;

                    string query1 = String.Format(@"Update NumberOverview
                                                    Set
                                                    NumberOverview.AnzahlBook -= {0}", fewerBooks);

                    SqlCommand sqlCommand1 = new SqlCommand(query1, sqlConnection);
                    sqlCommand1.CommandTimeout = 60;
                    sqlCommand1.ExecuteNonQuery();
                }
                else if(selectedBook.Anzahl < buch.Anzahl)
                {
                    selectedBook.Verfuegbar += buch.Anzahl - selectedBook.Anzahl;
                    int moreBooks = buch.Anzahl - selectedBook.Anzahl;

                    string query2 = String.Format(@"Update NumberOverview
                                                    Set
                                                    NumberOverview.AnzahlBook += {0}", moreBooks);

                    SqlCommand sqlCommand2 = new SqlCommand(query2, sqlConnection);
                    sqlCommand2.CommandTimeout = 60;
                    sqlCommand2.ExecuteNonQuery();
                }
            }

            string query = String.Format(@"Update Book
                            Set
                            Title = '{0}', Isbn = {1}, Verlag = '{2}', Verfuegbar = {3}, Anzahl = {4}, 
                            Autor = '{5}', Exemplarnr = {6}, Kategorie = '{7}'
                            where Title= '{8}'",
                            buch.Titel, buch.Isbn, buch.Verlag, selectedBook.Verfuegbar, buch.Anzahl,
                            buch.Autor, buch.Exemplarnr, buch.Kategorie, selectedBook.Titel);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.CommandTimeout = 60;

            sqlCommand.ExecuteNonQuery();
        }

        public ICommand RemoveBookCommand => new Command(RemoveBook);

        public void RemoveBook()
        {
            if(SelectedBook != null) 
            {
                SqlConnection sqlConnection = DataBase.Connection();

                string query = string.Format(@"DELETE Book
                                             where Book.Title = '{0}'"
                                             ,SelectedBook.Titel);
                
                string query1 = string.Format(@"Update NumberOverview
                                                Set
                                                NumberOverview.AnzahlBook -= {0}"
                                                ,SelectedBook.Anzahl);

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlCommand sqlCommand1 = new SqlCommand(query1, sqlConnection);

                sqlCommand.CommandTimeout = 60;
                sqlCommand1.CommandTimeout = 60;

                try 
                { 
                    sqlCommand.ExecuteNonQuery();
                    Books.Remove(SelectedBook);
                    sqlCommand1.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Buch kann nicht " +
                        "gelöscht werden, befor alle Exemplare zurückgegeben werden", "Ok");
                }
            }
        }

        public ObservableCollection<Buch> Books { get; set; }

        public Buch SelectedBook { get; set; }

        public BookListViewModel()
        {
            SqlConnection sqlConnection = DataBase.Connection();

            string query = "select * from Book";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.CommandTimeout = 60;

            SqlDataReader dataOfQuery = sqlCommand.ExecuteReader();

            if(dataOfQuery.HasRows)
            {
                Books = new ObservableCollection<Buch>();
                while (dataOfQuery.Read())
                {
                    Books.Add(new Buch(dataOfQuery.GetString(1), dataOfQuery.GetString(2), dataOfQuery.GetString(3), dataOfQuery.GetInt32(4),
                        dataOfQuery.GetInt32(5), dataOfQuery.GetString(6), dataOfQuery.GetString(7), dataOfQuery.GetInt32(8), dataOfQuery.GetString(9)));
                }
            }

            //Books = new ObservableCollection<Buch>()
            //{
            //    new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
            //    new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
            //    new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
            //    new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
            //    new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
            //    new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),
            //    new Buch("Python Programmierung",2020504,"Hanser",1,3,"https://i.imgur.com/jqmxRKx.png","Brend Klein",2,"Fachbuch")
            //};
        }
    }
}
