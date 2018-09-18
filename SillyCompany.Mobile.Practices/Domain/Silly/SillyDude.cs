using System;

namespace SillyCompany.Mobile.Practices.Domain.Silly
{
    public class SillyDude
    {
        public SillyDude(int id, string name, string role, string description, string imageUrl, int sillinessDegree, string sourceUrl = null)
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
            SourceUrl = sourceUrl;
        }

        public int Id { get; }

        public string Name { get; }

        public string Role { get; }

        public string Description { get; }

        public string ImageUrl { get; }

        public int SillinessDegree { get; }

        public string SourceUrl { get; }
    }
}