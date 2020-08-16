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

            this.BindingContext = new StudentListViewModel(); 
         
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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as StudentListViewModel;
            StudentListView.BeginRefresh();

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
                StudentListView.ItemsSource = _container.Students;
            else
                StudentListView.ItemsSource = _container.Students.Where(i => i.Matrikul.ToString().Contains(e.NewTextValue));

            StudentListView.EndRefresh();
        }

        private void Button_Clicked_Student(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            switch (b.Text)
            {
                case ("Regestrieren"):
                    PopupNavigation.Instance.PushAsync(new PopupView());
                    break;

                case ("Bearbeiten"):

                    break;

                case ("Löschen"):

                    break;
            }
        }

    }
}
