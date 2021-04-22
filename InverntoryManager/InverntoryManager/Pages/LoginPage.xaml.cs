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
    public partial class LoginPage : ContentPage
    {
        private SQLiteAsyncConnection _connection;
        private List<User> _users;
        private User info { get; set; }

        public LoginPage()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            UserName.Completed += (s, e) => password.Focus();
            password.Completed += (s, e) => signInBtm_Clicked(s, e);
            _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            getUsersTable();
        }

        private async void getUsersTable()
        {
            try 
            { 
                var users = await _connection.Table<User>().ToListAsync();
                _users = new List<User>();
                _users.AddRange(users);
            }
            catch  
            {
                await DisplayAlert("Error","No users in table","OK");
                await Navigation.PushAsync(new RegisterPage());
                return; 
            }
            
        }

        private void signInBtm_Clicked(object sender, EventArgs e)
        {
            if (checkUserInfo())
            {
                ConstentsUser.user = info;
                DisplayAlert("Sign in"," Signin Success","OK");
                if (isAdmin(info.admin))
                    Application.Current.MainPage = new TeacherMainPage();
            }
            else
            {
                DisplayAlert("Sign in failed", "No such username", "OK");
                return;
            }
        }

        private bool isAdmin(bool flag)
        {
            return flag;
        }

        private bool checkUserInfo()
        {
            if (String.IsNullOrEmpty(UserName.Text) && String.IsNullOrEmpty(password.Text))
            {
                DisplayAlert("Sign in", "Invald Username or Password", "OK");
                return false;
            }

            foreach (var user in _users)
            {
                if(UserName.Text == user.Username && password.Text != user.Password)
                {
                    DisplayAlert("Sign in", "Invald Password", "OK");
                    return false;
                }
                if (UserName.Text == user.Username && password.Text == user.Password)
                {
                    info = user;
                    return true;
                }
            }
            return false;
        }

        private void showPassword_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (showPassword.IsChecked) 
            {
                password.IsPassword = false;
            }
            if (!showPassword.IsChecked)
            {
                password.IsPassword = true;
            }
        }
    }
}