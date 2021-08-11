namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Web.ViewModels.Teams;

    public interface ITeamsService
    {
        IEnumerable<TeamsInListViewModel> GetAllTeams();

        TeamByIdViewModel GetById(int id);

        Task AddTeamAsync(AddTeamInputModel inputModel);

        Task EditTeamAsync(EditTeamInputModel inputModel, int id);

        EditTeamInputModel GetTeamDataForEdit(int id);

        Task DeleteTeamAsync(int id);

    }
}
