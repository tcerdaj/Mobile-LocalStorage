
namespace LocalDatabase.Mobile.ViewModels
{
    public class DisplayImageViewModel: BaseViewModel
    {
        private byte[] _photo;

        public byte[] Photo
        {
            get { return _photo; }
            set { _photo = value; OnPropertyChanged("Photo"); }
        }

        public DisplayImageViewModel(byte[] photo)
        {
            Photo = photo;
        }
    }
}