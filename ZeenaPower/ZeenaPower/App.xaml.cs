using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeenaPower.Helper;
using ZeenaPower.Models;
using ZeenaPower.Services;
using ZeenaPower.Views;

namespace ZeenaPower
{
    public partial class App : Application
    {
        public static Restful RestfulService { get; set; }
        public static LoginResponseModel LoggedInModel { get; set; }
        public App()
        {
            InitializeComponent();
            RestfulService = new Restful();
            MainPage = new WelcomePage();
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
