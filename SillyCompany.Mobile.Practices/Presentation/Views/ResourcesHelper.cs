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

        public const string DynamicBottomTabsShadow = nameof(DynamicBottomTabsShadow);
        public const string DynamicTabsShadow = nameof(DynamicTabsShadow);
        public const string DynamicShadow = nameof(DynamicShadow);
        public const string DynamicToolbarShadow = nameof(DynamicToolbarShadow);

        public const string DynamicOutlineColor = nameof(DynamicOutlineColor);

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

            SetDynamicResource(DynamicBottomTabsShadow, "NoShadow");
            SetDynamicResource(DynamicTabsShadow, "NoShadow");
            SetDynamicResource(DynamicShadow, "NoShadow");
            SetDynamicResource(DynamicToolbarShadow, "NoShadow");

            SetDynamicResource(DynamicOutlineColor, "Accent");

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

            SetDynamicResource(DynamicBottomTabsShadow, isAcrylic ? "AcrylicTopShadow" : "LightTopShadow");
            SetDynamicResource(DynamicTabsShadow, "SegmentedBottomShadow");
            SetDynamicResource(DynamicToolbarShadow, "LightBottomShadow");
            SetDynamicResource(DynamicShadow, "NoShadow");

            SetDynamicResource(DynamicOutlineColor, "Accent");

            SetDynamicResource(DynamicIsTabBlurVisible, false);
            SetDynamicResource(DynamicBottomBarBackground, isAcrylic ? "AcrylicFrameBackgroundColor" : "OnSurfaceColor");

            SetDynamicResource(DynamicBackgroundImageSource, new FileImageSource());
        }

        public static void SetNeumorphicMode()
        {
            SetDynamicResource(DynamicMaterialTheme, MaterialFrame.Theme.Light);
            SetDynamicResource(DynamicBlurTheme, MaterialFrame.Theme.Light);

            SetDynamicResource(DynamicNavigationBarColor, "Accent");

            SetDynamicResource(DynamicBarTextColor, "TextPrimaryDarkColor");
            SetDynamicResource(DynamicHeaderTextColor, "TextPrimaryLightColor");
            SetDynamicResource(DynamicPrimaryTextColor, "TextPrimaryLightColor");
            SetDynamicResource(DynamicSecondaryTextColor, "TextSecondaryLightColor");

            SetDynamicResource(DynamicBackgroundColor, "NeumorphismSurface" );
            SetDynamicResource(DynamicDudeBackgroundColor, "NeumorphismSurface");

            SetDynamicResource(Elevation4dpColor, "NeumorphismSurface");

            SetDynamicResource(DynamicLightThemeColor, "NeumorphismSurface");

            SetDynamicResource(DynamicElevation, 0);
            SetDynamicResource(DynamicCornerRadius, 10);

            SetDynamicResource(DynamicIsVisible, false);

            SetDynamicResource(DynamicBottomTabsShadow, "ThinNeumorphismShadow");
            SetDynamicResource(DynamicTabsShadow, "ThinNeumorphismShadow");
            SetDynamicResource(DynamicToolbarShadow, "LightBottomShadow");
            SetDynamicResource(DynamicShadow, "ThinNeumorphismShadow");

            SetDynamicResource(DynamicOutlineColor, Color.Default);

            SetDynamicResource(DynamicIsTabBlurVisible, false);
            SetDynamicResource(DynamicBottomBarBackground, "NeumorphismSurface");

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

            SetDynamicResource(DynamicBottomTabsShadow, "NoShadow");
            SetDynamicResource(DynamicTabsShadow, "NoShadow");
            SetDynamicResource(DynamicToolbarShadow, "NoShadow");
            SetDynamicResource(DynamicShadow, "NoShadow");

            SetDynamicResource(DynamicOutlineColor, Color.Default);

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

            SetDynamicResource(DynamicBottomTabsShadow, "NoShadow");
            SetDynamicResource(DynamicTabsShadow, "NoShadow");
            SetDynamicResource(DynamicToolbarShadow, "NoShadow");
            SetDynamicResource(DynamicShadow, "NoShadow");

            SetDynamicResource(DynamicIsTabBlurVisible, true);
            SetDynamicResource(DynamicBottomTabBlurStyle, MaterialFrame.BlurStyle.Light);
            SetDynamicResource(DynamicBottomBarBackground, Color.Transparent);

            SetDynamicResource(DynamicOutlineColor, Color.Default);

            SetDynamicResource(DynamicMaterialTheme, MaterialFrame.Theme.AcrylicBlur);
            SetDynamicResource(DynamicBlurTheme, MaterialFrame.Theme.AcrylicBlur);
            SetDynamicResource(DynamicBlurStyle, MaterialFrame.BlurStyle.Light);
        }
    }
}
