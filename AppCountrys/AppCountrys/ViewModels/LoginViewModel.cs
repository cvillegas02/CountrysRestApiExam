using AppCountrys.Helpers;
using AppCountrys.Models.Request;
using AppCountrys.Models.Response;
using AppCountrys.Services;
using AppCountrys.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppCountrys.ViewModels
{
    /// <summary>
    /// ViewModel de la pantalla para iniciar sesion
    /// </summary>
    public class LoginViewModel : BaseViewModel
    {
        #region Propertys

        private string user = "cvillegas2@hotmail.es";

        public string User
        {
            get { return user; }

            set { user = value; OnPropertyChanged("Usuario"); }
        }

        private string password = "Abcd1234";

        public string Password
        {
            get { return password; }

            set { password = value; OnPropertyChanged("Password"); }
        }

        #endregion

        #region Commands

        public ICommand LoginCommand
        {
            get { return new Command(UserLogin); }
        }

        public async void UserLogin()
        {

            if (string.IsNullOrWhiteSpace(User))
            {
                await ServiceMessage.DefaultServicio.ShowMessage("El correo no puede ser vacío.", "Iniciar Sesión");

                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                await ServiceMessage.DefaultServicio.ShowMessage("La contraseña no puede ser vacía.", "Iniciar Sesión");

                return;
            }

            
            try
            {
                //se crea el user request
                RequestLogin request = new RequestLogin { email = this.User, password = this.Password };

                var res = await ServicesRestApi.PostAsync<BaseResponse<ResponseLogin>>(ConfigurationString.URL_LOGIN, request, false);
                if (res != null && res.status)
                {
                    
                    if (! string.IsNullOrWhiteSpace(res.data.token))
                    {
                        DependencyService.Get<IRestClient>().Token = res.data.token;
                        App.Current.MainPage = new MainPage();
                    }
                    else
                    {
                        await ServiceMessage.DefaultServicio.ShowMessage("Correo o Contraseña Incorrectos", "Iniciar Sesión");

                    }
                }
                else if(res != null && !res.status)
                {
                    await ServiceMessage.DefaultServicio.ShowMessage(res.message, "Iniciar Sesión");
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

        #endregion

        #region Metodos

        #endregion

    }
}
