using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using InverntoryManager.Models;
using InverntoryManager.Data;

namespace InverntoryManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private User _user { get; set; }
        public AddItemPage(User user)
        {
            _user = user;
            InitializeComponent();
            try { _connection = DependencyService.Get<ISQLiteDb>().GetConnection(); }
            catch 
            {
                DisplayAlert("Error", "No table connected", "OK");
                return; 
            }
        }

        private bool CheckForm()
        {
            if (String.IsNullOrEmpty(itemName.Text))
            {
                DisplayAlert("Name is Empty","Item needs a name","Ok");
                return false;
            }
            return true;
        }

        private int GetAmount(string text)
        {
            if (String.IsNullOrEmpty(text)) 
            {
                return 0;
            }
            return Int32.Parse(text);
        }

        private string GetImage(string text)
        {
            if (String.IsNullOrEmpty(text))
            {
                return "noImage.png";
            }
            return text;
        }

        private async void add_Pressed(object sender, EventArgs e)
        {
            await _connection.CreateTableAsync<Item>();

            var item = new Item()
            {
                Name = itemName.Text,
                Stock = GetAmount(itemStock.Text),
                ImageUrl = GetImage(pictureUrl.Text),
                Owner = _user.Username,
                Class = "something",
                AddDate = DateTime.Now,
                UpdateData = DateTime.Now,
                Selected = false
            };

            if (CheckForm())
            {
                await _connection.InsertAsync(item);
                await Navigation.PopModalAsync();
            }
        }

        private async void goBack_Pressed(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}