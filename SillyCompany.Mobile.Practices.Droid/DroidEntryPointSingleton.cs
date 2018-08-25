// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DroidEntryPointSingleton.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Android.Util;
using ImageCircle.Forms.Plugin.Droid;
using Refractored.XamForms.PullToRefresh.Droid;

namespace SillyCompany.Mobile.Practices.Droid
{
    public static class DroidEntryPointSingleton
    {
        private static readonly CoreEntryPoint EntryPoint = new CoreEntryPoint();

        private static bool _isInitializing;

        private static bool _isInitialized;

        public static async Task EnsureInitialized(Action whenCompleted)
        {
            if (_isInitialized)
            {
                Log.Warn("pocm", "Tried to initialize app while it's already been initialized");
                whenCompleted();
                return;
            }

            if (_isInitializing)
            {
                Log.Warn("pocm", "Tried to initialize app while it's currently initializing");
                return;
            }

            _isInitializing = true;
            try
            {
                await InitializeApp().ConfigureAwait(false);
                _isInitialized = true;
                whenCompleted();
            }
            finally
            {
                _isInitializing = false;
            }
        }

        public static async Task InitializeApp()
        {
            await EntryPoint.RegisterDependenciesAsync().ConfigureAwait(false);
            ImageCircleRenderer.Init();
            PullToRefreshLayoutRenderer.Init();
        }
    }
}