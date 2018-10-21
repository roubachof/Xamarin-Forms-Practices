using System;

namespace SillyCompany.Mobile.Practices.Domain.Silly
{
    public class SillyDude
    {
        private readonly string _realName;

        public SillyDude(int id, string name, string role, string description, string imageUrl, int sillinessDegree, string sourceUrl = null)
        {
            if (sillinessDegree > 5 || sillinessDegree < 0)
            {
                throw new ArgumentException(@"sillinessDegree must be between 0 and 5", nameof(sillinessDegree));
            }

            Id = id;
            _realName = name;
            Role = role;
            Description = description;
            ImageUrl = imageUrl;
            SillinessDegree = sillinessDegree;
            SourceUrl = sourceUrl;
        }

        public int Id { get; }

#if INFINITE_LIST
        public string Name => $"{TruncateName(8)} n°{Id}";
#else
        public string Name => _realName;
#endif

        public string Role { get; }

        public string Description { get; }

        public string ImageUrl { get; }

        public int SillinessDegree { get; }

        public string SourceUrl { get; }

        public SillyDude Clone(int id)
        {
            return new SillyDude(id, _realName, Role, Description, ImageUrl, SillinessDegree, SourceUrl);
        }

        private string TruncateName(int maxChars)
        {
            return _realName.Length <= maxChars ? _realName : _realName.Substring(0, maxChars) + "...";
        }
    }
}