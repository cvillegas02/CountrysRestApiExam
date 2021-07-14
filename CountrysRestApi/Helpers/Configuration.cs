using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountrysRestApi.Helpers
{
    public class Configuration
    {
        public static string CONNECTION_BD_MYSQL_LOCAL_HOST = "server=localhost;port=3306;user=root;password=;database=examen_countrys";

        public static string CONNECTION_BD_MYSQL_DEV = "server=localhost;port=3306;user=root;password=E1n3n3di0r0!%#AT;database=examen_countrys";

        public static int SLEEP_MILLISECONDS_SERVICE = 1500;

        public static int DefaultPageNumber = 1;

        public static int DefaultPageSize = 50;
    }
}
