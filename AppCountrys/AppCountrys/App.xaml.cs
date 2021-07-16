using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppCountrys.Services;
using AppCountrys.Views;

namespace AppCountrys
{
    public partial class App : Application
    {

        public static NavigationPage Navigator { get; internal set; }

        public App()
        {
            InitializeComponent();

            ///Podemos injectar servicio Fake
            DependencyService.Register<IRestClient,RestClient>();

            MainPage = Navigator = new NavigationPage(new LoginPage());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
