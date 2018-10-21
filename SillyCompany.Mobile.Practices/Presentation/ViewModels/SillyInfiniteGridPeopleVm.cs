using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Sharpnado.Infrastructure.Services;
using Sharpnado.Infrastructure.Tasks;
using Sharpnado.Presentation.Forms.Paging;
using Sharpnado.Presentation.Forms.ViewModels;
using SillyCompany.Mobile.Practices.Domain;
using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Localization;
using SillyCompany.Mobile.Practices.Presentation.Commands;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.ViewModelObjects;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels
{
    public class SillyInfiniteGridPeopleVm : ANavigableViewModel
    {
        private const int PageSize = 20;
        private readonly ISillyDudeService _sillyDudeService;

        public SillyInfiniteGridPeopleVm(INavigationService navigationService, ISillyDudeService sillyDudeService)
            : base(navigationService)
        {
            _sillyDudeService = sillyDudeService;

            InitCommands();

            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            SillyPeoplePaginator = new Paginator<SillyDude>(LoadSillyPeoplePageAsync, pageSize: PageSize);
            SillyPeopleLoader = new ViewModelLoader<IReadOnlyCollection<SillyDude>>(
                ApplicationExceptions.ToString,
                SillyResources.Empty_Screen);
        }

        public IAsyncCommand GoToSillyDudeCommand { get; protected set; }

        public ViewModelLoader<IReadOnlyCollection<SillyDude>> SillyPeopleLoader { get; }

        public Paginator<SillyDude> SillyPeoplePaginator { get; }

        public ObservableRangeCollection<SillyDudeVmo> SillyPeople { get; set; }

        public override void Load(object parameter)
        {
            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            RaisePropertyChanged(nameof(SillyPeople));

            SillyPeopleLoader.Load(async () => (await SillyPeoplePaginator.LoadPage(1)).Items);
        }

        private void InitCommands()
        {
            GoToSillyDudeCommand = AsyncCommand.Create(
                parameter => NavigationService.NavigateToAsync<SillyDudeVm>(((SillyDudeVmo)parameter).Id));
        }

        private async Task<PageResult<SillyDude>> LoadSillyPeoplePageAsync(int pageNumber, int pageSize)
        {
            PageResult<SillyDude> resultPage = await _sillyDudeService.GetSillyPeoplePage(pageNumber, pageSize);
            var viewModels = resultPage.Items.Select(dude => new SillyDudeVmo(dude, GoToSillyDudeCommand)).ToList();
            SillyPeople.AddRange(viewModels);

            return resultPage;
        }
    }
}
