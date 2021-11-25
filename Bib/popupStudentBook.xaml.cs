using Bib.Klassen;
using Biblio_test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bib
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class popupStudentBook
    {
        private string studentName;

        public popupStudentBook(Student student)
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                this.BindingContext = new BookListViewModel();
                upbutton.IsVisible = false;
            }
            studentName = student.Name;
        }

        private void SearchBar_Book(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as BookListViewModel;
            BookListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                BookListView.ItemsSource = _container.Books;
            else
                BookListView.ItemsSource = _container.Books.Where(i => i.Titel.ToLower().Contains(e.NewTextValue.ToLower()));

            BookListView.EndRefresh();
        }

        private void registrieren_Clicked(object sender, EventArgs e)
        {
            Buch buch = BookListView.SelectedItem as Buch;

            Button b = (Button)sender;

            switch (b.Text)
            {
                case ("Bücher anzeigen"):
                    this.BindingContext = new BorrowListViewModel(studentName);
                    break;
                case ("Zurückgeben"):
                    if (studentName != null && buch != null)
                        DisplayAlert("Alert", "Dieses Buch\n(" + buch.ToString() + ")\nmuss von diesem Student\n(" + studentName.ToString() + ")\ngelöscht werden", "OK");
                    break;
            }
        }
    }
}