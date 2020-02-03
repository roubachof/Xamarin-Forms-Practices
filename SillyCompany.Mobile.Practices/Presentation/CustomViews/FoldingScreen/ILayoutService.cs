using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Xamarin.Duo.Forms.Samples
{
    public interface ILayoutService
    {
        event EventHandler<LayoutGuideChangedArgs> LayoutGuideChanged;

        IReadOnlyDictionary<string, LayoutGuide> LayoutGuides { get; }

        Point? GetLocationOnScreen(VisualElement visualElement);

        void AddLayoutGuide(string name, Rectangle location);
    }

    public abstract class LayoutServiceBase : ILayoutService
    {
        protected LayoutServiceBase()
        {
            LayoutGuides = LayoutGuidesInternal;
        }

        public event EventHandler<LayoutGuideChangedArgs> LayoutGuideChanged;

        private Dictionary<string, LayoutGuide> LayoutGuidesInternal { get; } = new Dictionary<string, LayoutGuide>();

        public IReadOnlyDictionary<string, LayoutGuide> LayoutGuides { get; }

        public void AddLayoutGuide(string name, Rectangle location)
        {
            var guide = new LayoutGuide(name, location);
            LayoutGuidesInternal[name] = guide;
            LayoutGuideChanged?.Invoke(this, new LayoutGuideChangedArgs(guide));
        }

        public abstract Point? GetLocationOnScreen(VisualElement visualElement);
    }

    public class LayoutGuideChangedArgs : EventArgs
    {
        public LayoutGuideChangedArgs(LayoutGuide layoutGuide)
        {
            LayoutGuide = layoutGuide;
        }

        public LayoutGuide LayoutGuide { get; }
    }

    public class LayoutGuide
    {
        public LayoutGuide(string name, Rectangle rectangle)
        {
            Name = name;
            Rectangle = rectangle;
        }

        public string Name { get; }

        public Rectangle Rectangle { get; }
    }
}