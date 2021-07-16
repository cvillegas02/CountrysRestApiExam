using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountrys.Helpers
{
    /// <summary>
    /// Clase encargada de las rutas de servicios
    /// </summary>
    public class ConfigurationString
    {
        public static string CONNECTION_REST_URL_BASE = "http://18.204.209.142:8086/api";

        public static string URL_LOGIN = CONNECTION_REST_URL_BASE + "/Auth/Login";

        public static string URL_COUNTRYS = CONNECTION_REST_URL_BASE + "/Countrys";

        public static string URL_SUBDIVISION = CONNECTION_REST_URL_BASE + "/Countrys/{0}/Subdivisions";

        public static string URL_SAVE_COUNTRY = CONNECTION_REST_URL_BASE + "/Countrys";

    }
}
