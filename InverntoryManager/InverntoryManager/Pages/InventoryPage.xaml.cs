using InverntoryManager.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InverntoryManager.Data;

namespace InverntoryManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InventoryPage : ContentPage
    {
        public User _user { get; set; }
        public Item item { get; set; }
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Item> _items;

        private BindableProperty IsSearchingProperty =
            BindableProperty.Create("IsSearching", typeof(bool), typeof(InventoryPage), false);

        public bool IsSearching
        {
            get { return (bool)GetValue(IsSearchingProperty); }
            set { SetValue(IsSearchingProperty, value); }
        }

        public InventoryPage()
        {
            _user = ConstentsUser.user;
            InitializeComponent();
            Init();
        }

        private async void Init()
        {
            try { _connection = DependencyService.Get<ISQLiteDb>().GetConnection(); }
            catch { await DisplayAlert("Error", "SQL Table Connection", "OK"); }
            itemCollectionView.ItemsSource = await getData();
        }

        protected override async void OnAppearing()
        {
            try
            {
                _items = await getData();
                itemCollectionView.ItemsSource = _items;
            }
            catch
            {
                return;
            }
            base.OnAppearing();
        }

        private async Task FindItems(string Item)
        {
            try
            {
                IsSearching = true;
                var table = await _connection.Table<Item>().ToListAsync();
                var items = from item in table
                            where item.Name == Item
                            select item;
                itemCollectionView.ItemsSource = items;
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Could not retrieve the list of items.", "OK");
            }
            finally
            {
                IsSearching = false;
            }
        }

        private async Task<ObservableCollection<Item>> getData()
        {
            try
            {
                if (_user.admin)
                {
                    var table = await _connection.Table<Item>().ToListAsync();
                    var items = from item in table
                                where item.Owner == _user.Username
                                select item;
                    return new ObservableCollection<Item>(items);
                }
                else
                {
                    var table = await _connection.Table<Item>().ToListAsync();
                    var items = from item in table
                               where item.Owner != _user.Username
                               select item;
                    return new ObservableCollection<Item>(items);
                }
            }

            catch
            {
                return null;
            }
                
        }

        private async void OnAdd_Clicked(object sender, EventArgs e)
        {
            if (_user.admin)
            {
                try
                {
                    await Navigation.PushModalAsync(new AddItemPage(_user));
                }
                catch
                {
                    await DisplayAlert("Error", "Can not display page", "Ok");
                }
            }
            else
            {
                item = itemCollectionView.SelectedItem as Item;
                var temp = new Item
                {
                    Name = item.Name,
                    Stock = 1,
                    ImageUrl = item.ImageUrl,
                    Owner = _user.Username,
                    Selected = false
                };
                await _connection.InsertAsync(temp);
            }
        }
        private async void Update_Clicked(object sender, EventArgs e)
        {
            if (_user.admin)
            {
                try
                {
                    var item = itemCollectionView.SelectedItem as Item;
                    if (item == null)
                    {
                        await DisplayAlert("Alert", "No item selected!", "OK");
                        return;
                    }
                    await Navigation.PushModalAsync(new UpdatePage(item));
                }
                catch
                {
                    return;
                }
            }
            return;
        }
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            if (_user.admin)
            {
                if (MultiSelect.IsChecked)
                {
                    try
                    {
                        foreach (Item item in _items.ToList())
                        {
                            if (item.Selected)
                            {
                                await _connection.DeleteAsync(item);
                                _items.Remove(item);
                            }
                        }
                    }
                    catch
                    {
                        return;
                    }

                }
                else
                {
                    try
                    {
                        var item = itemCollectionView.SelectedItem as Item;
                        if (item == null)
                        {
                            await DisplayAlert("Alert", "No item selected", "OK");
                            return;
                        }
                        await _connection.DeleteAsync(item);
                        _items.Remove(item);
                    }
                    catch
                    {
                        return;
                    }
                }
            }
           else { return;}
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null)
                return;

            await FindItems(e.NewTextValue);
        }
    }
}