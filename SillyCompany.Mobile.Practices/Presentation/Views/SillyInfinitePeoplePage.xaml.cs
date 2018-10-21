// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyPeoplePage.xaml.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The silly people page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using SillyCompany.Mobile.Practices.Presentation.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyInfinitePeoplePage : ContentPage, IBindablePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SillyPeoplePage"/> class.
        /// </summary>
        public SillyInfinitePeoplePage()
        {
            InitializeComponent();

            TaskLoaderHeader.Height = SizeScalingByScreenConverter.Instance.Convert(TaskLoaderHeader.Height.Value);
            SillyOfTheDayHeader.Height = SizeScalingByScreenConverter.Instance.Convert(SillyOfTheDayHeader.Height.Value);
            ListViewHeader.Height = SizeScalingByScreenConverter.Instance.Convert(ListViewHeader.Height.Value);
        }
    }
}