namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Bets;
    using DiamondBet.Web.ViewModels.Users;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Bet> betRepository;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Bet> betRepository)
        {
            this.usersRepository = usersRepository;
            this.betRepository = betRepository;
        }

        public UserByIdViewModel GetById(string id)
        {
            var bets = this.betRepository
                .AllAsNoTracking()
                .Where(x => x.UserId == id)
                .OrderByDescending(x => x.CreatedOn)
                .Take(5)
                .Select(x => new BetsInListViewModel
            {
                Id = x.Id,
                UserId = x.UserId,
                AwayTeamGoals = x.Game.AwayGoals,
                AwayTeamName = x.Game.AwayTeam.Name,
                BetOdds = x.BetOdds,
                BetStatus = x.BetStatus.ToString(),
                GameId = x.GameId,
                HomeTeamGoals = x.Game.HomeGoals,
                HomeTeamName = x.Game.HomeTeam.Name,
                Prediction = x.Prediction.ToString(),
            }).ToList();

            return this.usersRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new UserByIdViewModel
            {
                Id = x.Id,
                Username = x.UserName,
                Coins = x.Coins,
                FirstPlaces = x.FirstPlaces,
                SecondPlaces = x.SecondPlaces,
                ThirdPlaces = x.ThirdPlaces,
                RegisteredOn = x.CreatedOn.ToLocalTime().ToString("dd/MM/yyyy HH:mm"),
                Bets = bets,
            }).FirstOrDefault();
        }

        public async Task ResetUsersCoinsAsync()
        {
            var users = this.usersRepository
                .All()
                .Where(x => x.UserBets.Where(x => x.CreatedOn.Month == DateTime.Now.ToUniversalTime().Month).ToList().Count > 0)
                .OrderByDescending(x => x.Coins)
                .ThenBy(x => x.UserBets.Where(x => x.CreatedOn.Month == DateTime.Now.ToUniversalTime().Month).ToList().Count)
                .ToList();

            if (users.Count >= 3)
            {
                users[0].FirstPlaces++;
                users[1].SecondPlaces++;
                users[2].ThirdPlaces++;
            }
            else if (users.Count == 2)
            {
                users[0].FirstPlaces++;
                users[1].SecondPlaces++;
            }
            else if (users.Count == 1)
            {
                users[0].FirstPlaces++;
            }

            foreach (var user in users)
            {
                user.Coins = 1000;
            }

            await this.usersRepository.SaveChangesAsync();
        }
    }
}
