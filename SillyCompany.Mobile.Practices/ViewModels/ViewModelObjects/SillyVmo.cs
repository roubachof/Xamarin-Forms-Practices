// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyVmo.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The silly vmo.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SillyCompany.Mobile.Practices.ViewModels.ViewModelObjects
{
    /// <summary>
    /// The silly vmo.
    /// </summary>
    public class SillyVmo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SillyVmo"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <param name="description">
        /// The description.
        /// </param>
        /// <param name="imageUrl">
        /// The image url.
        /// </param>
        public SillyVmo(int id, string name, string role, string description, string imageUrl)
        {
            this.Id = id;
            this.Name = name;
            this.Role = role;
            this.Description = description;
            this.ImageUrl = imageUrl;
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the role.
        /// </summary>
        public string Role { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets the image url.
        /// </summary>
        public string ImageUrl { get; }
    }
}