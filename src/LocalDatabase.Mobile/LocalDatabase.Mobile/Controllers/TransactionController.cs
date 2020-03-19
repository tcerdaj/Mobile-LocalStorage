using LocalDatabase.Mobile.Models;
using LocalDatabase.Mobile.Services;
using Xamarin.Forms;

namespace LocalDatabase.Mobile.Controllers
{
    public class TransactionController
    {
        static ILocalDataStore<Transaction> _localDataStore => DependencyService.Get<ILocalDataStore<Transaction>>();

        public static ILocalDataStore<Transaction> LocalData
        {
            get { return _localDataStore; }
        }
    }
}
