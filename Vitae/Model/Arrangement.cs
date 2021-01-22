using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Vitae
{
	class Arrangement:INotifyPropertyChanged
    {
		public Arrangement()
		{

		}
		public Arrangement(double _scrollViewer_Height)
		{
			Image_Height = _scrollViewer_Height;
		}
        private double image_Height =200;

		

		public double Image_Height
		{
			get { return image_Height; }
			set 
			{ 
				image_Height = value; 
				NotifyPropertyChandged("Image_Height"); 
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChandged([CallerMemberName]string prop_name="")
		{
			if (this.PropertyChanged != null)
				this.PropertyChanged(this, new PropertyChangedEventArgs(prop_name));
		}
	}
}
