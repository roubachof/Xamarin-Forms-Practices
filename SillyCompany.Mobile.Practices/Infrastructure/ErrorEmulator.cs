using System;
using System.Collections.Generic;
using SillyCompany.Mobile.Practices.Localization;

namespace SillyCompany.Mobile.Practices.Infrastructure
{
    public enum ErrorType
    {
        None = 0,
        Unknown = 1,
        Network = 2,
        Server = 3,
        NoData = 4,
    }

    public class ErrorEmulator
    {
        public static List<string> ErrorLabels = new List<string>
        {
            SillyResources.ErrorType_None,
            SillyResources.ErrorType_Unknown,
            SillyResources.ErrorType_Network,
            SillyResources.ErrorType_Server,
            SillyResources.ErrorType_NoData,
        };

        public ErrorType ErrorType { get; set; }

        public TimeSpan DefaultLoadingTime => PlatformService.IsFoldingScreen ? TimeSpan.Zero : TimeSpan.FromSeconds(2);
    }
}
