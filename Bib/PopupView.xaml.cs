using Biblio_test;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

namespace Bib
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupView
    {
        private string selectedStudentName;
        public PopupView(string button, Student student)
        {
            InitializeComponent();

            this.BindingContext = new StudentListViewModel();

            if (Device.RuntimePlatform == Device.UWP) {
                fenster.Margin = new Thickness(450, 220, 450, 80);
            }

            if (button.Equals("Bearbeiten")) {

                selectedStudentName = student.Name;
               // Foto.ImageSource = student.Foto;
                Name.Text = student.Name;
                Vorname.Text = student.Vorname;
                Email.Text = student.Email;
                Matrikelnummer.Text = student.Matrikul.ToString();
                BibliothekNummer.Text = student.BiblioNummer.ToString();

                registrieren.Text = "aktualisieren";
                Title.Text = "Student bearbeiten";
            }
        }

        private void registrieren_Clicked(object sender, EventArgs e)
        {
            var email = Email.Text;
            var matrikel = Matrikelnummer.Text;

            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex regex1 = new Regex(@"^(2020\d{4})$");

            Match match = regex.Match(email);
            Match match1 = regex1.Match(matrikel);

            if (match.Success && match1.Success)
            {
                Student st1 = new Student(Int32.Parse(Matrikelnummer.Text), Int32.Parse(BibliothekNummer.Text), Name.Text, Vorname.Text, Email.Text, "");
                
                if(Title.Text.Equals("Studnet hinzufügen"))
                { 
                    StudentListViewModel.AddStudentinList(st1);
                }
                else
                {
                    StudentListViewModel.UpdateStudentInList(st1, selectedStudentName);
                }
                PopupNavigation.Instance.PopAsync();
            }

            else if (!match.Success && !match1.Success) 
                DisplayAlert("Alert", "Die Email und Matrikelnummer stimmen nicht.", "OK");

            else if(!match.Success)
                DisplayAlert("Alert", "Die Email stimmt nicht.", "OK");

            else if(!match1.Success)
                DisplayAlert("Alert", "Matrikelnummer stimmt nicht.", "OK");
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