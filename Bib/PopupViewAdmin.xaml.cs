using Bib.Klassen;
using Biblio_test;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Security;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Security.Cryptography;
using Rg.Plugins.Popup.Services;

namespace Bib
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupViewAdmin 
    {
        private string buttonClicked;
        private string selectedAdminName;
        public PopupViewAdmin(string button, Admin admin)
        {
            InitializeComponent();

            buttonClicked = button;

            this.BindingContext = new AdminListViewModel();

            if (Device.RuntimePlatform == Device.UWP)
            {
                fenster.Margin = new Thickness(450, 220, 450, 80);
                header.Margin = new Thickness(0, 0, 0, 40);
                registrieren.Margin = new Thickness(0, 40, 0, 0);
            }

            if (button.Equals("Bearbeiten"))
            {
                //  Foto.ImageSource = admin.Foto;
                selectedAdminName = admin.Name;
                Name.Text = admin.Name;
                Vorname.Text = admin.Vorname;
                Email.Text = admin.Email;
                Rolle.Text = admin.Rolle;
                Passwort.Text = admin.Passwort;
                ConPasswort.IsVisible = false;

                registrieren.Text = "aktualisieren";
                Title.Text = "Admin bearbeiten";
            }
        }

        private void registrieren_Clicked(object sender, EventArgs e)
        {
            var email = Email.Text;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            Match match = regex.Match(email);

            if (!buttonClicked.Equals("Bearbeiten")) 
            { 
                if (match.Success && Passwort.Text.Equals(ConPasswort.Text))
                {
                    string encyptPassword = Encypt(Passwort.Text);

                    Admin st1 = new Admin(Name.Text, Vorname.Text, Email.Text,"", Rolle.Text, encyptPassword);

                    AdminListViewModel.AddAdmin(st1);
                
                    DisplayAlert("Alert", "Admin: "+st1.Name+ " wurde hinzugefügt.", "OK");
                }
                else if (!match.Success && !Passwort.Text.Equals(ConPasswort.Text))
                    DisplayAlert("Alert", "Die Email und Passwörter stimmen nicht.", "OK");

                else if(!Passwort.Text.Equals(ConPasswort.Text))
                    DisplayAlert("Alert", "Die Passwörter stimmen nicht miteinander.", "OK");
            
                else if (!match.Success)
                    DisplayAlert("Alert", "Die Email stimmt nicht.", "OK");
            }
            else
            {
                if (!match.Success)
                { 
                    DisplayAlert("Alert", "Die Email stimmt nicht.", "OK");
                }
                else
                {
                    string encyptPassword = Encypt(Passwort.Text);

                    Admin st1 = new Admin(Name.Text, Vorname.Text, Email.Text, "", Rolle.Text, encyptPassword);

                    AdminListViewModel.UpdateAdmin(st1, selectedAdminName);
                }
            }
            PopupNavigation.Instance.PopAsync();
        }

        static string Encypt(string value)
        {
            using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                UTF8Encoding uTF8 = new UTF8Encoding();
                byte[] data = md5.ComputeHash(uTF8.GetBytes(value));
                return Convert.ToBase64String(data);
            }
        }

        // Add Image for Admin
        private async void Add_Image(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Keine Kamera", "Keine Kamera Verfügbar.", "Ok");
                return;
            }

            _ = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,

            });

        }
    }
}