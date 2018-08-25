// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyVmo.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The silly vmo.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Windows.Input;
using SillyCompany.Mobile.Practices.Domain.Silly;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels.ViewModelObjects
{
    public class SillyDudeVmo
    {
        public SillyDudeVmo(SillyDude dude, ICommand onItemTappedCommand)
        {
            Id = dude.Id;
            Name = dude.Name;
            Role = dude.Role;
            Description = dude.Description;
            ImageUrl = dude.ImageUrl;
            SillinessDegree = dude.SillinessDegree;

            OnItemTappedCommand = onItemTappedCommand;
        }

        public ICommand OnItemTappedCommand { get; set; }

        public int Id { get; }

        public string Name { get; }

        public string Role { get; }

        public string Description { get; }

        public string ImageUrl { get; }

        public int SillinessDegree { get; }
    }
}