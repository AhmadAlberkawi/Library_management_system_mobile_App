using Bib.Klassen;
using Biblio_test;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace Bib
{   
    class StudentListViewModel
    {
        public ObservableCollection<Student> Students { get; set; }

        public Student SelectedStudents { get; set; }

        public StudentListViewModel() 
        {
            string query = "select * from Student";

            SqlDataReader data = DataBase.ExecuteQueryReader(query);

            if (data.HasRows)
            {
                Students = new ObservableCollection<Student>();
                while (data.Read())                
                {
                    Students.Add(new Student(data.GetInt32(4), data.GetInt32(5), data.GetString(1),
                                             data.GetString(2), data.GetString(3), data.GetString(6)));
                }   
            }
        }

        // Add Student
        public static void AddStudentinList(Student st) 
        {
            string query = string.Format(@"Insert into Student(Name, Vorname, Email, Matrikul, biblioNummer, Foto) 
                                        Values ('{0}', '{1}','{2}', {3}, {4}, '')
                                        Update NumberOverview
                                        Set
                                        NumberOverview.AnzahlStudent += 1", 
                                        st.Name, st.Vorname, st.Email, st.Matrikul, st.BiblioNummer);

            DataBase.ExcuteQuery(query);
        }

        // Update Student
        public static void UpdateStudentInList(Student st, string selectedStudentName)
        {
            string query = string.Format(@"Update Student
                                           Set
                                           Name = '{0}', Vorname = '{1}', Email = '{2}', Matrikul = {3}, biblioNummer = {4}
                                           where Student.Name = '{5}'",
                                           st.Name, st.Vorname, st.Email, st.Matrikul, st.BiblioNummer, selectedStudentName);

            DataBase.ExcuteQuery(query);
        }

        // remove student
        public ICommand RemoveStudentCommand => new Command(RemoveStudent);

        public void RemoveStudent()
        {   
            if(SelectedStudents != null)
            { 
                string query = string.Format(@"DELETE Student
                                              where Student.Name = '{0}'"
                                              ,SelectedStudents.Name);

                string query1 = string.Format(@"Update NumberOverview
                                                Set
                                                NumberOverview.AnzahlStudent -= 1");
                try 
                {
                    DataBase.ExcuteQuery(query);
                    Students.Remove(SelectedStudents);
                    DataBase.ExcuteQuery(query1);
                }
                catch (Exception)
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Student kann nicht " +
                        "gelöscht werden, befor er alle Bücher zurückgibt.", "Ok");
                }
            }
        }
    }
}
