using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsCountrysRestApi.ModelsDB
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string salt { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
