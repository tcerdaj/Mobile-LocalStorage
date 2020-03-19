using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Mobile.Models;
using Microcharts;
using SkiaSharp;
using Entry = Microcharts.Entry;

namespace LocalDatabase.Mobile.ViewModels
{
    public class ChartViewModel : BaseViewModel
    {

        private Chart _expensesByDay;
        List<string> randomList = new List<string>();

        public Chart ExpensesByDay
        {
            get { return _expensesByDay; }
            set { _expensesByDay = value; OnPropertyChanged("ExpensesByDay"); }
        }

        private Chart _expensesByMonth;

        public Chart ExpensesByMonth
        {
            get { return _expensesByMonth; }
            set { _expensesByMonth = value; OnPropertyChanged("ExpensesByMonth"); }
        }

        private Chart _expensesByCategory;

        public Chart ExpensesByCategory
        {
            get { return _expensesByCategory; }
            set { _expensesByCategory = value; OnPropertyChanged("ExpensesByCategory"); }
        }

        private List<Transaction> _transactions;
        public List<Transaction> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; OnPropertyChanged("Transactions"); }
        }

        public ChartViewModel(List<Transaction> transactions)
        {
            Title = $"Chart transactions";
            Transactions = transactions;

            if (Transactions != null)
            {
                //transaction by day
                System.DateTime startDate = new System.DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                var tranByDayEntries = Transactions
                    .Where(x => x.Created >= startDate && x.Created <= endDate && x.TransactionType == TransactionType.Expense)
                    .GroupBy(d => d.Created.Day)
                    .OrderBy(x=>x.Key)
                    .Select(x => new Entry(x.Sum(s => (float)s.Amount))
                    {
                        Label = x.Key.ToString(),
                        ValueLabel = x.Sum(s =>s.Amount).ToString(),
                        Color = SKColor.Parse("#FF1493")

                    }).ToList();

                ExpensesByDay = new LineChart(){Entries = tranByDayEntries, LabelTextSize = 20f, LineSize = 8 };

                startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                endDate = new DateTime(DateTime.Now.Year, 12, 31);

                var tranByMonthEntries = Transactions
                    .Where(x => x.Created >= startDate && x.Created <= endDate && x.TransactionType == TransactionType.Expense)
                    .GroupBy(d => d.Created.ToString("MMMM"))
                    .OrderBy(x=>x.First().Created.Month)
                    .Select(x => new Entry(x.Sum(s => (float)s.Amount))
                    {
                        Label = x.Key.ToString(),
                        ValueLabel = x.Sum(s => s.Amount).ToString(),
                        Color = SKColor.Parse("#0000ff")

                    }).ToList();

                ExpensesByMonth = new LineChart() { Entries = tranByMonthEntries, LabelTextSize = 20f, LineSize = 8};

                var tranByCategoryEntries = Transactions
                    .Where(x => x.Created >= startDate && x.Created <= endDate && x.TransactionType == TransactionType.Expense)
                    .GroupBy(d => d.Category)
                    .Select(x => new Entry(x.Sum(s => (float)s.Amount))
                    {
                        Label = x.Key.ToString(),
                        ValueLabel = x.Sum(s => s.Amount).ToString(),
                        Color = SKColor.Parse(GetColor())

                    }).OrderBy(x=>x.Label).ToList();
                
                ExpensesByCategory = new DonutChart(){ Entries = tranByCategoryEntries, LabelTextSize = 20f};
            }
        }

        private string GetColor()
        {
            var random = new Random();
            var color = String.Format("#{0:X6}", random.Next(0x1000000));
            randomList.Add(color);

            if (randomList.Count > 1)
            {
                while (randomList.Contains(color))
                {
                    color = String.Format("#{0:X6}", random.Next(0x1000000));
                }
            }

            return color;
        }
    }
}