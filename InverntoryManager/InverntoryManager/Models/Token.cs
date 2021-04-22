using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace InverntoryManager.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string accessToken { get; set; }
        public string ErrorDecription { get; set; }
        public DateTime expireDate { get; set; }
        public Token(){ }
    }
}