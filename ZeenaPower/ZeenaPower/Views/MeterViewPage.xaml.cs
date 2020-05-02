using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeenaPower.Models;
using ZeenaPower.ViewModel;

namespace ZeenaPower.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MeterViewPage : ContentPage
    {
        MeterViewModel MVM;
        public MeterViewPage(string meterid)
        {
            InitializeComponent();
            MVM = new MeterViewModel(meterid, App.LoggedInModel.token);
            BindingContext = MVM;
            MVM.GetSingleMeter();
        }

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            await MVM.SwitchMeter(e.Value);
        }
        //protected override void OnDisappearing()
        //{
        //    MVM.closeWs();
        //}

    }
}