using InverntoryManager.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace InverntoryManager.Models
{
    public static class Constants
    {
        // Replace strings with your Azure Mobile App endpoint.
        public static string ApplicationURL = @"https://inventorymanager2.azurewebsites.net";
    }

    class ConstentsUser
    {
        private static User _LoginUser = null;

        public static User user
        {
            get { return _LoginUser; }
            set { _LoginUser = value; }
        }
    }

    class MainPageMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<MainPageMasterMenuItem> MenuItems { get; set; }

        public MainPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<MainPageMasterMenuItem>(new[]
            {
                    new MainPageMasterMenuItem { Id = 0, Title = "Home" , TargetType = typeof(HomePage) },
                    new MainPageMasterMenuItem { Id = 1, Title = "Account" , TargetType = typeof(ProfilePage) },
                    new MainPageMasterMenuItem { Id = 2, Title = "Inventory",TargetType = typeof(InventoryPage) },
                    //new MainPageMasterMenuItem { Id = 3, Title = "Notifications",TargetType = typeof(ReportPage) }
            });
        }


        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    class StudentMainPageMasterMenuItem : INotifyPropertyChanged
    {
        public ObservableCollection<StudentPageMasterMenuItem> MenuItems { get; set; }

        public StudentMainPageMasterMenuItem()
        {
            MenuItems = new ObservableCollection<StudentPageMasterMenuItem>(new[]
            {
                    new StudentPageMasterMenuItem { Id = 0, Title = "Home", TargetType = typeof(HomePage) },
                    new StudentPageMasterMenuItem { Id = 1, Title = "Account", TargetType = typeof(ProfilePage) },
                    new StudentPageMasterMenuItem { Id = 2, Title = "Items", TargetType = typeof(InventoryPage) },
                });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
