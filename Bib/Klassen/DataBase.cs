using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Bib.Klassen
{
    class DataBase
    {
        private static SqlConnection sqlConnection;

        public static SqlConnection Connection()
        {
            string connectionString = @"Server=nameOfServerOrIp;Database=Bib_DB;User Id=sa;Password=*******;Trusted_Connection=false";

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return sqlConnection;
        }

        public static void ExcuteQuery(string query)
        {
            SqlConnection sqlConnection = Connection();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.CommandTimeout = 60;

            sqlCommand.ExecuteNonQuery();

            sqlConnection.Close();
        }

        public static SqlDataReader ExecuteQueryReader(string query)
        {
            SqlConnection sqlConnection = Connection();

            SqlCommand sqlDataAdapter = new SqlCommand(query, sqlConnection);

            sqlDataAdapter.CommandTimeout = 60;

            SqlDataReader data = sqlDataAdapter.ExecuteReader();

            return data;
        }
    }
}
