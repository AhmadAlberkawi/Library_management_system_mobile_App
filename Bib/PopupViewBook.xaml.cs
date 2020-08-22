using Bib.Klassen;
using Biblio_test;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
    public partial class PopupViewBook
    {
        public PopupViewBook(string button, Buch buch)
        {
            InitializeComponent();

            this.BindingContext = new BookListViewModel();

            if (Device.RuntimePlatform == Device.UWP)
            {
                fenster.Margin = new Thickness(450, 220, 450, 80);
            }

            if (Device.RuntimePlatform == Device.Android)
            {
                fenster.Margin = new Thickness(450, 130, 450, 10);
                header.Margin = new Thickness(0, 0, 0, 0);
                registrieren.Margin = new Thickness(0, 5, 0, 0);
            }

            
            if (button.Equals("Bearbeiten"))
            {
                Foto.ImageSource = buch.B_foto;
                Titel.Text = buch.Titel;
                Isbn.Text = buch.Isbn.ToString();
                Verlag.Text = buch.Verlag;
                Anzahl.Text = buch.Anzahl.ToString();
                Autor.Text = buch.Autor;
                ExemplarNr.Text = buch.Exemplarnr.ToString();
                Kategorie.Text = buch.Kategorie;

                registrieren.Text = "aktualisieren";
                Title.Text = "Buch bearbeiten";
            }
            

        }

        private void registrieren_Clicked(object sender, EventArgs e)
        {
            //new Buch("C# Programmierung",2020504,"Klett-Gruppe",1,3,"https://i.imgur.com/LwYSutr.jpg","Daniel Lorig",2,"Fachbuch"),

            Buch buch = new Buch(Titel.Text, Int32.Parse(Isbn.Text), Verlag.Text, 0, Int32.Parse(Anzahl.Text), "",Autor.Text, Int32.Parse(ExemplarNr.Text), Kategorie.Text);
                DisplayAlert("Alert", buch.ToString(), "OK");
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