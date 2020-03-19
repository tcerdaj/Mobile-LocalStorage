using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using Xamarin.Forms;
using LocalDatabase.Mobile.FontAwesome;
using LocalDatabase.Mobile.Models;

namespace LocalDatabase.Mobile.Converters
{
	public class IconConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			string result = string.Empty;
			
			if ( value == null)
			{
				return result;
			}

			Category category = (Category)value;

			switch (category)
			{
				case Category.CarGas:
					result = FontAwesomeIcons.Gas_pump;
					break;
				case Category.CarMaintenance:
				case Category.CarMortgage:
					result = FontAwesomeIcons.Car_alt;
					break;
				case Category.Clothing:
					result = FontAwesomeIcons.Tshirt;
					break;
				case Category.CreditCardsPayments:
					result = FontAwesomeIcons.CreditCard;
					break;
				case Category.Groseries:
				case Category.Laundry:
					result = FontAwesomeIcons.ShoppingCart;
					break;
				case Category.HouseImprovement:
				case Category.HouseMortgage:
				case Category.LawnMaintenance:
					result = FontAwesomeIcons.Home;
					break;
				case Category.Internet:
					result = FontAwesomeIcons.ExclamationTriangle;
					break;
				case Category.Phone:
					result = FontAwesomeIcons.Mobile_alt;
					break;
				case Category.Payroll:
				case Category.OtherIncome:
					result = FontAwesomeIcons.Money;
					break;
				case Category.Restaurants:
					result = FontAwesomeIcons.Hamburger;
					break;
				case Category.LoanPayments:
					result = FontAwesomeIcons.Money_check;
					break;
			}

			return result;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
