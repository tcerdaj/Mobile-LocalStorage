using System;
using LocalDatabase.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LocalDatabase.Mobile.Services;
using LocalDatabase.Mobile.Views;

namespace LocalDatabase.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            DependencyService.Register<LocalDataStore<Contact>>();
            DependencyService.Register<LocalDataStore<Transaction>>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
