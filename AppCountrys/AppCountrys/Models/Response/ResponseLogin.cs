using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountrys.Models.Response
{
    /// <summary>
    /// Response de inicio de sesion
    /// </summary>
    public class ResponseLogin
    {
        public string token { get; set; }
        public int expire_date { get; set; }
    }
}
