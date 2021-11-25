using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bib.Klassen
{
    class UeberblickViewModel
    {
        public UeberblickViewModel()
        {
            string query = String.Format(@"select * from NumberOverview");

            SqlDataReader sqlData = DataBase.ExecuteQueryReader(query);
            
            if (sqlData.HasRows)
            {
                while (sqlData.Read())
                {
                    AnzahlStudent = sqlData.GetInt32(1).ToString();
                    AnzhalBooks = sqlData.GetInt32(2).ToString();
                    AnzahlBorrow = sqlData.GetInt32(3).ToString();
                    AnzahlAdmin = sqlData.GetInt32(4).ToString();
                }
            }
        }

        public string AnzahlStudent { get; set; }

        public string AnzhalBooks { get; set; }

        public string AnzahlBorrow { get; set; }

        public string AnzahlAdmin { get; set; }
    }
}
