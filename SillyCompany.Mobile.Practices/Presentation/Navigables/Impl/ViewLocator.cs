// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewLocator.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SillyCompany.Mobile.Practices.Presentation.ViewModels;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.TabsLayout;
using SillyCompany.Mobile.Practices.Presentation.Views;
using SillyCompany.Mobile.Practices.Presentation.Views.TabsLayout;

namespace SillyCompany.Mobile.Practices.Presentation.Navigables.Impl
{
    public class ViewLocator : IViewLocator
    {
        private static readonly Dictionary<string, Type> ViewLocatorDictionary = new Dictionary<string, Type>
            {
                { nameof(SillyPeopleVm), typeof(SillyPeoplePage) },
                { nameof(SillyDudeVm), typeof(SillyDudePage) },
                { nameof(SortSillyPeopleVm), typeof(SortSillyPeoplePage) },
                { nameof(SillyInfinitePeopleVm), typeof(SillyInfinitePeoplePage) },
                { nameof(SillyInfiniteGridPeopleVm), typeof(SillyInfiniteGridPeoplePage) },
                { nameof(SillyBottomTabsPageViewModel), typeof(SillyBottomTabsPage) },
            };

        public IBindablePage GetViewFor<TViewModel>()
            where TViewModel : ANavigableViewModel
        {
            var viewModel = DependencyContainer.Instance.GetInstance<TViewModel>();
            var view =
                (IBindablePage)DependencyContainer.Instance.GetInstance(ViewLocatorDictionary[typeof(TViewModel).Name]);
            view.BindingContext = viewModel;
            return view;
        }

        public IBindablePage GetViewFor<TViewModel>(TViewModel viewModel, NavigationTransition transition)
            where TViewModel : ANavigableViewModel
        {
            var view =
                (IBindablePage)DependencyContainer.Instance.GetInstance(
                    ViewLocatorDictionary[$"{viewModel.GetType().Name}+{transition}"]);
            view.BindingContext = viewModel;
            return view;
        }

        /// <summary>
        /// Gets the view type matching the given view model.
        /// </summary>
        /// <typeparam name="TViewModel">
        /// The view model type.
        /// </typeparam>
        /// <returns>
        /// </returns>
        public Type GetViewTypeFor<TViewModel>()
            where TViewModel : ANavigableViewModel
        {
            return ViewLocatorDictionary[typeof(TViewModel).Name];
        }

        /// <summary>
        /// Gets the view type matching the given view model and transition.
        /// </summary>
        /// <param name="viewModel">
        /// </param>
        /// <param name="transition">
        /// </param>
        /// <typeparam name="TViewModel">
        /// The view model type.
        /// </typeparam>
        /// <returns>
        /// </returns>
        public Type GetViewTypeFor<TViewModel>(TViewModel viewModel, NavigationTransition transition)
            where TViewModel : ANavigableViewModel
        {
            return ViewLocatorDictionary[$"{viewModel.GetType().Name}+{transition}"];
        }
    }
}