using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsCountrysRestApi.Request
{
    public class RequestRegister
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Departamento { get; set; }
    }
}
