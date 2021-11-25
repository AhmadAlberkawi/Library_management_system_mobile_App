using System;
using System.Collections.Generic;
using System.Text;

namespace Bib.Klassen
{
    class Borrow
    {
        public string Foto { get; set; }

        public string Title { get; set; }

        public string Isbn { get; set; }

        public string Verlag { get; set; }

        public string Autor { get; set; }

        public string Ausgeliehenvon { get; set; }

        public int Exemplarnr { get; set; }

        public string Kategorie { get; set; }
        public Borrow(string foto, string title, string isbn, string verlag, string autor, string ausgeliehenVon)
        {
            Foto = foto;
            Title = title;
            Isbn = isbn;
            Verlag = verlag;
            Autor = autor;
            Ausgeliehenvon = ausgeliehenVon;
        }

        public Borrow(string foto, string title, string isbn, string verlag, string autor, string ausgeliehenVon, int exemplarnr, string kategorie)
        {
            Foto = foto;
            Title = title;
            Isbn = isbn;
            Verlag = verlag;
            Autor = autor;
            Exemplarnr = exemplarnr;
            Kategorie = kategorie;
            Ausgeliehenvon = ausgeliehenVon;
        }
    }
}
