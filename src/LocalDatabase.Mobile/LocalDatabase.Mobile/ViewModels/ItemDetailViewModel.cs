using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Mobile.Controllers;
using LocalDatabase.Mobile.Helpers.SQLite.Models;
using LocalDatabase.Mobile.Models;
using LocalDatabase.Mobile.Utilities;
using LocalDatabase.Mobile.Views;
using Xamarin.Forms;

namespace LocalDatabase.Mobile.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        private Contact _contact;
        public Contact Contact
        {
            get { return _contact;}
            set { _contact = value; OnPropertyChanged("Contact"); }
        }

        private ObservableCollection<Transaction> _transactions;
        public ObservableCollection<Transaction> Transactions
        {
            get 
            {
                return _transactions;
            } 
            set {
                _transactions = value; OnPropertyChanged("Transactions");
            } 
        }

        private void Init()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                Transactions = await GetTransactions(); 
                RefreshBalances();
            });
        }

        private async Task AddRandomTransactions()
        {
            var random = new Random();

            var startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            for (int i = 0; i < 24; i++)
            {
                var day = i + 1;
                var createDate = new DateTime();
                if (i > 0)
                {
                    createDate = startDate.AddDays(day);
                }

                var transaction = new Transaction
                {
                    Description = "Test" + random.Next(1000),
                    Amount = random.Next(100),
                    Category = (Category) random.Next(Enum.GetNames(typeof(Category)).Length),
                    ContactId = Contact.Id,
                    TransactionType = TransactionType.Expense,
                    Created = createDate
                };

                await TransactionController.LocalData.Modify(transaction);
            }
        }

        private void RefreshBalances()
        {
            if (Transactions != null)
            {
                Expenses = Transactions.Where(x => x.TransactionType == TransactionType.Expense)
                    .Sum(s => s.Amount);
                Income = Transactions.Where(x => x.TransactionType == TransactionType.Income)
                    .Sum(s => s.Amount);
            }
        }
    
        private async Task<ObservableCollection<Transaction>> GetTransactions()
        {
            try
            {
                if (Contact != null)
                {
                    DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1),
                        endDate = new DateTime(DateTime.Now.Year, 12, 31);

                    var criteria = new SQLControllerListCriteriaModel
                    {
                        Filter = new List<SQLControllerListFilterField>()
                        {
                            new SQLControllerListFilterField()
                            {
                                FieldName = "ContactId",
                                ValueLBound = Contact.Id.ToString()
                            },
                            new SQLControllerListFilterField()
                            {
                                FieldName = "Created",
                                ValueLBound = startDate.ToString(),
                                ValueUBound = endDate.ToString(),
                                DateKind = SQLControllerListFilterField.DateKindEnum.Localized
                            },
                        },
                        Sort = new List<SQLControllerListSortField>()
                        {
                            new SQLControllerListSortField()
                            {
                                FieldName = "Created",
                                Descending = true
                            }
                        }
                    };

                    return new ObservableCollection<Transaction>(await TransactionController.LocalData.List(criteria));
                }
            }
            catch (Exception e)
            {
                UserDialogs.Instance.Alert($"Unable to get transactions, {e.Message}", "Transactions");
            }

            return null;
        }

        private double _expenses;

        public double Expenses
        {
            get { return _expenses; }
            set { _expenses = value; OnPropertyChanged("Expenses"); OnPropertyChanged("Total"); }
        }

        private double _income;
        public double Income
        {
            get { return _income; }
            set { _income = value; OnPropertyChanged("Income"); OnPropertyChanged("Total"); }
        }

        public double Total
        {
            get { return Income - Expenses; }
        }

        public ItemDetailViewModel(Contact contact = null)
        {
            Title = contact?.Name;
            Contact = contact;
            Init();

            MessagingCenter.Subscribe<NewTransactionPage, Transaction>(this, "AddTransaction", async (obj, item) =>
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                try
                {
                    var newItem = item as Transaction;
                    
                    if (newItem == null) return;
                    
                    if (Transactions == null)
                    {
                        Transactions = await GetTransactions();
                    }

                    newItem.ContactId = Contact.Id;
                    
                    Transactions.Add(newItem);

                    await TransactionController.LocalData.Modify(newItem);

                    OnPropertyChanged("Transactions");
                    
                    RefreshBalances();

                    Transactions = await GetTransactions();

                }
                catch (Exception e)
                {
                    UserDialogs.Instance.Alert($"Unable to save transaction, {e.Message}", "Save transaction");
                }
                finally
                {
                    IsBusy = false;
                }
            });
        }

        public ICommand TakePhotoCommand
        {
            get
            {
                return new RelayCommand(async ()=>
                {
                    var photo = await Utils.TakePhoto();
                    if (photo != null)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            photo.GetStream().CopyTo(memoryStream);
                            photo.Dispose();
                            Contact.Photo = memoryStream.ToArray();
                            OnPropertyChanged("Contact");
                        }
                    }
                });
            }
        }

        public INavigation Navigation { get; set; }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand<Transaction>(async (transaction) =>
                {
                    if (await UserDialogs.Instance.ConfirmAsync($"Are you sure want to delete {transaction?.Description} transaction?",
                        "Delete transaction", "Yes", "Cancel"))
                    {
                        try
                        {
                            await TransactionController.LocalData.Delete(transaction);
                            if (Transactions != null)
                            {
                                Transactions.Remove(transaction);
                                RefreshBalances();
                            }
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine(e);
                            UserDialogs.Instance.Alert("Unable to delete contact", "Delete contact");
                        }
                    }
                });
            }
        }

        public ICommand DisplayImageCommand
        {
            get
            {
                return new RelayCommand<object>(async(sourceObject) =>
                {
                    if(IsBusy) return;
                    IsBusy = true;

                    try
                    {
                        UserDialogs.Instance.ShowLoading("Loading...");
                        var photo = GetPropValue(sourceObject, "Photo") as byte[];
                        await Navigation.PushAsync(new DisplayImagePage(photo));
                    }
                    catch (Exception e)
                    {
                        UserDialogs.Instance.Alert($"Unable to navigate to view image, {e.Message}.", "View Image");
                    }
                    finally
                    {
                        UserDialogs.Instance.HideLoading();
                        IsBusy = false;
                    }
                });
            }
        }

        public ICommand ViewChartCommand
        {
            get 
            { 
                return new RelayCommand(async () =>
                {
                   await Navigation.PushAsync(new ChartPage(Transactions.ToList()));
                });
            }
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}