using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    class Kategorie
    {
        private string k_name;
        // private int id;
        // private List<Buch> BuchList;

        public Kategorie(string k_name)
        {
            Bechreibung = k_name;
        }

        public string Bechreibung
        {
            get
            {
                return k_name;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Beschreibung darf nicht leer sein");
                }
                else
                {
                    this.k_name = value;
                
                }
            }
        }

        public override string ToString()
        {
            return k_name;
        }

    }
}

