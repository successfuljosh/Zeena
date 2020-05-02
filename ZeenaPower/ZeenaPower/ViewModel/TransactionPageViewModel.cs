using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ZeenaPower.Models;

namespace ZeenaPower.ViewModel
{
   public class TransactionPageViewModel
    {
        public ObservableCollection<Transaction> TransactionHistoryList { get; set; }
        public TransactionPageViewModel()
        {
            TransactionHistoryList = new ObservableCollection<Transaction>();
            TransactionHistoryList.Add(new Transaction
            {
                RefId = "12SE2",
                MeterId = "MSAH323",
                Amount = 708,
                Status = "Enabled",
                Tariff = "UNIK",
                DateTime = DateTime.Now
            }); TransactionHistoryList.Add(new Transaction
            {
                RefId = "12SE2",
                MeterId = "MSAH323",
                Amount = 708,
                Status = "Enabled",
                Tariff = "UNIK",
                DateTime = DateTime.Now
            }); TransactionHistoryList.Add(new Transaction
            {
                RefId = "12SE2",
                MeterId = "MSAH323",
                Amount = 708,
                Status = "Enabled",
                Tariff = "UNIK",
                DateTime = DateTime.Now
            }); TransactionHistoryList.Add(new Transaction
            {
                RefId = "12SE2",
                MeterId = "MSAH323",
                Amount = 708,
                Status = "Enabled",
                Tariff = "UNIK",
                DateTime = DateTime.Now
            }); TransactionHistoryList.Add(new Transaction
            {
                RefId = "12SE2",
                MeterId = "MSAH323",
                Amount = 708,
                Status = "Enabled",
                Tariff = "UNIK",
                DateTime = DateTime.Now
            });
        }
    }
}
