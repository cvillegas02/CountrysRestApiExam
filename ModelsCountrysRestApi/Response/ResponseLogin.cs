using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsCountrysRestApi.Response
{
    public class ResponseLogin
    {
        public string token { get; set; }
        public int expire_date { get; set; }
    }
}
