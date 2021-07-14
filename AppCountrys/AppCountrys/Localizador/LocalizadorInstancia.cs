using AppCountrys.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountrys.Localizador
{
    public class LocalizadorInstancia
    {
        static LocalizadorInstancia defaultInstance = new LocalizadorInstancia();

        public static LocalizadorInstancia DefaultServicio
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

        private LocalizadorInstancia()
        {


        }

        LoginViewModel loginVM { get; set; }
        public LoginViewModel _loginVM
        {
            get
            {
                if (loginVM == null)
                {
                    loginVM = new LoginViewModel();

                }
                return loginVM;
            }
            set
            {
                loginVM = value;
            }
        }

        NewCountryModelView newCountryVM { get; set; }
        public NewCountryModelView _newCountryVM
        {
            get
            {
                if (newCountryVM == null)
                {
                    newCountryVM = new NewCountryModelView();

                }
                return newCountryVM;
            }
            set
            {
                newCountryVM = value;
            }
        }
    }
}
