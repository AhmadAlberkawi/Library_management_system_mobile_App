using Biblio_test;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bib
{   
    class StudentListViewModel : INotifyPropertyChanged
    {
        public ICommand AddStudentsCommand => new Command(AddStudent);

        public ObservableCollection<Student> Students { get; set; }

        public StudentListViewModel() {

            Students = new ObservableCollection<Student>()
            {
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20205094,202050,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),

                new Student(20205095,202050,"Khier","Mohammad","khier@th-brandenburg.de","image.png")
            };

        }

        public void AddStudent(object obj)
        {
            Students.Add((Student)obj);
        }

        #region ListViewImplementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;


            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }
}
