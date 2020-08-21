using App;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;


namespace Biblio_test
{
    class Buch
    {
        private string titel;
        private int isbn;
        private string verlag;
        private int verfuegbar;
        private int anzahl;
        private string b_foto;
        private string autor;
        private int exemplarnr;

        //private Kategorie kategorie;
        private int kategorie;

        public Buch(string titel, int isbn, string verlag, int verfuegbar, int anzahl, string foto, string autor, int exemplarnr, int kategorie)
        {
            Titel = titel;
            Isbn = isbn;
            Verlag = verlag;
            Verfuegbar = verfuegbar;
            Anzahl = anzahl;
            B_foto = foto;
            Autor = autor;
            Exemplarnr = exemplarnr;

            Kategorie = kategorie;
        }

        public string Titel 
        { 
            get
            {
                return titel;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Titel darf nicht leer sein");
                }
                else
                {
                    this.titel = value;
                }
            }
        }

        public int Isbn
        {
            get
            {
                return isbn;
            }

            set
            {
                if (value == 0 || value < 0)
                {
                    Console.WriteLine("ISBN stimmt nicht");
                }
                else
                {
                    this.isbn = value;
                }
            }
        }

        public string Verlag
        {
            get
            {
                return verlag;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    Console.WriteLine("verlag darf nicht leer sein");
                }
                else
                {
                    this.verlag = value;
                }
            }
        }

        public int Verfuegbar
        {
            get
            {
                return verfuegbar;
            }

            set
            {
                // brauch kein Set, da er automatisch gerechet wird
                // mit Trigger in der DB
            }
        }

        public int Anzahl
        {
            get
            {
                return anzahl;
            }

            set
            {
                if (value == 0 || value < 0)
                {
                    Console.WriteLine("Anzahl stimmt nicht");
                }
                else
                {
                    this.anzahl = value;
                }
            }
        }

        public string B_foto
        {
            get
            {
                return b_foto;
            }

            set
            {
                // wird implementieren und mit Exception behandeln
            }
        }

        public string Autor
        {
            get
            {
                return autor;
            }

            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Autor darf nicht leer sein");
                }
                else
                {
                    this.autor = value;
                }
            }
        }
        
         public int Exemplarnr
         {
            get
            {
                return exemplarnr;
            }

            set
            {
                if (value == 0 || value < 0)
                {
                    Console.WriteLine("Exemplarnr stimmt nicht");
                }
                else
                {
                    this.exemplarnr = value;
                }
            }
         }

        public int Kategorie
        {
            get
            {
                return kategorie;
            }

            set
            {
                if (value == 0 || value < 0)
                {
                    Console.WriteLine("Kategorie stimmt nicht");
                }
                else
                {
                    this.kategorie = value;
                }
            }
        }

        public override string ToString()
        {
            return titel + isbn + verlag + verfuegbar + anzahl + B_foto + Autor + Exemplarnr + Kategorie;
        }

    }
}
