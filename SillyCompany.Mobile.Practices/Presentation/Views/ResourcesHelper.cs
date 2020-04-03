using System;

using Sharpnado.Presentation.Forms.RenderedViews;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.Views
{
    public static class ResourcesHelper
    {
        public const string DynamicPrimaryTextColor = nameof(DynamicPrimaryTextColor);
        public const string DynamicSecondaryTextColor = nameof(DynamicSecondaryTextColor);

        public const string DynamicNavigationBarColor = nameof(DynamicNavigationBarColor);
        public const string DynamicBackgroundColor = nameof(DynamicBackgroundColor);
        public const string DynamicBarTextColor = nameof(DynamicBarTextColor);

        public const string DynamicTopShadow = nameof(DynamicTopShadow);
        public const string DynamicBottomShadow = nameof(DynamicBottomShadow);

        public const string DynamicHasShadow = nameof(DynamicHasShadow);

        public const string Elevation4dpColor = nameof(Elevation4dpColor);

        public const string DynamicLightThemeColor = nameof(DynamicLightThemeColor);

        public const string DynamicCornerRadius = nameof(DynamicCornerRadius);

        public static T GetResource<T>(string key)
        {
            if (Application.Current.Resources.TryGetValue(key, out var value))
            {
                return (T)value;
            }

            throw new InvalidOperationException($"key {key} not found in the resource dictionary");
        }

        public static Color GetResourceColor(string key)
        {
            if (Application.Current.Resources.TryGetValue(key, out var value))
            {
                return (Color)value;
            }

            throw new InvalidOperationException($"key {key} not found in the resource dictionary");
        }

        public static void SetDynamicResource(string targetResourceName, string sourceResourceName)
        {
            if (!Application.Current.Resources.TryGetValue(sourceResourceName, out var value))
            {
                throw new InvalidOperationException($"key {sourceResourceName} not found in the resource dictionary");
            }

            Application.Current.Resources[targetResourceName] = value;
        }

        public static void SetDynamicResource<T>(string targetResourceName, T value)
        {
            Application.Current.Resources[targetResourceName] = value;
        }

        public static void SetDarkMode()
        {
            MaterialFrame.ChangeGlobalTheme(MaterialFrame.Theme.Dark);

            SetDynamicResource(DynamicNavigationBarColor, "DarkElevation2dp");
            SetDynamicResource(DynamicBarTextColor, "TextPrimaryDarkColor");

            SetDynamicResource(DynamicTopShadow, ShadowType.None);
            SetDynamicResource(DynamicBottomShadow, ShadowType.None);
            SetDynamicResource(DynamicHasShadow, false);

            SetDynamicResource(DynamicPrimaryTextColor, "TextPrimaryDarkColor");
            SetDynamicResource(DynamicSecondaryTextColor, "TextSecondaryDarkColor");

            SetDynamicResource(DynamicBackgroundColor, "DarkSurface");

            SetDynamicResource(Elevation4dpColor, "DarkElevation4dp");

            SetDynamicResource(DynamicCornerRadius, 5);
        }

        public static void SetLightMode(bool isAcrylic)
        {
            MaterialFrame.ChangeGlobalTheme(isAcrylic ? MaterialFrame.Theme.Acrylic : MaterialFrame.Theme.Light);

            SetDynamicResource(DynamicNavigationBarColor, "Accent");
            SetDynamicResource(DynamicBarTextColor, "TextPrimaryDarkColor");

            SetDynamicResource(DynamicTopShadow,  isAcrylic ? ShadowType.AcrylicTop : ShadowType.Top);
            SetDynamicResource(DynamicBottomShadow, ShadowType.Bottom);
            SetDynamicResource(DynamicHasShadow, true);

            SetDynamicResource(DynamicPrimaryTextColor, "TextPrimaryLightColor");
            SetDynamicResource(DynamicSecondaryTextColor, "TextSecondaryLightColor");

            SetDynamicResource(DynamicBackgroundColor, isAcrylic ? "AcrylicSurface" : "LightSurface");

            SetDynamicResource(Elevation4dpColor, isAcrylic ? "AcrylicFrameBackgroundColor" : "OnSurfaceColor");

            SetDynamicResource(DynamicLightThemeColor, isAcrylic ? "AcrylicFrameBackgroundColor" : "OnSurfaceColor");

            SetDynamicResource(DynamicCornerRadius, isAcrylic ? 10 : 5);
        }
    }
}
