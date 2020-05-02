using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ZeenaPower.Models;
using ZeenaPower.Services;

namespace ZeenaPower.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private int _activemeter;
        private int _inactivemeter;
        private int _complain;
        private List<Meter> _meterlist;
        Restful WebService;
        private string token;

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public int ActiveMeter
        {
            get
            {
                return _activemeter;
            }
            set
            {
                _activemeter = value;
                OnPropertyChanged();
            }
        }
        public int InActiveMeter
        {
            get
            {
                return _inactivemeter;
            }
            set
            {
                _inactivemeter = value;
                OnPropertyChanged();
            }
        }
        public int Complain
        {
            get
            {
                return _complain;
            }
            set
            {
                _complain = value;
                OnPropertyChanged();
            }
        }

        public List<Meter> MeterList
        {
            get { return _meterlist; }
            set
            {
                _meterlist = value;
                OnPropertyChanged();
            }
        }

        public DashboardViewModel(string tokenvalue)
        {
            WebService = new Restful();
            MeterList = new List<Meter>();
            token = tokenvalue;
            LoadDashboard();
        }
        public void LoadDashboard()
        {
           LoadDashboardAsync();
        }

        private async Task LoadDashboardAsync()
        {
            var m = await WebService.GetAllMeters(token);
            if (m != null)
                MeterList = m;
        }
    }
}
