using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountrys.Models.ModelsDB
{
    /// <summary>
    /// Modelo de usuario
    /// </summary>
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string salt { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
