using System;

using Android.App;
using Android.OS;
using Android.Runtime;
using SillyCompany.Mobile.Practices.Infrastructure;

namespace SillyCompany.Mobile.Practices.Droid
{
    [Application]
    public class SillyApplication : Application, Application.IActivityLifecycleCallbacks
    {
        public SillyApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            PlatformService.Initialize(
                Resources.DisplayMetrics.Density,
                (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density),
                (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density));

            RegisterActivityLifecycleCallbacks(this);
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
            UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
        }

        public void OnActivityStopped(Activity activity)
        {
        }
    }
}