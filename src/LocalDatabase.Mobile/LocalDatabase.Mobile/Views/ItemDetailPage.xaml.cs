using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LocalDatabase.Mobile.Models;
using LocalDatabase.Mobile.ViewModels;

namespace LocalDatabase.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemDetailPage : ContentPage
    {
        ItemDetailViewModel viewModel;

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.viewModel.Navigation = Navigation;
            BindingContext = this.viewModel;
        }

        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new Contact();

            viewModel = new ItemDetailViewModel(item);
            BindingContext = viewModel;
        }

        private async void SaveItem_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "SaveItem", viewModel.Contact);
            await Navigation.PopAsync(false);
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewTransactionPage(new NewTransactionViewModel(viewModel.Contact)));
        }
    }
}