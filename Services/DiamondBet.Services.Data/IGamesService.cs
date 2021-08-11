namespace DiamondBet.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Games;

    public interface IGamesService
    {
        IEnumerable<GamеsInListViewModel> GetAllUpcomingGames();

        IEnumerable<GamеsInListViewModel> GetUpcomingGamesToday();

        IEnumerable<GamеsInListViewModel> GetPreviousGames();

        Task AddGameAsync(AddGameInputModel inputModel);

        EditGameInputModel GetGameDataForEdit(int id);

        Task EditGameAsync(EditGameInputModel inputModel, int id);

        Task DeleteGameAsync(int id);

        GameByIdViewModel GetById(int id);

        IEnumerable<GamеsInListViewModel> GetPreviousGamesByTeam(int teamId);

        IEnumerable<GamеsInListViewModel> GetUpcomingGamesByTeam(int teamid);
    }
}
