using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sharpnado.Presentation.Forms.CustomViews;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views.DudeDetails
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Meme : ContentView, IAnimatableReveal
	{
		public Meme ()
		{
			InitializeComponent ();
		}

        public bool Animate { get; set; }
    }
}