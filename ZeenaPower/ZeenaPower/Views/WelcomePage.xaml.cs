using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeenaPower.Helper;

namespace ZeenaPower.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            //storage pull
            if (await new PermissionCheck().CheckStorageAccess())
            {
                StorageHelper.RetrieveStorageObject();
                if (StorageHelper.StoredShared != null)
                {            //checked if logged in
                    if (StorageHelper.StoredShared.LoggedInModel != null && StorageHelper.StoredShared.LoggedInModel.IsLoggedIn)
                    {
                        App.LoggedInModel = StorageHelper.StoredShared.LoggedInModel;
                        await DisplayAlert("Zeena Power", "Welcome Back " + App.LoggedInModel.user.first_name, "OK");
                        App.Current.MainPage = new HomeMasterDetailPage();
                    }
                }
                else
                {
                    StorageHelper.StoredShared = new Shared
                    {
                        LoggedInModel = new Models.LoginResponseModel { IsLoggedIn = false }
                    };
                    App.LoggedInModel = new Models.LoginResponseModel { IsLoggedIn = false };
                    StorageHelper.SaveStorageObjectToFile();
                }
            }

            Task.Run(async () =>
               {
                   await new PermissionCheck().CheckLocationAccess();
                   if (StorageHelper.StoredShared.SavedLatitude != 0)
                   {

                       await App.RestfulService.PostData(StorageHelper.StoredShared.IPAddress, StorageHelper.StoredShared.DeviceId,
                                   StorageHelper.StoredShared.SavedLongitude.ToString(), StorageHelper.StoredShared.SavedLatitude.ToString());

                   }
               });
        }
        private void signInBtn_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private void signUpLabel_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new RegisterPage());
        }
    }
}