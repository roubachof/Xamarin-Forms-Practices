﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Sharpnado.Infrastructure.Services;

namespace SillyCompany.Mobile.Practices.Domain.Silly
{
    public interface ISillyDudeService
    {
        Task<IReadOnlyCollection<SillyDude>> GetSillyPeople();

        Task<PageResult<SillyDude>> GetSillyPeoplePage(int pageNumber, int pageSize);

        Task<SillyDude> GetSilly(int id);

        Task<SillyDude> GetRandomSilly();
    }
}