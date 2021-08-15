namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Bets;
    using DiamondBet.Web.ViewModels.Games;

    public interface IBetsService
    {
        IEnumerable<BetsInListViewModel> GetAllBets();

        BetByIdViewModel GetById(string id);

        IEnumerable<BetsInListViewModel> GetBetsByUserId(string userId);

        Task AddBetAsync(AddBetInputModel inputModel);

        bool CheckIfValid(AddBetInputModel inputModel, ApplicationUser user);

        Task SettleBetsOnGameResultEditAsync(EditGameInputModel input, int gameId);
    }
}
