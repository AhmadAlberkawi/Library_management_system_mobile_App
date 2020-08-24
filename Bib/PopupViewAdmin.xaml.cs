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

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bib
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupViewAdmin 
    {
        public PopupViewAdmin(string button, Admin admin)
        {
            InitializeComponent();

            this.BindingContext = new AdminListViewModel();

            if (Device.RuntimePlatform == Device.UWP)
            {
                fenster.Margin = new Thickness(450, 220, 450, 80);
            }

            if (button.Equals("Bearbeiten"))
            {
              //  Foto.ImageSource = admin.Foto;
                Name.Text = admin.Name;
                Vorname.Text = admin.Vorname;
                Email.Text = admin.Email;
                Rolle.Text = admin.Rolle;
                Passwort.Text = admin.Passwort;
                ConPasswort.Text = admin.Passwort;

                registrieren.Text = "aktualisieren";
                Title.Text = "Admin bearbeiten";
            }
        }

        private void registrieren_Clicked(object sender, EventArgs e)
        {
            var email = Email.Text;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            Match match = regex.Match(email);

            if (match.Success && Passwort.Text.Equals(ConPasswort.Text))
            {
                Admin st1 = new Admin(Name.Text, Vorname.Text, Email.Text,"", Rolle.Text, Passwort.Text);
                DisplayAlert("Alert", st1.ToString(), "OK");
            }
            else if (!match.Success)
                DisplayAlert("Alert", "Die Email stimmt nicht.", "OK");
           
            else if(!Passwort.Text.Equals(ConPasswort.Text))
                DisplayAlert("Alert", "Die Passwörter stimmen nicht miteinander.", "OK");
            
            else if(!match.Success && !Passwort.Text.Equals(ConPasswort.Text))
                DisplayAlert("Alert", "Die Email und Passwörter stimmen nicht.", "OK");
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

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,

            });
        }
    }
}