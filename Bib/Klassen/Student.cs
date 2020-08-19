using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;

namespace Biblio_test
{
    public class Student :Person
    {
        private int matrikul;
        private int biblioNummer;


       public Student(int matrikul, int biblioNummer, string name, string vorname, string email, string foto):
            base(name, vorname, email, foto)
        {
            Matrikul = matrikul;
            BiblioNummer = biblioNummer;
        }

        public int Matrikul
        { 
            get 
            {
                return matrikul;
            }

            set
            {
                if (value == 0 || value < 0)
                {
                    Console.WriteLine("Matrikul stimmt nicht");
                }
                else
                {
                    this.matrikul = value;
                }
            }
        }

        public int BiblioNummer
        {
            get
            {
                return biblioNummer;
            }

            set
            {
                if (value == 0 || value < 0)
                {
                    Console.WriteLine("BiblioNummer stimmt nicht");
                }
                else
                {
                    this.biblioNummer = value;
                }
            }
        }


        public override string ToString()
        {
            return base.ToString()+ matrikul + biblioNummer;
        }
        
    }
}
