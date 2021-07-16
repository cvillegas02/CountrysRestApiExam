using AppCountrys.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountrys.Localizador
{
    /// <summary>
    /// Clase singleton Localizador de las clases MVVM
    /// </summary>
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


        DetailCountryViewModel detailCountryVM { get; set; }
        public DetailCountryViewModel _detailCountryVM
        {
            get
            {
                if (detailCountryVM == null)
                {
                    detailCountryVM = new DetailCountryViewModel();

                }
                return detailCountryVM;
            }
            set
            {
                detailCountryVM = value;
            }
        }

        CountriesViewModel countriesVM { get; set; }
        public CountriesViewModel _countriesVM
        {
            get
            {
                if (countriesVM == null)
                {
                    countriesVM = new CountriesViewModel();

                }
                return countriesVM;
            }
            set
            {
                countriesVM = value;
            }
        }
        
    }
}
