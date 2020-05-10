using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeenaPower.Helper;
using ZeenaPower.Services;

namespace ZeenaPower.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void loginBtn_Clicked(object sender, EventArgs e)
        {
            loader.IsVisible = true;
            var user = usernameEntry.Text;
            var pass = passwordEntry.Text;
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
            {
               await DisplayAlert("Login", "All fields are required", "OK");
            }
            else
            {
                var response = await App.RestfulService.LoginPost(user, pass);
                if (response.status == "success")
                {
                    StorageHelper.StoredShared.LoggedInModel = response;
                    StorageHelper.SaveStorageObjectToFile();
                    App.LoggedInModel = response;
                    App.Current.MainPage = new HomeMasterDetailPage();
                }
                else
                {
                    await DisplayAlert("Login", response.status, "OK");
                }
            }
            loader.IsVisible = false;
        }

        private void signUpLabel_Tapped(object sender, EventArgs e)
        {
            if (Navigation.ModalStack.Count == 0)
                Navigation.PushModalAsync(new RegisterPage());
            else
                Navigation.PopModalAsync();

        }
    }
}