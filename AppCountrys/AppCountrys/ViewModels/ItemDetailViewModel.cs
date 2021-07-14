using System;

using AppCountrys.Models;
using AppCountrys.Models.ModelsDB;

namespace AppCountrys.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Country Item { get; set; }
        public ItemDetailViewModel(Country item = null)
        {
            Title = item?.name;
            Item = item;
        }
    }
}
