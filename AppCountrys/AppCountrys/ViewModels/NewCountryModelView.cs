using AppCountrys.Helpers;
using AppCountrys.Models.ModelsDB;
using AppCountrys.Models.Request;
using AppCountrys.Models.Response;
using AppCountrys.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCountrys.ViewModels
{
    /// <summary>
    /// ViewModel de la pantalla de nuevo pais
    /// </summary>
    public class NewCountryModelView : BaseViewModel
    {
        #region Propertys
        private string _Alpha = "";

        public string Alpha
        {
            get { return _Alpha; }

            set { _Alpha = value; OnPropertyChanged("Alpha"); }
        }

        private string _Name = "";

        public string Name
        {
            get { return _Name; }

            set { _Name = value; OnPropertyChanged("Name"); }
        }

        private int _Code;

        public int Code
        {
            get { return _Code; }

            set { _Code = value; OnPropertyChanged("Code"); }
        }
        #endregion

        #region Commands
        public ICommand SaveCountryCommand
        {
            get { return new Command(SaveCountry); }
        }

        public async void SaveCountry()
        {

            if (string.IsNullOrWhiteSpace(Alpha))
            {
                await ServiceMessage.DefaultServicio.ShowMessage("El correo no puede ser vacío.", "Iniciar Sesión");

                return;
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                await ServiceMessage.DefaultServicio.ShowMessage("La contraseña no puede ser vacía.", "Iniciar Sesión");

                return;
            }

            try
            {
                //se crea el user request
                RequestCountry request = new RequestCountry { alpha = this.Alpha, name = this.Name, numeric_code = this.Code, independent = true };

                var result = await ServicesRestApi.PostAsync<BaseResponse<Country>>(ConfigurationString.URL_SAVE_COUNTRY, request, false);
                if (result != null && result.status)
                {
                    Localizador.LocalizadorInstancia.DefaultServicio._countriesVM.ListCountry.Add(result.data);
                    App.Current.MainPage.Navigation.PopModalAsync();


                }
                else if (result != null && !result.status)
                {
                    await ServiceMessage.DefaultServicio.ShowMessage(result.message, "Iniciar Sesión");
                }
                else
                {
                    await ServiceMessage.DefaultServicio.ShowMessage("Imposible conectar conectar con el servidor, intente de nuevo", "Iniciar Sesión");
                }
            }
            catch (Exception ex)
            {
                await ServiceMessage.DefaultServicio.ShowMessage("Ocurrió un error, vuelva a intentarlo", "Iniciar Sesión");

            }




        }

        public ICommand CancelCommand
        {
            get { return new Command(Cancel); }
        }

        public async void Cancel()
        {

           await App.Current.MainPage.Navigation.PopModalAsync();

        }
        #endregion

    }
}
