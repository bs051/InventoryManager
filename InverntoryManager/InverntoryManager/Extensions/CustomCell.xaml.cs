using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InverntoryManager.Extensions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomCell : ViewCell
    {
        public static readonly BindableProperty UrlProperty = 
            BindableProperty.Create("Url", typeof(string), typeof(CustomCell));
        
        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        public static readonly BindableProperty nameProperty =
            BindableProperty.Create("name", typeof(string), typeof(CustomCell));

        public string name 
        { 
            get { return (string)GetValue(nameProperty); }
            set { SetValue(nameProperty,value); }
        }

        public static readonly BindableProperty amountProperty =
            BindableProperty.Create("amount", typeof(string), typeof(CustomCell));
        public string amount
        {
            get { return (string)GetValue(amountProperty); }
            set { SetValue(amountProperty, value); }
        }

        public static readonly BindableProperty selectedProperty =
            BindableProperty.Create("selected", typeof(bool), typeof(CustomCell));

        public bool selected
        {
            get { return (bool)GetValue(selectedProperty); }
            set { SetValue(selectedProperty, value); }
        }

        public static readonly BindableProperty visiableProperty =
            BindableProperty.Create("visiable", typeof(bool), typeof(CustomCell));

        public bool visiable
        {
            get { return (bool)GetValue(visiableProperty); }
            set { SetValue(visiableProperty, value); }
        }

        public CustomCell()
        {
            InitializeComponent();

            BindingContext = this;
        }
    }
}