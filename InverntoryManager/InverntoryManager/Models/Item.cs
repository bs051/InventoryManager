using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace InverntoryManager.Models
{
    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        private string _Name;

        [MaxLength(255)]
        public string Name 
        { 
            get { return _Name; }
            set 
            {
                if(_Name == value) { return; }
                _Name = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(Name));
            } 
        }

        private string _ImageUrl;

        [MaxLength(255)]
        public string ImageUrl 
        {
            get { return _ImageUrl; }
            set
            {
                if(_ImageUrl == value) { return; }
                _ImageUrl = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(ImageUrl));
            }
        }

        private int _Stock;

        public int Stock 
        {
            get { return _Stock; }
            set
            {
                if (_Stock == value) { return; }
                _Stock = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(Stock));
             }
        }

        [MaxLength(255)]
        public string Owner { get; set; }

        [MaxLength(255)]
        public string Class { get; set; }

        [MaxLength(255)]
        public DateTime AddDate { get; set; }

        [MaxLength(255)]
        public DateTime UpdateData { get; set; }

        public bool Selected { get; set; }

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
