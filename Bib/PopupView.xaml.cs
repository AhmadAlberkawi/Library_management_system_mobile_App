using Biblio_test;
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
        StudentListViewModel studentListViewModel;

        public PopupView()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.UWP) {
                fenster.Margin = new Thickness(450, 220, 450, 80);
            }
        }

        private void registrieren_Clicked(object sender, EventArgs e)
        {

            Student st1 = new Student(Int32.Parse(Matrikelnummer.Text), Int32.Parse(BibliothekNummer.Text), Name.Text, Vorname.Text, Email.Text, "");

            studentListViewModel.AddStudent(st1);
        }
    }
}