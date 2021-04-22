using Newtonsoft.Json;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InverntoryManager.Models
{
    public class User
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        //[JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [MaxLength(255)]
        //[JsonProperty(PropertyName = "username")]
        public string Username { get; set; }
        
        [MaxLength(255)]
        //[JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        private string _firstName;

        [MaxLength(255)]
        //[JsonProperty(PropertyName = "firstname")]
        public string FirstName {
            get { return _firstName; } 
            set
            {
                if (_firstName == value)
                    return;

                _firstName = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _LastName;

        [MaxLength(255)]
        //[JsonProperty(PropertyName = "lastname")]
        public string LastName 
        { 
            get { return _LastName; }
            set 
            {
                if (_LastName == value)
                    return;

                _LastName = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(LastName));
            } 
        }

        //[JsonProperty(PropertyName = "admin")]
        public bool admin { get; set; }

        [MaxLength(255)]
        //[JsonProperty(PropertyName = "imageUrl")]
        public string imageUrl { get; set; }

        [MaxLength(255)]
        //[JsonProperty(PropertyName = "Course")]
        public string Course { get; set; }

        public User() { }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
