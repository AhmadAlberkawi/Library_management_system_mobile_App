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
    public partial class Page1 : ContentPage
    {
        public Page1()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.Android)
            {
                header.Padding = new Thickness(30, 10, 0, 20);
                login.FontSize = 32;
                body.VerticalOptions = LayoutOptions.CenterAndExpand;
                body.HorizontalOptions = LayoutOptions.CenterAndExpand;

                bild.Margin = new Thickness(0, 30, 0, 35);
                bild.HeightRequest = 180;
                bild.WidthRequest = 180;

                AdminOption.Margin = new Thickness(0, 5, 150, 20);
                login_button.Padding = new Thickness(80, 15, 80, 15);
                login_button.FontSize = 24;
                login_button.Margin = new Thickness(0, 10, 0, 0);
            }
        }

        private void login_button_Clicked_1(object sender, EventArgs e)
        {
            if (username.Text == "Admin" && password.Text == "Admin" && Option.IsToggled)
                 Navigation.PushAsync(new MainPage(), true);
            else
                DisplayAlert("Error", "Die Eingaben sind unrichtig", "OK");
        }
    }
}