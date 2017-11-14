// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyFrontService.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The SillyFrontService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SillyCompany.Mobile.Practices.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using SillyCompany.Mobile.Practices.ViewModels.ViewModelObjects;

    /// <summary>
    /// The SillyFrontService interface.
    /// </summary>
    public interface ISillyFrontService
    {
        /// <summary>
        /// The get silly people.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<IReadOnlyList<SillyVmo>> GetSillyPeople();

        /// <summary>
        /// The get silly.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<SillyVmo> GetSilly(int id);
    }

    /// <summary>
    /// The silly front service.
    /// </summary>
    public class SillyFrontService : ISillyFrontService
    {
        /// <summary>
        /// The people counter.
        /// </summary>
        private readonly int peopleCounter = 0;

        /// <summary>
        /// The repository.
        /// </summary>
        private readonly Dictionary<int, SillyVmo> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="SillyFrontService"/> class.
        /// </summary>
        public SillyFrontService()
        {
            this.repository = new Dictionary<int, SillyVmo>
                {
                    {
                        ++this.peopleCounter,
                        new SillyVmo(
                            this.peopleCounter,
                            "Will Ferrel",
                            "Actor",
                            "Hey. They laughed at Louis Armstrong when he said he was gonna go to the moon. Now he’s up there, laughing at them.",

#if LOCAL_DATA
                            "will_ferrell.jpg")
#else
                            "https://iwonderandwander.files.wordpress.com/2013/03/ferrell-twitter-photo.jpg?w=660")
#endif
                    },
                    {
                        ++this.peopleCounter,
                        new SillyVmo(
                            this.peopleCounter,
                            "Knights of Ni",
                            "Knights",
                            "Keepers of the sacred words 'Ni', 'Peng', and 'Neee-Wom'",
#if LOCAL_DATA
                            "knights_of_ni.jpg")
#else
                            "http://www.geekalerts.com/u/Knights-of-Ni-Plush-Hat.jpg")
#endif
                    },
                    {
                        ++this.peopleCounter,
                        new SillyVmo(
                            this.peopleCounter,
                            "Jean-Claude",
                            "Actor",
                            "J’adore les cacahuètes. Tu bois une bière et tu en as marre du goût. Alors tu manges des cacahuètes. Les cacahuètes, c’est doux et salé, fort et tendre, comme une femme. Manger des cacahuètes. It’s a really strong feeling. Et après tu as de nouveau envie de boire une bière. Les cacahuètes, c’est le mouvement perpétuel à la portée de l’homme.",
#if LOCAL_DATA
                            "jean_claude_van_damme.jpg")
#else
                            "http://www.cultivonsnous.fr/images/stories/jean-claude-van-damme.jpg")
#endif
                    },
                    {
                        ++this.peopleCounter,
                        new SillyVmo(
                            this.peopleCounter,
                            "Louis C.K.",
                            "Comedian",
                            "There are people that really live by doing the right thing, but I don't know what that is, I'm really curious about that. I'm really curious about what people think they're doing when they're doing something evil, casually. I think it's really interesting, that we benefit from suffering so much, and we excuse ourselves from it.",
#if LOCAL_DATA
                            "louis_ck.jpg")
#else
                            "http://pixel.nymag.com/imgs/daily/vulture/2016/04/21/21-louis-ck.w529.h529.jpg")
#endif
                    }
                };
        }

        public async Task<IReadOnlyList<SillyVmo>> GetSillyPeople()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));

            if (DateTime.UtcNow.Second % 3 == 0)
            {
                // throw new InvalidOperationException("By the beard of Zeus !");
            }

            return new List<SillyVmo>(this.repository.Values);
        }

        public async Task<SillyVmo> GetSilly(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));

            if (DateTime.UtcNow.Second % 2 == 0)
            {
                throw new InvalidOperationException("By the beard of Zeus !");
            }

            return this.repository[id];
        }        
    }
}