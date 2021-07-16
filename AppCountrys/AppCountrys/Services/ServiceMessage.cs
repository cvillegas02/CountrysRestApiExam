using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCountrys.Services
{
    /// <summary>
    /// Servicio de Mensaje
    /// </summary>
    public class ServiceMessage
    {
        static ServiceMessage defaultInstance = new ServiceMessage();

        public static ServiceMessage DefaultServicio
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        public async Task ShowMessage(string message, string title)
        {
            await App.Navigator.DisplayAlert(title, message, "OK");
        }
    }
}
