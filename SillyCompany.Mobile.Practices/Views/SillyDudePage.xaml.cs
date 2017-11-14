// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyDudePage.xaml.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The silly dude page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SillyCompany.Mobile.Practices.Views
{
    using System;

    using Xamarin.Forms;

    /// <summary>
    /// The silly dude page.
    /// </summary>
    public partial class SillyDudePage : ContentPage, IBindablePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SillyDudePage"/> class.
        /// </summary>
        public SillyDudePage()
        {
            this.InitializeComponent();
        }
    }
}