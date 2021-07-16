using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppCountrys.Models;
using AppCountrys.Views;
using AppCountrys.ViewModels;
using AppCountrys.Models.ModelsDB;

namespace AppCountrys.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class CountriesPage : ContentPage
    {
        public CountriesPage()
        {
            InitializeComponent();
            BindingContext = Localizador.LocalizadorInstancia.DefaultServicio._countriesVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Localizador.LocalizadorInstancia.DefaultServicio._countriesVM.ListCountry.Count == 0)
            {
                Localizador.LocalizadorInstancia.DefaultServicio._countriesVM.ExecuteLoadItemsCommand();
            }

        }
    }
}