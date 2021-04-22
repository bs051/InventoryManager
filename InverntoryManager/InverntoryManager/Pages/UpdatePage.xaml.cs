using InverntoryManager.Data;
using InverntoryManager.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InverntoryManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdatePage : ContentPage
    {
        public Item _item { get; set; }
        private SQLiteAsyncConnection _connection;
        public UpdatePage(Item item)
        {
            _item = item;
            InitializeComponent();
            BindingContext = item;
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
                DisplayAlert("Name is Empty", "Item needs a name", "Ok");
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

        private async void Update_Pressed(object sender, EventArgs e)
        {
            if (CheckForm())
            {
                _item.UpdateData = DateTime.Now;
                await _connection.UpdateAsync(_item);
                await Navigation.PopModalAsync();
            }
        }

        private async void goBack_Pressed(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private void itemName_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _item.Name = itemName.Text;
        }

        private void itemStock_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _item.Stock = GetAmount(itemStock.Text);
        }

        private void pictureUrl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _item.ImageUrl = GetImage(pictureUrl.Text);
        }
    }
}