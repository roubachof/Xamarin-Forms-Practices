using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Infrastructure
{
    public enum ScreenSize
    {
        Regular = 0,
        Small = 1,
        Big = 2,
    }

    public static class PlatformService
    {
        public static double DisplayScaleFactor { get; private set; }

        public static Size MainSize { get; private set; }

        public static ScreenSize ScreenSize
        {
            get
            {
                if (MainSize.Width <= 320)
                {
                    return ScreenSize.Small;
                }

                if (MainSize.Width <= 375)
                {
                    return ScreenSize.Regular;
                }

                return ScreenSize.Big;
            }
        }

        public static void Initialize(double scaleFactor, double width, double height)
        {
            DisplayScaleFactor = scaleFactor;

            if (width > height)
            {
                var temp = width;
                width = height;
                height = temp;
            }

            MainSize = new Size(width, height);
        }

        public static int DpToPixels(int dp) => (int)(DisplayScaleFactor * dp);

        public static int DpToPixels(double dp) => (int)(DisplayScaleFactor * dp);
    }
}