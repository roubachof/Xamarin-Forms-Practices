using System;
using System.Collections.Generic;
using System.Text;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels.DudeDetails
{
    public class FilmoVmo
    {
        public FilmoVmo(string filmoMarkdown)
        {
            FilmoMarkdown = filmoMarkdown;
        }

        public string FilmoMarkdown { get; }
    }
}
