using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Biblio_test
{
    public abstract class Person
    {
        protected string name;
        protected string vorname;
        protected string email;
        protected string foto;

        //protected string passwort;

        public Person(string name, string vorname, string email, string foto)//, string passwort)
        {
            Name = name;
            Vorname = vorname;
            Email = email;
            Foto = foto;

        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Name darf nicht leer sein");
                }
                else
                {
                    this.name = value;
                }
            }
        }

        public String Vorname
        {
            get
            {
                return vorname;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Vorname darf nicht leer sein");
                }
                else
                {
                    this.vorname = value;
                }
            }
        }

        public String Email
        {
            get
            {
                return email;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Email darf nicht leer sein");
                }
                else
                {
                    this.email = value;
                }
            }
        }

        public String Foto
        {
            get
            {
                return foto;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Foto darf nicht leer sein");
                }
                else
                {
                    this.foto = value;
                }
            }
        }

       /* public String Password
        {
            get
            {
                return passwort;
            }

            set
            {
               
            }
        }*/


        // InfoPeron
        public override string ToString() {
            return name + vorname + email + foto;
        }

    }
}
