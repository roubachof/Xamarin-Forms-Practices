using System;

namespace SillyCompany.Mobile.Practices.Domain.Silly
{
    public class SillyDude
    {
        public SillyDude(int id, string name, string role, string description, string imageUrl, int sillinessDegree)
        {
            if (sillinessDegree > 5 || sillinessDegree < 0)
            {
                throw new ArgumentException("sillinessDegree must be between 0 and 5", nameof(sillinessDegree));
            }

            Id = id;
            Name = name;
            Role = role;
            Description = description;
            ImageUrl = imageUrl;
            SillinessDegree = sillinessDegree;
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

        public int SillinessDegree { get; }
    }
}