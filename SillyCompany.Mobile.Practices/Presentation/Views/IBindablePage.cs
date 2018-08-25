// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBindablePage.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   Bindable page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SillyCompany.Mobile.Practices.Presentation.Views
{
    /// <summary>
    /// Bindable page.
    /// </summary>
    public interface IBindablePage
    {
        /// <summary>
        /// Gets or sets the binding context.
        /// </summary>
        object BindingContext { get; set; }
    }
}