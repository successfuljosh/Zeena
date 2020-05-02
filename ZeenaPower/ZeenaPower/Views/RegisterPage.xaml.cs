using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZeenaPower.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void registerBtn_Clicked(object sender, EventArgs e)
        {
            loader.IsVisible = true;
            var email = emailEntry.Text;
            var phone = phoneEntry.Text;
            var pass = passwordEntry.Text;
            var confirmPass = confirmPasswordEntry.Text;
            if (confirmPass != pass)
                await DisplayAlert("Register", "Password don't match", "OK");
            else if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(phone))
                await DisplayAlert("Login", "All fields are required", "OK");
            else
            {
                //var response = await App.RestfulService.LoginPost(email, pass);
                //if (response.status == "success")
                //{
                //    App.LoggedInModel = response;
                //    App.Current.MainPage = new HomeMasterDetailPage();
                //}
                //else
                {
             //       await DisplayAlert("Register", response.status, "OK");
                    await DisplayAlert("Register", "Not available", "OK");
                }
            }
            loader.IsVisible = false;
        }

        private void loginLabel_Tapped(object sender, EventArgs e)
        {
            if (Navigation.ModalStack.Count == 0)
                Navigation.PushModalAsync(new LoginPage());
            else
                Navigation.PopModalAsync();
        }
    }
}