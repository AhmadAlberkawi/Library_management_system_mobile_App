using Biblio_test;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Bib.Klassen
{
    class AdminListViewModel
    {
        public ICommand AddAdminCommand => new Command(AddAdmin);

        public ICommand RemoveAdminCommand => new Command(RemoveAdmin);

        public ObservableCollection<Admin> Admins { get; set; }

        public Admin SelectedAdmin { get; set; }

        public AdminListViewModel()
        {
            Admins = new ObservableCollection<Admin>()
            {
                new Admin("Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg",null,null),
                new Admin("Abboud","Zaher","abboudz@th-brandenburg.de","https://i.imgur.com/PmfYwFl.jpg",null,null),
                new Admin("Dovonon","Boris","dovonon@th-brandenburg.de","https://i.imgur.com/sRrYbDc.jpg",null,"123"),
                new Admin("Rouatbi","Rami","rouatbi@th-brandenburg.de","https://i.imgur.com/Xl8Tjmu.jpg",null,null),
                new Admin("Alberkawi","Ahmad","alberkaw@th-brandenburg.de","https://i.imgur.com/WXTbjzq.jpg",null,null),
                new Admin("Abboud","Zaher","abboudz@th-brandenburg.de","https://i.imgur.com/PmfYwFl.jpg",null,null),
                new Admin("Dovonon","Boris","dovonon@th-brandenburg.de","https://i.imgur.com/sRrYbDc.jpg",null,null),
                new Admin("Rouatbi","Rami","rouatbi@th-brandenburg.de","https://i.imgur.com/Xl8Tjmu.jpg",null,null),
                new Admin("Khier","Mohammad","khier@th-brandenburg.de","image.png",null,null)
            };
        }

        public void AddAdmin(object obj)
        {
            Admins.Add((Admin)obj);
        }

        public void RemoveAdmin()
        {
            Admins.Remove(SelectedAdmin);
        }
    }
}

