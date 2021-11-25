using Bib.Klassen;
using Biblio_test;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;


namespace Bib
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage(string adminName, string adminRolle)
        {
            InitializeComponent();

            if(adminRolle.Equals("Admin"))
            {
                B_Admin.IsEnabled = false;
            }

            AdminName.Text = adminName;

            this.BindingContext = new UeberblickViewModel();

            if (Device.RuntimePlatform == Device.Android)
            {
                Padding = new Thickness(0, -15, 0, 0);
                Student.Margin = new Thickness(50, 0, 0, 0);
                Borrow.Margin = new Thickness(0, 0, 50, 0);
                Books.Margin = new Thickness(50, 50, 0, 50);
                Admin.Margin = new Thickness(0, 0, 50, 50);
                BButon.Margin = new Thickness(0, 0, 80, 0);
            }

            if (Device.RuntimePlatform == Device.UWP)
            {
                HPP.Margin = new Thickness(10, 10, 0, 60);
            }

            Navigation.PopAsync();
        }
        
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
        
        private void Button_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            switch (b.Text)
            {
                case ("Überblick"):
                    this.BindingContext = new UeberblickViewModel();
                    Student_Page.IsVisible = false;
                    Ausleihe_Page.IsVisible = false;
                    Bücher_Page.IsVisible = false;
                    Admin_Page.IsVisible = false;
                    Ueberblick_Page.IsVisible = true;
                 //   Anzhal_Buecher.Text = Buch.BookId.ToString();
                    break;

                case ("Student"):
                    this.BindingContext = new StudentListViewModel();
                    Ueberblick_Page.IsVisible = false;
                    Ausleihe_Page.IsVisible = false;
                    Bücher_Page.IsVisible = false;
                    Admin_Page.IsVisible = false;
                    Student_Page.IsVisible = true;
                    break;

                case ("Ausleihe"):
                    this.BindingContext = new BorrowListViewModel();
                    Ueberblick_Page.IsVisible = false;
                    Student_Page.IsVisible = false;
                    Bücher_Page.IsVisible = false;
                    Admin_Page.IsVisible = false;
                    Ausleihe_Page.IsVisible = true;
                    break;

                case ("Bücher"):
                    this.BindingContext = new BookListViewModel();
                    Ueberblick_Page.IsVisible = false;
                    Student_Page.IsVisible = false;
                    Ausleihe_Page.IsVisible = false;
                    Admin_Page.IsVisible = false;
                    Bücher_Page.IsVisible = true;
                    break;

                case ("Admin"):
                    this.BindingContext = new AdminListViewModel();
                    Ueberblick_Page.IsVisible = false;
                    Student_Page.IsVisible = false;
                    Ausleihe_Page.IsVisible = false;
                    Bücher_Page.IsVisible = false;
                    Admin_Page.IsVisible = true;
                    break;
                case ("Abmelden"):
                    Navigation.PushAsync(new Page1(), true);
                    break;
            }
        }

        private async void Button_Clicked_Student(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            Student student = StudentListView.SelectedItem as Student;

            switch (b.Text)
            {
                case ("Regestrieren"):
                    await PopupNavigation.Instance.PushAsync(new PopupView("", null));
                    await Task.Factory.StartNew(() => { Initialise(); });
                    StudentListView_Refreshing(null, null);
                    break;
                case ("Bearbeiten"):
                    if (student != null)
                        await PopupNavigation.Instance.PushAsync(new PopupView("Bearbeiten", student));
                        await Task.Factory.StartNew( () => { Initialise(); });
                        StudentListView_Refreshing(null, null);
                    break;
                case ("Buch ausleihen"):
                   await PopupNavigation.Instance.PushAsync(new PopupViewBorrow(student));
                    break;
                case ("Bücher anzeigen"):
                    await PopupNavigation.Instance.PushAsync(new popupStudentBook(student));
                    break;
            }
        }

        private void Initialise()
        {
            while(PopupNavigation.Instance.PopupStack.Count != 0)
            {
                Thread.Sleep(100);
            }
        }

        private async void Button_Clicked_Admin(object sender, EventArgs e)
        {
            Admin admin = AdminListView.SelectedItem as Admin;
    
            Button b = (Button)sender;

            switch (b.Text)
            {
                case ("Regestrieren"):
                        await PopupNavigation.Instance.PushAsync(new PopupViewAdmin("",null));
                        await Task.Factory.StartNew(() => { Initialise(); });
                        AdminListView_Refreshing(null, null); ;
                    break;

                case ("Bearbeiten"):
                    if(admin != null)
                        await PopupNavigation.Instance.PushAsync(new PopupViewAdmin("Bearbeiten", admin));
                        await Task.Factory.StartNew(() => { Initialise(); });
                        AdminListView_Refreshing(null, null); ;
                    break;

            }
        }

        private async void Button_Clicked_Book(object sender, EventArgs e)
        {
            Buch buch = BookListView.SelectedItem as Buch;

            Button b = (Button)sender;

            switch (b.Text)
            {
                case ("Regestrieren"):
                        await PopupNavigation.Instance.PushAsync(new PopupViewBook("",null));
                        await Task.Factory.StartNew(() => { Initialise(); });
                        BuercherListView_Refreshing(null, null);
                    break;

                case ("Bearbeiten"):
                    if(buch != null)                    
                        await PopupNavigation.Instance.PushAsync(new PopupViewBook("Bearbeiten", buch));
                        await Task.Factory.StartNew(() => { Initialise(); });
                        BuercherListView_Refreshing(null, null);
                    break;
            }
        }

        private void SearchBar_Student(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as StudentListViewModel;
            StudentListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                StudentListView.ItemsSource = _container.Students;
            else
                StudentListView.ItemsSource = _container.Students.Where(i => i.Matrikul.ToString().Contains(e.NewTextValue));

            StudentListView.EndRefresh();
        }

        private void SearchBar_Admin(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as AdminListViewModel;
            AdminListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                AdminListView.ItemsSource = _container.Admins;
            else
                AdminListView.ItemsSource = _container.Admins.Where(i => i.Name.ToLower().Contains(e.NewTextValue.ToLower()));

            AdminListView.EndRefresh();
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

        private void SearchBar_Borrow(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as BorrowListViewModel;
            BorrowListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                BorrowListView.ItemsSource = _container.Borrows;
            else
                BorrowListView.ItemsSource = _container.Borrows.Where(i => i.Title.ToLower().Contains(e.NewTextValue.ToLower()));

            BorrowListView.EndRefresh();
        }

        public void StudentListView_Refreshing(object sender, EventArgs e)
        {
            this.BindingContext = new StudentListViewModel();
            StudentListView.EndRefresh();
        }

        public void BuercherListView_Refreshing(object sender, EventArgs e)
        {
            this.BindingContext = new BookListViewModel();
            BookListView.EndRefresh();
        }

        public void BorrowListView_Refreshing(object sender, EventArgs e)
        {
            this.BindingContext = new BorrowListViewModel();
            BorrowListView.EndRefresh();
        }

        public void AdminListView_Refreshing(object sender, EventArgs e)
        {
            this.BindingContext = new AdminListViewModel();
            AdminListView.EndRefresh();
        }

    }
}
