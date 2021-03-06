﻿using System;
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
    public partial class AllMetersPage : ContentPage
    {
        public DashboardViewModel DVM;
        public AllMetersPage()
        {
            InitializeComponent();

            //meterListview.ItemsSource = new List<Meter>
            //{
            //    new Meter{ Id="MET21", Tariff ="AD2", Status="Enabled", StartDate=DateTime.Now},
            //    new Meter{ Id="MET21", Tariff ="AD2", Status="Enabled", StartDate=DateTime.Now},
            //    new Meter{ Id="MET21", Tariff ="AD2", Status="Enabled", StartDate=DateTime.Now},
            //    new Meter{ Id="MET21", Tariff ="AD2", Status="Enabled", StartDate=DateTime.Now},
            //    new Meter{ Id="MET21", Tariff ="AD2", Status="Enabled", StartDate=DateTime.Now},
            //    new Meter{ Id="MET21", Tariff ="AD2", Status="Enabled", StartDate=DateTime.Now}
            //};
            //meterListview.ItemsSource = new List<Meter>
            //{
            //    new Meter
            //    {
            //        meter_id = "jdhfdff",
            //        connectivity = false,
            //        house_number = "12",
            //        street_name = "dsjhfn"
            //    }
            //};
            DVM = new DashboardViewModel(App.LoggedInModel.token);
            meterListview.SetBinding(ListView.ItemsSourceProperty, "MeterList");
            meterListview.SetBinding(ListView.IsRefreshingProperty, "IsLoading");
            meterListview.BindingContext = DVM;
        }



        private void meterListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Meter;
            if (item == null) return;
            Navigation.PushAsync(new MeterViewPage(item.meter_id) { Title = item.meter_id });
            meterListview.SelectedItem = null;
        }

        private void requestMeterBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RequestMeterPage());
        }
    }
}