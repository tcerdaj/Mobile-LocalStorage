using System;
using System.ComponentModel;
using System.IO;
using LocalDatabase.Mobile.CustomControls;
using Xamarin.Forms;
using LocalDatabase.Mobile.ViewModels;

namespace LocalDatabase.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class DisplayImagePage : ContentPage
    {
        private DisplayImageViewModel viewModel;

        public DisplayImagePage(byte[] photo)
        {
            InitializeComponent();

            viewModel = new DisplayImageViewModel(photo);

            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}