using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace Xamarin.Duo.Forms.Samples
{
    public interface IHingeService : INotifyPropertyChanged, IDisposable
    {
        event EventHandler<HingeEventArgs> OnHingeUpdated;

        bool IsSpanned { get; }

        bool IsLandscape { get; }

        Rectangle GetHinge();
    }

    public class HingeEventArgs : EventArgs
    {
        public HingeEventArgs(int angle)
            : base()
        {
            Angle = angle;
        }

        public int Angle { get; private set; }
    }
}