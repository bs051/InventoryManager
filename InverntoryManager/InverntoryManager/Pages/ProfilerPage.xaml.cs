using InverntoryManager.Data;
using InverntoryManager.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InverntoryManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public User _user { get; set; }
        public string url { get; set; }
        public CollectionView CollectionView;
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Item> _items;

        public ProfilePage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            _user = ConstentsUser.user;
            BindingContext = _user;
            itemCollectionView.SelectionMode = SelectionMode.None;
            getProfileInfo();
            try { _connection = DependencyService.Get<ISQLiteDb>().GetConnection(); }
            catch
            {
                DisplayAlert("Error", "No table connected", "OK");
                return;
            }
        }

        private void getProfileInfo()
        {
            fName.Text = _user.FirstName;
            lName.Text = _user.LastName;
            profileImageHandler();
        }

        public void profileImageHandler()
        {
            if (String.IsNullOrEmpty(_user.imageUrl)) 
            {
                profile.Source = ImageSource.FromResource("HelloWorld.Image.BlankProfile.jpg");
                return;
            }
            else
            {
                url = _user.imageUrl;
            }
        }
        protected override async void OnAppearing()
        {
            try
            {
                int total = 0;
                var table = await _connection.Table<Item>().ToListAsync();
                var items = from item in table
                            where item.Owner == _user.Username
                            orderby item.Stock descending
                            select item;
                _items = new ObservableCollection<Item>(items);

                foreach(var num in _items)
                {
                    total += num.Stock;
                }

                Totalitems.Text = total.ToString();
                itemCollectionView.ItemsSource = _items;
            }
            catch
            {
                return;
            }
            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            bool responce =await DisplayAlert("Sign out", "Would you like to sign out?", "Yes", "No");
            if (responce) { Application.Current.MainPage = new NavigationPage(new OpeningPage()); }
            else { return; }
        }

        private void editProfile_Clicked(object sender, EventArgs e)
        {
            //need page
        }

        private async void inventory_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new InventoryPage());
            }
            catch
            {
                await DisplayAlert("Error", "Can not display page", "Ok");
            }
        }

        private async void delete_Clicked(object sender, EventArgs e)
        {
            bool repsonce = await DisplayAlert("Remove Account", "Are you sure you want to delete this account", "Yes", "No");
            if (repsonce)
            {
                removeUser(_user);
                Application.Current.MainPage = new NavigationPage(new OpeningPage());
            }    
            else { return; }
        }

        private async void removeUser(User user)
        {
           await _connection.DeleteAsync(user);
        }
    }

}