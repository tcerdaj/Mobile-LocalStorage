using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using LocalDatabase.Mobile.Models;
using Xamarin.Forms;

namespace LocalDatabase.Mobile.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var transaction = value as Transaction;

            if (value == null || (transaction != null && transaction.Photo == null))
            {
                return ImageSource.FromFile("imageplaceholder.png");
            }

            if (transaction != null && transaction.Photo != null)
            {
                string _fileName = $"{transaction.Id}.png";
                var fullFileName =
                    Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), _fileName);
                
                if (!File.Exists(fullFileName))
                {
                    File.WriteAllBytes(fullFileName, transaction.Photo);
                }

                return ImageSource.FromFile(fullFileName);
            }

            var bytes = value as Byte[];

            if (bytes == null)
            {
                return ImageSource.FromFile("imageplaceholder.png");
            }

            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public string GetSizeInMemory(long bytesize)
        {


            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len =  System.Convert.ToDouble(bytesize);
            int order = 0;
            while (len >= 1024D && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            return string.Format(CultureInfo.CurrentCulture, "{0:0.##} {1}", len, sizes[order]);
        }
    }
}
