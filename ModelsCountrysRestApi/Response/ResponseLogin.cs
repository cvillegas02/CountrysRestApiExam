using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsCountrysRestApi.Response
{
    public class ResponseLogin
    {
        public string Token { get; set; }
        public int ExpireDate { get; set; }
    }
}
