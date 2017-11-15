// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DroidEntryPointSingleton.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Refractored.XamForms.PullToRefresh.Droid;

namespace SillyCompany.Mobile.Practices.Droid
{
    using System;
    using System.Threading.Tasks;

    using Android.Util;
    
    public static class DroidEntryPointSingleton
    {
        private static readonly CoreEntryPoint EntryPoint = new CoreEntryPoint();

        private static bool isInitializing;
        
        private static bool isInitialized;
        
        public static async Task EnsureInitialized(Action whenCompleted)
        {
            if (isInitialized)
            {
                Log.Warn("pocm", "Tried to initialize app while it's already been initialized");
                whenCompleted();
                return;
            }

            if (isInitializing)
            {
                Log.Warn("pocm", "Tried to initialize app while it's currently initializing");
                return;
            }

            isInitializing = true;
            try
            {
                await InitializeApp().ConfigureAwait(false);
                isInitialized = true;
                whenCompleted();
            }
            finally
            {
                isInitializing = false;
            }
        }

        public static async Task InitializeApp()
        {
            await EntryPoint.RegisterDependenciesAsync().ConfigureAwait(false);
            PullToRefreshLayoutRenderer.Init();
        }
    }
}