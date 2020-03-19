using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace LocalDatabase.Mobile.CustomControls
{
	public class CustomScrollView: ScrollView
	{
		public event EventHandler<PinchGestureRecognizer> Zoom;

		public ScrollOrientation ScrollOrientation
		{
			get => Orientation;
			set
			{
				if(value.Equals(Orientation))
					return;
				Orientation = value;
			}
		}
	}
}