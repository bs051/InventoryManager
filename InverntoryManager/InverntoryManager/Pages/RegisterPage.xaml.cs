using InverntoryManager.Data;
using InverntoryManager.Models;
using SQLite;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InverntoryManager.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private List<User> _users;

        public RegisterPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            try { _connection = DependencyService.Get<ISQLiteDb>().GetConnection(); }
            catch { return; }
            GetUsersTable();
        }

        private async void GetUsersTable()
        {
            try
            {
                await _connection.CreateTableAsync<User>();
                var users = await _connection.Table<User>().ToListAsync();
                _users = new List<User>(users);
            }
            catch
            {
                await DisplayAlert("Error", "Can not find table", "Ok");
                return;
            }
        }

        private bool checkUserName() 
        {
            foreach(var user in _users)
            {
                if (user.Username == userName.Text) 
                {
                    DisplayAlert("Username Invalid!", "Your username already exists.", "OK");
                    return false;
                }
            }
            return true;
        }

        private bool checkPassword()
        {
            if(password.Text == passwordCheck.Text)
            {
                return true;
            }
            DisplayAlert("Invalid Password!", "Your password does not match.", "OK");
            return false;
        }

        private bool AdminCheck()
        {
            if (admin.SelectedItem.ToString() == "Admin")
                return true;
            else
                return false;
        }

        private bool checkInfo()
        {
            if (String.IsNullOrEmpty(firstName.Text))
            {
                DisplayAlert("General info","First name is blank","OK");
                return false;
            }
            if (String.IsNullOrEmpty(lastName.Text))
            {
                DisplayAlert("General info", "Last name is blank", "OK");
                return false;
            }
            if (admin.SelectedItem == null)
            {
                DisplayAlert("General info", "You must choose occupation", "OK");
                return false;
            }
            return true;
        }

        private async void signUpBtm_Clicked(object sender, EventArgs e)
        {
            if(checkPassword() && checkUserName() && checkInfo())
            {
                var user = new User
                {
                    Username = userName.Text,
                    Password = passwordCheck.Text,
                    FirstName = firstName.Text,
                    LastName = lastName.Text,
                    admin = AdminCheck()
                };
                try
                {
                    await _connection.InsertAsync(user);
                    _users.Add(user);
                    await Navigation.PushAsync(new LoginPage());
                }
                catch
                {
                    await DisplayAlert("Error", "Can not add user to table", "OK");
                    return;
                }
            }
        }
    }
}