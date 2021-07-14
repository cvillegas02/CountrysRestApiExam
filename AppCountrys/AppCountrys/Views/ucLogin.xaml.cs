using AppCountrys.Localizador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppCountrys.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ucLogin : ContentPage
    {
        public ucLogin()
        {
            InitializeComponent();
            this.BindingContext = LocalizadorInstancia.DefaultServicio._loginVM;
        }
    }
}