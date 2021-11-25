using Biblio_test;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bib.Klassen
{
    class AdminListViewModel
    {
        public ObservableCollection<Admin> Admins { get; set; }

        public Admin SelectedAdmin { get; set; }

        public AdminListViewModel()
        {
            string query = String.Format(@"select * from Admin");

            SqlDataReader sqlData = DataBase.ExecuteQueryReader(query);

            if (sqlData.HasRows)
            {
                Admins = new ObservableCollection<Admin>();
                while (sqlData.Read())
                {
                    Admins.Add(new Admin(sqlData.GetString(1), sqlData.GetString(2), sqlData.GetString(3),
                        sqlData.GetString(4), sqlData.GetString(5) ));
                }
            }
        }

        public static void AddAdmin(Admin admin)
        {
            string query = String.Format(@"Insert Into Admin(Name, Vorname, Email, Foto, Rolle, Passwort)
                                           Values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')
                                           Update NumberOverview
                                           Set
                                           NumberOverview.AnzahlAdmin +=1", 
                                           admin.Name, admin.Vorname, admin.Email, "", admin.Rolle, admin.Passwort);

            DataBase.ExcuteQuery(query);
        }

        public static void UpdateAdmin(Admin admin, string selectedAdminName)
        {
            SqlConnection sqlConnection = DataBase.Connection();

            string query = String.Format(@"Select a.Passwort from Admin a where a.Name = '{0}'", selectedAdminName);


            string query1 = String.Format(@"Update Admin
                                           Set
                                           Admin.Name = '{0}', Admin.Vorname = '{1}', Admin.Email = '{2}', Admin.Rolle = '{3}'
                                           where Admin.Name = '{4}'"
                                           ,admin.Name, admin.Vorname, admin.Email, admin.Rolle, selectedAdminName);

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            SqlDataReader sqlData = sqlCommand.ExecuteReader();

            if(sqlData.HasRows)
            {
                sqlData.Read();
                
                if (sqlData.GetString(0).Equals(admin.Passwort))
                {
                    sqlData.Close();

                    SqlCommand sqlCommand1 = new SqlCommand(query1, sqlConnection);

                    sqlCommand1.CommandTimeout = 60;

                    sqlCommand1.ExecuteNonQuery();

                    Application.Current.MainPage.DisplayAlert("Alert", "Admin: "+ selectedAdminName+" wurde bearbeitet.", "Ok");
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Alert", "Passwort ist falsch.", "Ok");
                }
            }
        }

        public ICommand RemoveAdminCommand => new Command(RemoveAdmin);

        public void RemoveAdmin()
        {
            if(SelectedAdmin != null)
            { 
                string query = String.Format($@"Delete Admin 
                                               where Admin.Name= '{SelectedAdmin.Name}'
                                               Update NumberOverview
                                               Set
                                               NumberOverview.AnzahlAdmin -=1");

                DataBase.ExcuteQuery(query);

                Admins.Remove(SelectedAdmin);
            }
        }
    }
}

