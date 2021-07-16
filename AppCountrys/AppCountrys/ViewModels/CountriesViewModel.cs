using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using AppCountrys.Models;
using AppCountrys.Views;
using AppCountrys.Models.ModelsDB;
using AppCountrys.Services;
using AppCountrys.Models.Response;
using AppCountrys.Helpers;
using System.Collections.Generic;
using System.Windows.Input;
using AppCountrys.Localizador;

namespace AppCountrys.ViewModels
{
    /// <summary>
    /// ViewModel de la pantalla de Paises
    /// </summary>
    public class CountriesViewModel : BaseViewModel
    {
        #region Propertys
        private ObservableCollection<Country> listCountry = new ObservableCollection<Country>();

        public ObservableCollection<Country> ListCountry 
        {
            get { return listCountry; }

            set { listCountry = value; OnPropertyChanged("ListCountry"); }
        }
        #endregion

        #region Commands
        public ICommand LoadItemsCommand
        {
            get { return new Command(ExecuteLoadItemsCommand); }
        }

        public async void ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                ListCountry.Clear();
                var result = await ServicesRestApi.GetAsync<BaseResponse<List<Country>>>(ConfigurationString.URL_COUNTRYS, true);
                
                if(result!=null && result.status)
                {
                    ListCountry = new ObservableCollection<Country>(result.data);
                }
                else if(result!=null && !result.status)
                {
                   ServiceMessage.DefaultServicio.ShowMessage(result.message,"Información");
                }
                else 
                {
                    ServiceMessage.DefaultServicio.ShowMessage("Ocurrio un error en el sistema.", "Información");
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

        public ICommand TapCountryCommand
        {
            get { return new Command<Country>(CountryInformation); }
        }

        public async void CountryInformation(Country pParameter)
        {
            if(pParameter!=null)
            {
                LocalizadorInstancia.DefaultServicio._detailCountryVM.Item = pParameter;
                LocalizadorInstancia.DefaultServicio._detailCountryVM.Title = pParameter.name;
                await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushAsync(new DetailCountryPage());  
            }
        }

        public ICommand AddCommand
        {
            get { return new Command(AddCountry); }
        }

        public async void AddCountry()
        {
            await (App.Current.MainPage as MasterDetailPage).Detail.Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        #endregion

        #region Metodos

        #endregion
    }
}