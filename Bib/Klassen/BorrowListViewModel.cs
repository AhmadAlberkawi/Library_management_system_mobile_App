using Biblio_test;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bib.Klassen
{
    class BorrowListViewModel
    {
        public ObservableCollection<Borrow> Borrows { get; set; }

        public Borrow SelectedBorrow { get; set; }
        
        public BorrowListViewModel()
        {
            SqlConnection sqlConnection = DataBase.Connection();

            string query = String.Format(@"select b.Title, b.Isbn, b.Verlag, b.Autor, s.Name from Borrow bo, Student s, Book b 
                                          where bo.BookId = b.BookId and bo.StudentId = s.StudentId");

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.CommandTimeout = 60;

            SqlDataReader sqlData = sqlCommand.ExecuteReader();

            if(sqlData.HasRows)
            {
                Borrows = new ObservableCollection<Borrow>();
                while (sqlData.Read()) 
                {
                    Borrows.Add(new Borrow("", sqlData.GetString(0), sqlData.GetString(1), sqlData.GetString(2), 
                        sqlData.GetString(3), sqlData.GetString(4)));
                }
            }
        }

        // the borrow Books from a student

        public BorrowListViewModel(string studentName) 
        {
            SqlConnection sqlConnection = DataBase.Connection();

            string query = String.Format(@"select b.Title, b.Isbn, b.Verlag, b.Autor, b.Exemplarnr, b.Kategorie from Borrow bo, Student s, Book b 
                                           where bo.BookId = b.BookId and bo.StudentId = s.StudentId 
                                           and s.Name = '{0}'", studentName);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.CommandTimeout = 60;

            SqlDataReader sqlData = sqlCommand.ExecuteReader();

            if (sqlData.HasRows)
            {
                Borrows = new ObservableCollection<Borrow>();
                while (sqlData.Read())
                {
                    Borrows.Add(new Borrow("", sqlData.GetString(0), sqlData.GetString(1), sqlData.GetString(2),
                        sqlData.GetString(3), studentName, sqlData.GetInt32(4), sqlData.GetString(5)));
                }
            }
        }

        // Borrow a book for student

        public static void BorrowBook(string studentName, Buch buch)
        {   
            if(buch.Verfuegbar > 0)
            { 
                SqlConnection sqlConnection = DataBase.Connection();

                string query = String.Format(@"declare @sId int = (select s.StudentId from Student s where s.Name = '{0}')
                                               declare @bId int = (select b.BookId from Book b where b.Title = '{1}')
                                               Insert Into Borrow(BookId, StudentId) Values(@bId, @sId)
                                               Update NumberOverview
                                               Set
                                               NumberOverview.AnzahlBorrow += 1
                                               Update Book 
                                               Set 
                                               Book.Verfuegbar -=1 
                                               where Book.Title = '{1}'"
                                               ,studentName, buch.Titel);

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                sqlCommand.CommandTimeout = 60;

                sqlCommand.ExecuteNonQuery();
            }
            else if(buch.Verfuegbar == 0)
            {
                Application.Current.MainPage.DisplayAlert("Alert", "Buch ist nicht Verfügbar", "Ok");
            }
        }

        public ICommand RemoveBorrowCommand => new Command(ReturnBook);

        private void ReturnBook()
        {
            SqlConnection sqlConnection = DataBase.Connection();

            string query = String.Format(@"delete Borrow from Book b, Student s, Borrow bo 
                                        where bo.BookId = b.BookId and bo.StudentId = s.StudentId 
                                        and s.Name = '{0}' and b.Title = '{1}'
                                        Update NumberOverview
                                        Set
                                        NumberOverview.AnzahlBorrow -= 1
                                        Update Book 
                                        Set 
                                        Book.Verfuegbar +=1 
                                        where Book.Title = '{1}'"
                                        , SelectedBorrow.Ausgeliehenvon, SelectedBorrow.Title);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.CommandTimeout = 60;

            sqlCommand.ExecuteNonQuery();

            Borrows.Remove(SelectedBorrow);
        }
    }
}
