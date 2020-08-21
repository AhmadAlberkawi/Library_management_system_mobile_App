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
    class StudentListViewModel
    {
        public ICommand AddStudentsCommand => new Command(AddStudent);

        public ICommand RemoveStudentCommand => new Command(RemoveStudent);

        public ObservableCollection<Student> Students { get; set; }

        public Student SelectedStudents { get; set; }

        public StudentListViewModel() {

            Students = new ObservableCollection<Student>()
            {
                new Student(20205094,2020504,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20209123,2020501,"Abboud","Zaher","abboudz@th-brandenburg.de","https://i.imgur.com/PmfYwFl.jpg"),
                new Student(20204482,2020502,"Dovonon","Boris","dovonon@th-brandenburg.de","https://i.imgur.com/sRrYbDc.jpg"),
                new Student(20208643,2020503,"Rouatbi","Rami","rouatbi@th-brandenburg.de","https://i.imgur.com/Xl8Tjmu.jpg"),
                new Student(20205094,2020504,"Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg"),
                new Student(20209123,2020501,"Abboud","Zaher","abboudz@th-brandenburg.de","https://i.imgur.com/PmfYwFl.jpg"),
                new Student(20204482,2020502,"Dovonon","Boris","dovonon@th-brandenburg.de","https://i.imgur.com/sRrYbDc.jpg"),
                new Student(20208643,2020503,"Rouatbi","Rami","rouatbi@th-brandenburg.de","https://i.imgur.com/Xl8Tjmu.jpg"),
                new Student(20205095,2020505,"Khier","Mohammad","khier@th-brandenburg.de","image.png")
            };
        }

        public void AddStudent(object obj)
        {
            Students.Add((Student)obj);
        }

        public void RemoveStudent() {

            Students.Remove(SelectedStudents);
        }
    }
}
