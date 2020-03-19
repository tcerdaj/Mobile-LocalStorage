using System;
using System.Collections.Generic;
using System.ComponentModel;
using LocalDatabase.Mobile.Models;
using Xamarin.Forms;
using LocalDatabase.Mobile.ViewModels;

namespace LocalDatabase.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ChartPage : ContentPage
    {
       private ChartViewModel viewModel;

        public ChartPage(List<Transaction> transactions)
        {
            InitializeComponent();

            viewModel = new ChartViewModel(transactions);

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