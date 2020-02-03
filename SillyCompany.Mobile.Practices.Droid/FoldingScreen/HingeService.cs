using System;
using System.ComponentModel;
using System.Linq;

using Android.App;
using Android.Views;

using Microsoft.Device.Display;

using Xamarin.Duo.Forms.Samples;
using Xamarin.Forms;

[assembly: Dependency(typeof(HingeService))]

namespace Xamarin.Duo.Forms.Samples
{
    public class HingeService : IHingeService, IDisposable
    {
        private static ScreenHelper helper;

        private int _hingeAngle;

        private Rectangle _hingeLocation;

        private readonly HingeSensor hingeSensor;

        private readonly bool isDuo;

        public HingeService()
        {
            if (helper == null)
            {
                helper = new ScreenHelper();
            }

            this.isDuo = helper.Initialize(MainActivity);

            if (this.isDuo)
            {
                this.hingeSensor = new HingeSensor(MainActivity);
                this.hingeSensor.OnSensorChanged += this.OnSensorChanged;
                this.hingeSensor.StartListening();
            }
        }

        public static Activity MainActivity { get; set; }

        private ILayoutService LayoutService => DependencyService.Get<ILayoutService>();

        public void Dispose()
        {
            if (this.hingeSensor != null)
            {
                this.hingeSensor.OnSensorChanged -= this.OnSensorChanged;
                this.hingeSensor.StopListening();
            }
        }

        public bool IsSpanned => this.isDuo && (helper?.IsDualMode ?? false);

        public Rectangle GetHinge()
        {
            if (!this.isDuo || helper == null)
            {
                return Rectangle.Zero;
            }

            var rotation = ScreenHelper.GetRotation(helper.Activity);
            var hinge = helper.DisplayMask.GetBoundingRectsForRotation(rotation).FirstOrDefault();
            var hingeDp = new Rectangle(
                this.PixelsToDp(hinge.Left),
                this.PixelsToDp(hinge.Top),
                this.PixelsToDp(hinge.Width()),
                this.PixelsToDp(hinge.Height()));

            return hingeDp;
        }

        public bool IsLandscape
        {
            get
            {
                if (!this.isDuo || helper == null)
                {
                    return false;
                }

                var rotation = ScreenHelper.GetRotation(helper.Activity);

                return rotation == SurfaceOrientation.Rotation270 || rotation == SurfaceOrientation.Rotation90;
            }
        }

        public event EventHandler<HingeEventArgs> OnHingeUpdated;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnSensorChanged(object sender, HingeSensor.HingeSensorChangedEventArgs e)
        {
            if (this._hingeLocation != this.GetHinge())
            {
                this._hingeLocation = this.GetHinge();
                this.LayoutService.AddLayoutGuide("Hinge", this._hingeLocation);
            }

            if (this._hingeAngle != e.HingeAngle)
            {
                this.OnHingeUpdated?.Invoke(this, new HingeEventArgs(e.HingeAngle));
            }

            this._hingeAngle = e.HingeAngle;
        }

        private double PixelsToDp(double px)
        {
            return px / MainActivity.Resources.DisplayMetrics.Density;
        }
    }
}