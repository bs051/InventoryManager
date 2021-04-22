using InverntoryManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InverntoryManager
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StudentMainPageMaster : ContentPage
    {
        public ListView ListView;

        public StudentMainPageMaster()
        {
            InitializeComponent();

            BindingContext = new StudentMainPageMasterMenuItem();
            ListView = MenuItemsListView;
        }
    }
}