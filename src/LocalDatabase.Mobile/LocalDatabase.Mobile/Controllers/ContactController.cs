using LocalDatabase.Mobile.Models;
using LocalDatabase.Mobile.Services;
using Xamarin.Forms;

namespace LocalDatabase.Mobile.Controllers
{
    public class ContactController
    {
        static ILocalDataStore<Contact> _localDataStore => DependencyService.Get<ILocalDataStore<Contact>>();

        public static ILocalDataStore<Contact> LocalData
        {
            get { return _localDataStore; }
        }
    }
}
