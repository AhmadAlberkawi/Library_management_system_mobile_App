using Biblio_test;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupView
    {
        public PopupView()
        {
            InitializeComponent();

            this.BindingContext = new StudentListViewModel();

            if (Device.RuntimePlatform == Device.UWP) {
                fenster.Margin = new Thickness(450, 220, 450, 80);
            }
        }

        private void registrieren_Clicked(object sender, EventArgs e)
        {

            Student st1 = new Student(Int32.Parse(Matrikelnummer.Text), Int32.Parse(BibliothekNummer.Text), Name.Text, Vorname.Text, Email.Text, "");

            DisplayAlert("Alert", st1.ToString(), "OK");
        }

        // Add Image for student
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