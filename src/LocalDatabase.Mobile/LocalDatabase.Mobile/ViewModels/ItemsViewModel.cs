using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using LocalDatabase.Mobile.Controllers;
using Xamarin.Forms;

using LocalDatabase.Mobile.Models;
using LocalDatabase.Mobile.Views;
using GalaSoft.MvvmLight.Command;

namespace LocalDatabase.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private ObservableCollection<Contact> _items;
        public ObservableCollection<Contact> Items
        {
            get { return _items;}
            set { _items = value;OnPropertyChanged("Items"); }
        }

        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Contacts";
            Items = new ObservableCollection<Contact>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Contact>(this, "AddItem", async (obj, item) =>
            {
                try
                {
                    var newItem = item as Contact;
                    Items.Add(newItem);
                    await ContactController.LocalData.Modify(newItem);
                    Items = new ObservableCollection<Contact>(Items);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.Alert($"Unable to Save contact, {e.Message}", "Save Contact");
                }
                
                IsBusy = false;
            });

            MessagingCenter.Subscribe<ItemDetailPage, Contact>(this, "SaveItem", async (obj, item) =>
            {
                if (IsBusy)
                    return;
                
                IsBusy = true;

                try
                {
                    var newItem = item as Contact;
                    await ContactController.LocalData.Modify(newItem);
                }
                catch (Exception e)
                {
                    UserDialogs.Instance.Alert($"Unable to Save contact, {e.Message}", "Save Contact");
                }
                finally
                {
                    IsBusy = false;
                    await ExecuteLoadItemsCommand();
                }
            });
        }

        public INavigation Navigation { get; set; }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await ContactController.LocalData.List();
                Items = new ObservableCollection<Contact>(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new RelayCommand<Contact>(async (contact) =>
                {
                    if (await UserDialogs.Instance.ConfirmAsync($"Are you sure want to delete {contact?.Name} contact?",
                        "Delete contact", "Yes", "Cancel"))
                    {
                        try
                        {
                            await ContactController.LocalData.Delete(contact);
                            await ExecuteLoadItemsCommand();
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
                return new RelayCommand<object>(async (sourceObject) =>
                {
                    if (IsBusy) return;
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

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}