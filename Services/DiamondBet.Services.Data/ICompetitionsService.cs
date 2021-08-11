namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Web.ViewModels.Competitions;

    public interface ICompetitionsService
    {
        IEnumerable<CompetitionsInListViewModel> GetAllCompetitions();

        CompetitionByIdViewModel GetById(int id);

        Task AddCompetitionAsync(AddCompetitionInputModel inputModel);

        Task EditCompetitionAsync(EditCompetitionInputModel inputModel, int id);

        EditCompetitionInputModel GetCompetitionDataForEdit(int id);

        Task DeleteCompetitionAsync(int id);
    }
}
