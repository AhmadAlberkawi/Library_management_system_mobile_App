using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Biblio_test
{
    public class Admin :Person
    {
       private string rolle;

       public Admin(string name, string vorname, string email, string foto, string rolle, int passwort):
       base(name, vorname, email, foto)
       {
            Rolle = rolle;
            Passwort = passwort;
       }

        public String Rolle
        {
            get
            {
                return rolle;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Rolle darf nicht leer sein");
                }
                else
                {
                    this.rolle = value;
                }
            }
        }

        public int Passwort { get; private set; }

        public override string ToString()
        {
            return base.ToString() + Rolle;
        }
    }
}
