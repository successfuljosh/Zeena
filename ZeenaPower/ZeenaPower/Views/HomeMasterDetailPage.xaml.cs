using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZeenaPower.Models;

namespace ZeenaPower.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeMasterDetailPage : MasterDetailPage
    {
        public HomeMasterDetailPage()
        {
            InitializeComponent();
            userLabel.Text = App.LoggedInModel.user.first_name + " " + App.LoggedInModel.user.last_name;
            userImg.Source = "logo.png";
            masterListView.ItemsSource = new ObservableCollection<MenuItemModel>
{
    new MenuItemModel{Id=0,Title="Dashboard", IconSrc="logo.png"},
    new MenuItemModel{Id=1,Title="Transactions", IconSrc="logo.png"},
        new MenuItemModel{Id=1,Title="Request Meter", IconSrc="meter.png"},
    new MenuItemModel{Id=2,Title="Complaints", IconSrc="complain.png"}

};
        }
        private void masterListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuItemModel;
            if (item == null)
                return;
            switch (item.Title)
            {
                case "Dashboard":
                    Detail = new NavigationPage(new DashboardPage());
                    break;
                case "Transaction":
                    Detail = new NavigationPage(new TransactionPage());
                    break;
                case "Complain":
                    Detail = new NavigationPage(new ComplainPage());
                    break;
                case "Request Meter":
                    Detail = new NavigationPage(new RequestMeterPage());
                    break;
                default:
                    break;
            }
            IsPresented = false;
            masterListView.SelectedItem = null;
        }
    }
}