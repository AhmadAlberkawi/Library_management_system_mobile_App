using Bib.Klassen;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

            if (Option.IsToggled)
            {
                SqlConnection sqlConnection = DataBase.Connection();

                string encyptPassword = Encypt(password.Text);

                string query = String.Format(@"Select a.Name, a.Passwort, a.Rolle from Admin a 
                                            where a.Name = '{0}' and a.Passwort = '{1}'"
                                            , username.Text, encyptPassword);

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

                SqlDataReader sqlData = sqlCommand.ExecuteReader();

                if(sqlData.HasRows)
                {
                    sqlData.Read();
                    Navigation.PushAsync(new MainPage(sqlData.GetString(0), sqlData.GetString(2)), true);
                }
                else
                    DisplayAlert("Error", "Die Eingaben sind unrichtig", "OK");
            }
        }

        static string Encypt(string value)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding uTF8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(uTF8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }
    }
}