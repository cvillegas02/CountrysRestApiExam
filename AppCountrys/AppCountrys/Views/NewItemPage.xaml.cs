using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AppCountrys.Models;

namespace AppCountrys.Views
{
   
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
       

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = Localizador.LocalizadorInstancia.DefaultServicio._newCountryVM;
        }

        
    }
}