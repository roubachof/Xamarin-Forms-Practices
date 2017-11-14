// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FormsNavigationService.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The forms navigation service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SillyCompany.Mobile.Practices.Services.Navigables.Impl
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using SillyCompany.Mobile.Practices.ViewModels;
    using SillyCompany.Mobile.Practices.Views;

    using Xamarin.Forms;

    /// <summary>
    /// The forms navigation service.
    /// </summary>
    public class FormsNavigationService : INavigationService
    {
        /// <summary>
        /// The lazy forms navigation.
        /// </summary>
        private readonly Lazy<NavigationPage> lazyFormsNavigation;

        /// <summary>
        /// The view locator.
        /// </summary>
        private readonly IViewLocator viewLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormsNavigationService"/> class.
        /// </summary>
        /// <param name="lazyFormsNavigation">
        /// The lazy forms navigation.
        /// </param>
        /// <param name="viewLocator">
        /// The view locator.
        /// </param>
        public FormsNavigationService(Lazy<NavigationPage> lazyFormsNavigation, IViewLocator viewLocator)
        {
            this.lazyFormsNavigation = lazyFormsNavigation;
            this.viewLocator = viewLocator;
        }

        /// <summary>
        /// The navigation page.
        /// </summary>
        private NavigationPage NavigationPage => this.lazyFormsNavigation.Value;

        /// <summary>
        /// The forms navigation.
        /// </summary>
        private INavigation FormsNavigation => this.lazyFormsNavigation.Value.Navigation;
  
        public async Task NavigateToAsync<TViewModel>(
            object parameter = null, 
            bool clearStack = false, 
            bool animated = true) where TViewModel : ANavigableViewModel
        {
            if (clearStack)
            {
                var viewType = this.viewLocator.GetViewTypeFor<TViewModel>();
                var rootPage = this.FormsNavigation.NavigationStack.First();
                if (viewType != rootPage.GetType())
                {
                    var newRootView = (Page)this.viewLocator.GetViewFor<TViewModel>();

                    // Make the new view the root of our navigation stack
                    this.FormsNavigation.InsertPageBefore(newRootView, rootPage);
                    rootPage = newRootView;
                }

                // Then we want to go back to root page and clear the stack
                await this.NavigationPage.PopToRootAsync(animated);
                ((ANavigableViewModel)rootPage.BindingContext).Load(parameter);
                return;
            }

            var view = this.viewLocator.GetViewFor<TViewModel>();
            await this.NavigationPage.PushAsync((Page)view, animated);
            ((ANavigableViewModel)view.BindingContext).Load(parameter);
        }
    
        public async Task NavigateToAsync<TViewModel>(
            TViewModel viewModel, 
            NavigationTransition transition, 
            bool rootChild = false) where TViewModel : ANavigableViewModel
        {
            var view = this.viewLocator.GetViewFor(viewModel, transition);
            await this.NavigationPage.PushAsync((Page)view, true);

            if (rootChild)
            {
                foreach (
                    var page in
                        this.FormsNavigation.NavigationStack.Take(this.FormsNavigation.NavigationStack.Count - 1)
                            .Skip(1))
                {
                    this.FormsNavigation.RemovePage(page);
                }
            }
        }
  
        public async Task NavigateFromMenuToAsync<TViewModel>() where TViewModel : ANavigableViewModel
        {
            var view = this.viewLocator.GetViewFor<TViewModel>();
            await this.NavigationPage.PushAsync((Page)view);
            ((ANavigableViewModel)view.BindingContext).Load(null);

            foreach (
                var page in
                    this.FormsNavigation.NavigationStack.Take(this.FormsNavigation.NavigationStack.Count - 1).Skip(1))
            {
                this.FormsNavigation.RemovePage(page);
            }
        }
  
        public async Task<IBindablePage> NavigateBackAsync()
        {
            return (IBindablePage)await this.NavigationPage.PopAsync();
        }
    }
}