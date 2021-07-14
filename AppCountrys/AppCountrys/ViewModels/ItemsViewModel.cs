using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using AppCountrys.Models;
using AppCountrys.Views;
using AppCountrys.Models.ModelsDB;

namespace AppCountrys.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Country> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Paises";
            Items = new ObservableCollection<Country>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Country>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Country;
                Items.Add(newItem);
                //await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var result = await Services.ServiceRestApi.DefaultServicio.GetCountrys();
                foreach (var item in result.data)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}