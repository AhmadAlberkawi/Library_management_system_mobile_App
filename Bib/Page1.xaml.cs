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
        }

        private void login_button_Clicked_1(object sender, EventArgs e)
        {
            if (username.Text == "Admin" && password.Text == "Admin")
                 Navigation.PushAsync(new MainPage(), true);
            else
                DisplayAlert("Error", "Die Eingaben sind unrichtig", "OK");
        }
    }
}