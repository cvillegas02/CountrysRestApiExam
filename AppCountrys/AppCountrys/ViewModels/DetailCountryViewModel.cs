using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using AppCountrys.Helpers;
using AppCountrys.Models;
using AppCountrys.Models.ModelsDB;
using AppCountrys.Models.Response;
using AppCountrys.Services;
using Xamarin.Forms;

namespace AppCountrys.ViewModels
{
    /// <summary>
    /// ViewModel de la pantalla de detalle del pais
    /// </summary>
    public class DetailCountryViewModel : BaseViewModel
    {
        #region Propertys
        public DetailCountryViewModel()
        {
        }

        public Country Item { get; set; }

        private ObservableCollection<Subdivision> listSubdivision = new ObservableCollection<Subdivision>();

        public ObservableCollection<Subdivision> ListSubdivision
        {
            get { return listSubdivision; }

            set { listSubdivision = value; OnPropertyChanged("ListSubdivision"); }
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
                ListSubdivision.Clear();
                var result = await ServicesRestApi.GetAsync<BaseResponse<List<Subdivision>>>(string.Format(ConfigurationString.URL_SUBDIVISION, Item.alpha), true);

                if (result != null && result.status)
                {
                    ListSubdivision = new ObservableCollection<Subdivision>(result.data);
                }
                else if (result != null && !result.status)
                {
                    ServiceMessage.DefaultServicio.ShowMessage(result.message, "Información");
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
        #endregion

        #region Metodos

        #endregion
    }
}
