using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountrys.Models.Request
{
    /// <summary>
    /// Request de Inicio de Sesion
    /// </summary>
    public class RequestLogin
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
