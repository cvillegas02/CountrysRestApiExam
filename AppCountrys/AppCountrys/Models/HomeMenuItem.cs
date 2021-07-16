using AppCountrys.Models.Enumerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountrys.Models
{
    /// <summary>
    /// Modelode para el menu del master detail
    /// </summary>
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
