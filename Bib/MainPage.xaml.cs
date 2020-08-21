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

namespace Bib
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
         
            if(Device.RuntimePlatform == Device.Android)
            {
                Padding = new Thickness(0, -15, 0, 0);
                Student.Margin = new Thickness(50, 0, 0, 0);
                Borrow.Margin = new Thickness(0, 0, 50, 0);
                Books.Margin = new Thickness(50, 50, 0, 50);
                Admin.Margin = new Thickness(0, 0, 50, 50);
            }

            if (Device.RuntimePlatform == Device.UWP)
            {
                HPP.Margin = new Thickness(10, 10, 0, 60);
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            switch (b.Text)
            {
                case ("Überblick"):
                    Student_Page.IsVisible = false;
                    Ausleihe_Page.IsVisible = false;
                    Bücher_Page.IsVisible = false;
                    Admin_Page.IsVisible = false;
                    Ueberblick_Page.IsVisible = true;
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
                    Ueberblick_Page.IsVisible = false;
                    Student_Page.IsVisible = false;
                    Bücher_Page.IsVisible = false;
                    Admin_Page.IsVisible = false;
                    Ausleihe_Page.IsVisible = true;
                    break;

                case ("Bücher"):
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

        private void Button_Clicked_Student(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            Student student = StudentListView.SelectedItem as Student;

            switch (b.Text)
            {
                case ("Regestrieren"):
                    PopupNavigation.Instance.PushAsync(new PopupView("", null));
                    break;

                case ("Bearbeiten"):
                    PopupNavigation.Instance.PushAsync(new PopupView("Bearbeiten", student));
                    break;
            }
        }

        private void Button_Clicked_Admin(object sender, EventArgs e)
        {
            Admin admin = AdminListView.SelectedItem as Admin;
    
            Button b = (Button)sender;

            switch (b.Text)
            {
                case ("Regestrieren"):
                    PopupNavigation.Instance.PushAsync(new PopupViewAdmin("",null));
                    break;

                case ("Bearbeiten"):
                    PopupNavigation.Instance.PushAsync(new PopupViewAdmin("Bearbeiten", admin));
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
    }
}
