using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using LocalDatabase.Mobile.Models;
using LocalDatabase.Mobile.Utilities;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace LocalDatabase.Mobile.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private Contact _item;
        public Contact Item
        {
            get { return _item;}
            set { _item = value; OnPropertyChanged("Item"); }
        }
        public NewItemViewModel()
        {
            Item = new Contact();
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