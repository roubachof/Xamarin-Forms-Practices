using System.Collections.Generic;
using System.Threading.Tasks;

namespace SillyCompany.Mobile.Practices.Domain.Silly
{
    public interface ISillyDudeService
    {
        Task<IReadOnlyList<SillyDude>> GetSillyPeople();

        Task<SillyDude> GetSilly(int id);

        Task<SillyDude> GetRandomSilly();
    }
}