using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppCountrys.Models;
using AppCountrys.ViewModels;
using AppCountrys.Models.ModelsDB;
using AppCountrys.Localizador;

namespace AppCountrys.Views
{
    [DesignTimeVisible(false)]
    public partial class DetailCountryPage : ContentPage
    {
      
        public DetailCountryPage()
        {
            InitializeComponent();
            BindingContext = LocalizadorInstancia.DefaultServicio._detailCountryVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Localizador.LocalizadorInstancia.DefaultServicio._detailCountryVM.ExecuteLoadItemsCommand();
        }

    }
}