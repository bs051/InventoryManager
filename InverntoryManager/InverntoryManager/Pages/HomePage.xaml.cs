using InverntoryManager.Data;
using InverntoryManager.Models;
using SQLite;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InverntoryManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public User user { get; set; }
        private SQLiteAsyncConnection _connection;
        private ObservableCollection<Item> _items;

        public HomePage()
        {
            user = ConstentsUser.user;
            InitializeComponent();
            profile.Text = user.Username;
            itemCollectionView.SelectionMode = SelectionMode.None;
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
        }

        protected override async void OnAppearing()
        {
            try
            {
                var table = await _connection.Table<Item>().ToListAsync();
                var items = from item in table
                            where item.Owner == user.Username
                            orderby item.AddDate descending
                            select item;

                _items = new ObservableCollection<Item>(items);
                itemCollectionView.ItemsSource = _items;
            }
            catch
            {
                return;
            }
            base.OnAppearing();
        }
    }
}