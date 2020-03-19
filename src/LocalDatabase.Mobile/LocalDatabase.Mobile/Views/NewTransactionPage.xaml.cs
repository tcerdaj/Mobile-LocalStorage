using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using LocalDatabase.Mobile.Models;
using LocalDatabase.Mobile.ViewModels;
using MR.Gestures;
using ContentPage = Xamarin.Forms.ContentPage;
using Picker = MR.Gestures.Picker;

namespace LocalDatabase.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewTransactionPage : ContentPage
    {
        private NewTransactionViewModel viewModel;

        public NewTransactionPage(NewTransactionViewModel _viewModel)
        {
            InitializeComponent();

            viewModel = _viewModel;
            

            BindingContext = viewModel;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddTransaction", viewModel.Item);
            await Navigation.PopAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void TransactionType_OnTapped(object sender, TapEventArgs e)
        {
            transactionTypePicker.Focus();
            transactionTypePicker.SelectedIndexChanged += TransactionTypePicker_SelectedIndexChanged;
            transactionTypePicker.Unfocused -= TransactionTypePicker_Unfocused;
            transactionTypePicker.Unfocused += TransactionTypePicker_Unfocused;
        }

        private void TransactionTypePicker_Unfocused(object sender, FocusEventArgs e)
        {
            transactionTypePicker.SelectedIndexChanged -= TransactionTypePicker_SelectedIndexChanged;
        }

        private void TransactionTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Xamarin.Forms.Picker;
            
            if (picker != null)
            {
                try
                {
                    var _enum = (TransactionType)Enum.Parse(typeof(TransactionType), picker.SelectedItem.ToString());
                    viewModel.Item.TransactionType = _enum;
                    viewModel.OnItemChanged();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

        private void Category_OnTapped(object sender, TapEventArgs e)
        {
            categoryPicker.Focus();
            categoryPicker.SelectedIndexChanged += CategoryPicker_SelectedIndexChanged;
            categoryPicker.Unfocused -= CategoryPicker_Unfocused;
            categoryPicker.Unfocused += CategoryPicker_Unfocused;
        }

        private void CategoryPicker_Unfocused(object sender, FocusEventArgs e)
        {
            categoryPicker.SelectedIndexChanged -= CategoryPicker_SelectedIndexChanged;
        }

        private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Xamarin.Forms.Picker;

            if (picker != null)
            {
                try
                {
                    var _enum = (Category)Enum.Parse(typeof(Category), picker.SelectedItem.ToString());
                    viewModel.Item.Category = _enum;
                    viewModel.OnItemChanged();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

        private void VendorPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Xamarin.Forms.Picker;

            if (picker != null)
            {
                try
                {
                    var _enum = (VendorType)Enum.Parse(typeof(VendorType), picker.SelectedItem.ToString());
                    viewModel.Item.Vendor = _enum;
                    viewModel.OnItemChanged();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }

        private void VendorPicker_Unfocused(object sender, FocusEventArgs e)
        {
            vendorPicker.SelectedIndexChanged -= VendorPicker_SelectedIndexChanged;
        }

        private void Vendor_OnTapped(object sender, TapEventArgs e)
        {
            vendorPicker.Focus();
            vendorPicker.SelectedIndexChanged += VendorPicker_SelectedIndexChanged;
            vendorPicker.Unfocused -= VendorPicker_Unfocused;
            vendorPicker.Unfocused += VendorPicker_Unfocused;
        }

        private void TransactionDate_OnTapped(object sender, TapEventArgs e)
        {
            transactionDatePicker.Focus();
            transactionDatePicker.DateSelected += TransactionDatePicker_DateSelected;
            transactionDatePicker.Unfocused -= TransactionDatePicker_Unfocused;
            transactionDatePicker.Unfocused += TransactionDatePicker_Unfocused;
        }

        private void TransactionDatePicker_Unfocused(object sender, FocusEventArgs e)
        {
            transactionDatePicker.DateSelected -= TransactionDatePicker_DateSelected;
        }

        private void TransactionDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            var picker = sender as Xamarin.Forms.DatePicker;

            if (picker != null)
            {
                try
                {
                    viewModel.Item.Created = picker.Date;
                    viewModel.OnItemChanged();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }
        }
    }
}