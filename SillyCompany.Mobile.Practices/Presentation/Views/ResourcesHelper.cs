using System;

using Sharpnado.MaterialFrame;
using Sharpnado.Presentation.Forms.RenderedViews;

using SillyCompany.Mobile.Practices.Presentation.Views.TabsLayout;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.Views
{
    public static class ResourcesHelper
    {
        public const string DynamicMaterialTheme = nameof(DynamicMaterialTheme);

        public const string DynamicPrimaryTextColor = nameof(DynamicPrimaryTextColor);
        public const string DynamicSecondaryTextColor = nameof(DynamicSecondaryTextColor);

        public const string DynamicNavigationBarColor = nameof(DynamicNavigationBarColor);
        public const string DynamicBackgroundColor = nameof(DynamicBackgroundColor);

        public const string DynamicDudeBackgroundColor = nameof(DynamicDudeBackgroundColor);

        public const string DynamicBarTextColor = nameof(DynamicBarTextColor);
        public const string DynamicHeaderTextColor = nameof(DynamicHeaderTextColor);

        public const string DynamicTopShadow = nameof(DynamicTopShadow);
        public const string DynamicBottomShadow = nameof(DynamicBottomShadow);

        public const string DynamicHasShadow = nameof(DynamicHasShadow);

        public const string Elevation4dpColor = nameof(Elevation4dpColor);

        public const string DynamicLightThemeColor = nameof(DynamicLightThemeColor);

        public const string DynamicCornerRadius = nameof(DynamicCornerRadius);

        public const string DynamicIsVisible = nameof(DynamicIsVisible);

        public const string DynamicBackgroundImageSource = nameof(DynamicBackgroundImageSource);

        public const string DynamicBlurTheme = nameof(DynamicBlurTheme);
        public const string DynamicBlurStyle = nameof(DynamicBlurStyle);

        public const string DynamicIsTabBlurVisible = nameof(DynamicIsTabBlurVisible);

        public const string DynamicBottomBarBackground = nameof(DynamicBottomBarBackground);

        public const string DynamicBottomTabBlurStyle = nameof(DynamicBottomTabBlurStyle);

        public const string DynamicElevation = nameof(DynamicElevation);

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
            SetDynamicResource(DynamicMaterialTheme, MaterialFrame.Theme.Dark);
            SetDynamicResource(DynamicBlurTheme, MaterialFrame.Theme.Dark);

            SetDynamicResource(DynamicNavigationBarColor, "DarkElevation2dp");

            SetDynamicResource(DynamicBarTextColor, "TextPrimaryDarkColor");
            SetDynamicResource(DynamicHeaderTextColor, "TextPrimaryDarkColor");
            SetDynamicResource(DynamicPrimaryTextColor, "TextPrimaryDarkColor");
            SetDynamicResource(DynamicSecondaryTextColor, "TextSecondaryDarkColor");

            SetDynamicResource(DynamicBackgroundColor, "DarkSurface");
            SetDynamicResource(DynamicDudeBackgroundColor, "DarkSurface");

            SetDynamicResource(Elevation4dpColor, "DarkElevation4dp");

            SetDynamicResource(DynamicElevation, 4);
            SetDynamicResource(DynamicCornerRadius, 5);

            SetDynamicResource(DynamicIsVisible, false);

            SetDynamicResource(DynamicTopShadow, ShadowType.None);
            SetDynamicResource(DynamicBottomShadow, ShadowType.None);
            SetDynamicResource(DynamicHasShadow, false);

            SetDynamicResource(DynamicIsTabBlurVisible, false);
            SetDynamicResource(DynamicBottomBarBackground, "DarkElevation4dp");

            SetDynamicResource(DynamicBackgroundImageSource, new FileImageSource());
        }

        public static void SetLightMode(bool isAcrylic)
        {
            SetDynamicResource(DynamicMaterialTheme, isAcrylic ? MaterialFrame.Theme.Acrylic : MaterialFrame.Theme.Light);
            SetDynamicResource(DynamicBlurTheme, isAcrylic ? MaterialFrame.Theme.Acrylic : MaterialFrame.Theme.Light);

            SetDynamicResource(DynamicNavigationBarColor, "Accent");

            SetDynamicResource(DynamicBarTextColor, "TextPrimaryDarkColor");
            SetDynamicResource(DynamicHeaderTextColor, "TextPrimaryLightColor");
            SetDynamicResource(DynamicPrimaryTextColor, "TextPrimaryLightColor");
            SetDynamicResource(DynamicSecondaryTextColor, "TextSecondaryLightColor");

            SetDynamicResource(DynamicBackgroundColor, isAcrylic ? "AcrylicSurface" : "LightSurface");
            SetDynamicResource(DynamicDudeBackgroundColor, isAcrylic ? "AcrylicSurface" : "LightSurface");

            SetDynamicResource(Elevation4dpColor, isAcrylic ? "AcrylicFrameBackgroundColor" : "OnSurfaceColor");

            SetDynamicResource(DynamicLightThemeColor, isAcrylic ? "AcrylicFrameBackgroundColor" : "OnSurfaceColor");

            SetDynamicResource(DynamicElevation, 4);
            SetDynamicResource(DynamicCornerRadius, isAcrylic ? 10 : 5);

            SetDynamicResource(DynamicIsVisible, false);

            SetDynamicResource(DynamicTopShadow,  isAcrylic ? ShadowType.AcrylicTop : ShadowType.Top);
            SetDynamicResource(DynamicBottomShadow, ShadowType.Bottom);
            SetDynamicResource(DynamicHasShadow, true);

            SetDynamicResource(DynamicIsTabBlurVisible, false);
            SetDynamicResource(DynamicBottomBarBackground, "AcrylicFrameBackgroundColor");

            SetDynamicResource(DynamicBackgroundImageSource, new FileImageSource());
        }

        public static void SetDarkBlur()
        {
            SetDynamicResource(DynamicBackgroundImageSource, new FileImageSource { File = "vista_portrait_2.jpg" });

            SetDynamicResource(DynamicNavigationBarColor, Color.Transparent);

            SetDynamicResource(DynamicBarTextColor, "TextPrimaryDarkColor");
            SetDynamicResource(DynamicHeaderTextColor, "TextPrimaryDarkColor");
            SetDynamicResource(DynamicPrimaryTextColor, "TextPrimaryDarkColor");
            SetDynamicResource(DynamicSecondaryTextColor, "TextSecondaryDarkColor");

            SetDynamicResource(DynamicBackgroundColor, Color.Transparent);
            SetDynamicResource(DynamicDudeBackgroundColor, "DarkSurface");

            SetDynamicResource(DynamicCornerRadius, 5);
            SetDynamicResource(DynamicElevation, 0);

            SetDynamicResource(DynamicIsVisible, false);

            SetDynamicResource(DynamicTopShadow, ShadowType.None);
            SetDynamicResource(DynamicBottomShadow, ShadowType.None);
            SetDynamicResource(DynamicHasShadow, true);

            SetDynamicResource(DynamicIsTabBlurVisible, true);
            SetDynamicResource(DynamicBottomTabBlurStyle, MaterialFrame.BlurStyle.Dark);
            SetDynamicResource(DynamicBottomBarBackground, Color.Transparent);

            SetDynamicResource(DynamicMaterialTheme, MaterialFrame.Theme.AcrylicBlur);
            SetDynamicResource(DynamicBlurTheme, MaterialFrame.Theme.AcrylicBlur);
            SetDynamicResource(DynamicBlurStyle, MaterialFrame.BlurStyle.Dark);
        }

        public static void SetLightBlur()
        {
            SetDynamicResource(DynamicBackgroundImageSource, new FileImageSource { File = "bliss_portrait.jpg" });

            SetDynamicResource(DynamicNavigationBarColor, Color.Transparent);

            SetDynamicResource(DynamicBarTextColor, "TextPrimaryLightColor");
            SetDynamicResource(DynamicHeaderTextColor, "TextPrimaryLightColor");
            SetDynamicResource(DynamicPrimaryTextColor, "TextPrimaryLightColor");
            SetDynamicResource(DynamicSecondaryTextColor, "TextSecondaryLightColor");

            SetDynamicResource(DynamicBackgroundColor, Color.Transparent);
            SetDynamicResource(DynamicDudeBackgroundColor, Color.Transparent);

            SetDynamicResource(Elevation4dpColor, Color.Transparent);

            SetDynamicResource(DynamicCornerRadius, 10);
            SetDynamicResource(DynamicElevation, 0);

            SetDynamicResource(DynamicIsVisible, true);

            SetDynamicResource(DynamicTopShadow, ShadowType.None);
            SetDynamicResource(DynamicBottomShadow, ShadowType.None);
            SetDynamicResource(DynamicHasShadow, false);

            SetDynamicResource(DynamicIsTabBlurVisible, true);
            SetDynamicResource(DynamicBottomTabBlurStyle, MaterialFrame.BlurStyle.Light);
            SetDynamicResource(DynamicBottomBarBackground, Color.Transparent);

            SetDynamicResource(DynamicMaterialTheme, MaterialFrame.Theme.AcrylicBlur);
            SetDynamicResource(DynamicBlurTheme, MaterialFrame.Theme.AcrylicBlur);
            SetDynamicResource(DynamicBlurStyle, MaterialFrame.BlurStyle.Light);
        }
    }
}
