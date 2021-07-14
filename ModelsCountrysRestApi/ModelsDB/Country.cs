using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsCountrysRestApi.ModelsDB
{
    public class Country
    {
        public string alpha { get; set; }
        public string name { get; set; }
        public int numeric_code { get; set; }
        public bool independent { get; set; }
    }
}
