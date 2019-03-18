// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyFrontService.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The SillyFrontService interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sharpnado.Infrastructure;
using Sharpnado.Infrastructure.Services;
using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Localization;

namespace SillyCompany.Mobile.Practices.Domain.Silly
{
    public class SillyDudeService : ISillyDudeService
    {
        private readonly ErrorEmulator _errorEmulator;
        private readonly int _peopleCounter = 0;
        private readonly Dictionary<int, SillyDude> _repository;

        private readonly Random _randomizer = new Random();

        public SillyDudeService(ErrorEmulator errorEmulator)
        {
            _errorEmulator = errorEmulator;
            _repository = new Dictionary<int, SillyDude>
                {
                    {
                        ++_peopleCounter,
                        new SillyDude(
                            _peopleCounter,
                            "Will Ferrell",
                            "Actor",
                            "Hey.\nThey laughed at Louis Armstrong when he said he was gonna go to the moon.\nNow he’s up there, laughing at them.",
#if LOCAL_DATA
                            "will_ferrell.jpg",
#else
                            "http://www.2oceansvibe.com/wp-content/uploads/2017/10/willlferell.jpg",
#endif
                            4,
                            Filmos.Ferrell,
                            "https://sayingimages.com/wp-content/uploads/dear-monday-will-ferrell-memes.jpg",
                            "https://youtu.be/sPFRZP4qY7I?t=26")
                    },
                    {
                        ++_peopleCounter,
                        new SillyDude(
                            _peopleCounter,
                            "Knights of Ni",
                            "Knights",
                            "Keepers of the sacred words 'Ni', 'Peng', and 'Neee-Wom'",
#if LOCAL_DATA
                            "knights_of_ni.jpg",
#else
                            "http://images.uncyc.org/commons/d/dd/The_leader_of_the_Knights_Who_Say_Ni.jpg",
#endif
                            5,
                            "ni!",
                            "http://www.quickmeme.com/img/e8/e835352f2b7cd21efc12eba56c0d5a3e00c411965b029126cb5eb360fcfa6eb5.jpg",
                            "https://www.youtube.com/watch?v=zIV4poUZAQo")
                    },
                    {
                        ++_peopleCounter,
                        new SillyDude(
                            _peopleCounter,
                            "Jean-Claude",
                            "Actor",
                            "J’adore les cacahuètes.\nTu bois une bière et tu en as marre du goût. Alors tu manges des cacahuètes.\nLes cacahuètes, c’est doux et salé, fort et tendre, comme une femme. Manger des cacahuètes. It’s a really strong feeling.\nEt après tu as de nouveau envie de boire une bière.\nLes cacahuètes, c’est le mouvement perpétuel à la portée de l’homme.",
#if LOCAL_DATA
                            "jean_claude_van_damme.jpg",
#else
                            "http://media.popculture.com/2017/06/jean-claude-van-damme-predator-20003834-640x480.jpg",
#endif
                            5,
                            Filmos.VanDamme,
                            "http://www.quickmeme.com/img/48/485340aeb0d8a432c3f3eb5b33ad9fb2c92b6c2ef17ccc53719474302e51b9de.jpg"
                            )
                    },
//                    {
//                        ++_peopleCounter,
//                        new SillyDude(
//                            _peopleCounter,
//                            "Louis C.K.",
//                            "Comedian",
//                            "There are people that really live by doing the right thing, but I don't know what that is, I'm really curious about that.\nI'm really curious about what people think they're doing when they're doing something evil, casually.\nI think it's really interesting, that we benefit from suffering so much, and we excuse ourselves from it.",
//#if LOCAL_DATA
//                            "louis_ck.jpg",
//#else
//                            "http://pixel.nymag.com/imgs/daily/vulture/2016/04/21/21-louis-ck.w529.h529.jpg",
//#endif
//                            2)
//                    },
                    {
                        ++_peopleCounter,
                        new SillyDude(
                            _peopleCounter,
                            "Triumph",
                            "Insult Comic Dog",
                            "Occupy Wall Street, talking to a trader with a Fox News mustache on.\nThese protesters with their whining and crying right?\nDon't they realize that their public education are being funded from the taxes you evade each year?\nI don't want to keep you, you're a good man! You better hurry back from lunch so you can collect your hurry back from lunch bonus.",
#if LOCAL_DATA
                            "louis_ck.jpg",
#else
                            "https://jnxz07-a.akamaihd.net/wp-content/uploads/2015/03/triumph-e1425247855563.jpeg",
#endif
                            2,
                            Filmos.Triumph,
                            "http://2.bp.blogspot.com/-sPduDIOi2-U/UR3Bpkjn0jI/AAAAAAAAGlo/SnSOI-tYyqo/s1600/PoopBoatTriumph.jpg",
                            "https://youtu.be/O-253uBJap8?t=315")
                    },
                    {
                        ++_peopleCounter,
                        new SillyDude(
                            _peopleCounter,
                            "Ricky Gervais",
                            "Comedian",
                            "Science seeks the truth. And it does not discriminate. For better or worse it finds things out.\nScience is humble.\nIt knows what it knows and it knows what it doesn’t know. It bases its conclusions and beliefs on hard evidence -­- evidence that is constantly updated and upgraded.\nIt doesn’t get offended when new facts come along. It embraces the body of knowledge.\nIt doesn’t hold on to medieval practices because they are tradition.",
#if LOCAL_DATA
                            "louis_ck.jpg",
#else
                            "http://wfmj.images.worldnow.com/images/6382251_G.jpg?auto=webp&disable=upscale&width=800",
#endif
                            3,
                            Filmos.Gervais,
                            "https://cdn.newsapi.com.au/image/v1/e737788003cdba5e51cea05a15d8094a")
                    },
                    {
                        ++_peopleCounter,
                        new SillyDude(
                            _peopleCounter,
                            "Steve Carell",
                            "Comedian",
                            "Vincent van Gogh. Everyone told him: \"You only have one ear. You cannot be a great artist.\"\nAnd you know what he said?\n\"I can\'t hear you.",
#if LOCAL_DATA
                            "louis_ck.jpg",
#else
                            "http://www4.pictures.zimbio.com/mp/8q1mIIQGkXHm.jpg",
#endif
                            3,
                            Filmos.Carell,
                            "https://sayingimages.com/wp-content/uploads/fool-me-once-michael-scott-memes-1.jpg",
                            "https://youtu.be/N9Z4vqysxMQ?t=92")
                    },
                    {
                        ++_peopleCounter,
                        new SillyDude(
                            _peopleCounter,
                            "Father Ted",
                            "Priest",
                            "My Lovely Horse,\r\nRunning through the field,\r\nWhere are you going,\r\nWith your fetlocks blowing,\r\nIn the wind.\r\n\r\n“I want to shower you with sugar lumps,\r\nAnd ride you over fences,\r\nI want to polish your hooves every single day,\r\nAnd bring you to the horse dentist.\r\n\r\n“My lovely horse,\r\nYou’re a pony no more,\r\nRunning around,\r\nWith a man on your back,\r\nLike a train in the night,\r\nLike a train in the night!”",
#if LOCAL_DATA
                            "louis_ck.jpg",
#else
                            "http://cdn.playbuzz.com/cdn/b0cd9743-c236-4c0e-b468-00f83724f117/e2b08eac-053b-4b06-8538-405852dc865b.jpg",
#endif
                            4,
                            Filmos.Ted,
                            "https://cdn.psychologytoday.com/sites/default/files/styles/image-article_inline_full/public/blogs/129003/2014/08/158581-164751.jpg?itok=f7HhI_lo",
                            "https://www.youtube.com/watch?v=jzYzVMcgWhg")
                    },
                    {
                        ++_peopleCounter,
                        new SillyDude(
                            _peopleCounter,
                            "Moss",
                            "IT Guy",
                            "Did you see that ludicrous display last night?\nThe thing about Arsenal is, they always try to walk it in!",
#if LOCAL_DATA
                            "louis_ck.jpg",
#else
                            "http://i123.photobucket.com/albums/o320/lucy_edward/moss_pic2.jpg",
#endif
                            3,
                            Filmos.Moss,
                            "https://images1.memedroid.com/images/UPLOADED8/501f9cd68575e.jpeg",
                            "https://youtu.be/NKHyqjHqQLU?t=32")
                    },
                    {
                        ++_peopleCounter,
                        new SillyDude(
                            _peopleCounter,
                            "Les Nuls",
                            "Crétins Fabuleux",
                            "Agad la té'évision é pis dors!\nAgad la té'évision é pis dors.\nAgad la té'évision é pis dors!\nAgad la té'évision é pis dors.\nAgad la té'évision é pis dors!\nAgad la té'évision é pis dors.\nAgad la té'évision é pis dors.\n",
#if LOCAL_DATA
                            "louis_ck.jpg",
#else
                            "http://4.bp.blogspot.com/_fMA4vs4kLc0/S0yka8se4NI/AAAAAAAAAtc/chbrVG878_0/s1600/carette+chabat.jpg",
#endif
                            4,
                            Filmos.LesNuls,
                            "https://img.static-rmg.be/a/view/q75/w720/h480/2223699/un-clin-doeil-a-la-cite-de-la-peur-sur-le-site-de-2-24535-1433777643-0-dblbig-jpg.jpg",
                            "https://www.youtube.com/watch?v=lNEpFJYduto")
                    },
                };

            for (int id = 1; id < 200; id++)
            {
                var dudeToClone = _repository[id];
                _repository.Add(++_peopleCounter, dudeToClone.Clone(_peopleCounter));
            }
        }

        public async Task<IReadOnlyCollection<SillyDude>> GetSillyPeople()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            if (ProcessErrorEmulator())
            {
                return new List<SillyDude>();
            }

            return _repository.Values.Take(9).ToList();
        }

        public async Task<PageResult<SillyDude>> GetSillyPeoplePage(int pageNumber, int pageSize)
        {
            Contract.Requires(() => pageNumber > 0);
            Contract.Requires(() => pageSize >= 10);

            await Task.Delay(TimeSpan.FromSeconds(pageNumber > 1 ? 1 : 3));
            if (ProcessErrorEmulator())
            {
                return PageResult<SillyDude>.Empty;
            }

            return new PageResult<SillyDude>(
                _repository.Count,
                _repository.Values.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
        }

        public async Task<SillyDude> GetSilly(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            ProcessErrorEmulator();

            return _repository[id];
        }

        public async Task<SillyDude> GetRandomSilly()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));

            int minId = _repository.Keys.Min();
            int maxId = _repository.Keys.Max();

            return _repository[_randomizer.Next(minId, maxId)];
        }

        private bool ProcessErrorEmulator()
        {
            switch (_errorEmulator.ErrorType)
            {
                case ErrorType.Unknown:
                    throw new InvalidOperationException();
                case ErrorType.Network:
                    throw new NetworkException();
                case ErrorType.Server:
                    throw new ServerException();
                case ErrorType.NoData:
                    return true;
                default:
                    return false;
            }
        }
    }
}