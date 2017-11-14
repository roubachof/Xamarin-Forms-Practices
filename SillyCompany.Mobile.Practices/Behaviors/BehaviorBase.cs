// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BehaviorBase.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   https://github.com/davidbritch/xamarin-forms/blob/master/ItemSelectedBehavior/ItemSelectedBehavior/Behaviors/BehaviorBase.cs
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SillyCompany.Mobile.Practices.Behaviors
{
    using System;

    using Xamarin.Forms;

    /// <summary>
    /// The behavior base.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class BehaviorBase<T> : Behavior<T>
        where T : BindableObject
    {
        /// <summary>
        /// Gets the associated object.
        /// </summary>
        public T AssociatedObject { get; private set; }

        /// <summary>
        /// The on attached to.
        /// </summary>
        /// <param name="bindable">
        /// The bindable.
        /// </param>
        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            this.AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                this.BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += this.OnBindingContextChanged;
        }

        /// <summary>
        /// The on detaching from.
        /// </summary>
        /// <param name="bindable">
        /// The bindable.
        /// </param>
        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= this.OnBindingContextChanged;
            this.AssociatedObject = null;
        }

        /// <summary>
        /// The on binding context changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            this.OnBindingContextChanged();
        }

        /// <summary>
        /// The on binding context changed.
        /// </summary>
        protected override void OnBindingContextChanged()
        {
            this.OnBindingContextChanged();
            this.BindingContext = this.AssociatedObject.BindingContext;
        }
    }
}