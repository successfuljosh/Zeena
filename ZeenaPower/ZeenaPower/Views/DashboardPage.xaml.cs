using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeenaPower.Helper;
using ZeenaPower.Models;
using ZeenaPower.ViewModel;
using ZeenaPower.Views.Special;

namespace ZeenaPower.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        DashboardViewModel dvm;
        public DashboardPage()
        {
            InitializeComponent();
            activeMeterLabel.Text = App.LoggedInModel.meter.ToString();
            inactiveMeterLabel.Text = "0";
            complainLabel.Text = App.LoggedInModel.complaint.ToString();

            dvm = new DashboardViewModel(App.LoggedInModel.token);
         }

        private void allMetersBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AllMetersPage());
        }
        protected async override void OnAppearing()
        {
            if (!string.IsNullOrWhiteSpace(StorageHelper.StoredShared.Notice))
            {
                var noticeD = StorageHelper.StoredShared.Notice.Split(';');
                if (noticeD.Length > 1)
                {
                    var title = noticeD[0];
                    var content = noticeD[1];
                   await DisplayAlert(title, content, "OK");
                    StorageHelper.StoredShared.Notice = "";
                    StorageHelper.SaveStorageObjectToFile();
                }
                else
                {
                    await DisplayAlert("App Frozen", "Destructive command 'Msg(Name), Splitter(;)' sent from backend, \nContact developer!", "OK");
                    App.Current.MainPage = new CrashPage();
                }

            }
        }
    }
}