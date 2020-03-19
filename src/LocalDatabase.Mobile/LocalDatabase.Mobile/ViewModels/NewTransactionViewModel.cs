using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Mobile.Models;
using LocalDatabase.Mobile.Utilities;

namespace LocalDatabase.Mobile.ViewModels
{
    public class NewTransactionViewModel : BaseViewModel
    {
        private Contact _contact;

        public Contact Contact
        {
            get { return _contact; }
            set { _contact = value; OnPropertyChanged("Contact"); }
        }

        private Transaction _item;
        public Transaction Item
        {
            get { return _item;}
            set { _item = value; OnPropertyChanged("Item"); }
        }

        public  void OnItemChanged()
        {
            OnPropertyChanged("Item");
        }

        public string[] TransactionTypes
        {
            get { return Enum.GetNames(typeof(TransactionType)); }
        }

        public string[] Categories
        {
            get { return Enum.GetNames(typeof(Category)); }
        }

        public string[] Vendors
        {
            get { return Enum.GetNames(typeof(VendorType)); }
        }

        public NewTransactionViewModel(Contact contact)
        {
            Item = new Transaction(){Created = DateTime.Now};
            Contact = contact;
            Title = $"New transaction";
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
                            Item.Photo = memoryStream.ToArray();
                            OnPropertyChanged("Item");
                        }
                    }
                });
            }
        }
    }
}