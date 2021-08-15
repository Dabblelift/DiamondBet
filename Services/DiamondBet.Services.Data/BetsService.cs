namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Data.Models.Enums;
    using DiamondBet.Web.ViewModels.Bets;
    using DiamondBet.Web.ViewModels.Games;

    public class BetsService : IBetsService
    {
        private readonly IDeletableEntityRepository<Bet> betsRepository;
        private readonly IDeletableEntityRepository<Odds> oddsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public BetsService(
           IDeletableEntityRepository<Bet> betsRepository,
           IDeletableEntityRepository<Odds> oddsRepository,
           IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.betsRepository = betsRepository;
            this.oddsRepository = oddsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task AddBetAsync(AddBetInputModel inputModel)
        {
            var gameOdds = this.oddsRepository.AllAsNoTracking().Where(x => x.GameId == inputModel.GameId).FirstOrDefault();
            var user = this.usersRepository.All().Where(x => x.Id == inputModel.UserId).SingleOrDefault();

            decimal betOdds = 0;
            BetType type = default;

            if (inputModel.Prediction == Prediction.HomeWin.ToString())
            {
                betOdds = gameOdds.HomeWinOdds;
                type = BetType.FullTimeResult;
            }
            else if (inputModel.Prediction == Prediction.Draw.ToString())
            {
                betOdds = gameOdds.DrawOdds;
                type = BetType.FullTimeResult;
            }
            else if (inputModel.Prediction == Prediction.AwayWin.ToString())
            {
                betOdds = gameOdds.AwayWinOdds;
                type = BetType.FullTimeResult;
            }
            else if (inputModel.Prediction == Prediction.Over.ToString())
            {
                betOdds = gameOdds.OverOdds;
                type = BetType.MatchGoals;
            }
            else if (inputModel.Prediction == Prediction.Under.ToString())
            {
                betOdds = gameOdds.UnderOdds;
                type = BetType.MatchGoals;
            }
            else if (inputModel.Prediction == Prediction.BTTSYes.ToString())
            {
                betOdds = gameOdds.BTTSYesOdds;
                type = BetType.BothTeamsToScore;
            }
            else if (inputModel.Prediction == Prediction.BTTSNo.ToString())
            {
                betOdds = gameOdds.BTTSNoOdds;
                type = BetType.BothTeamsToScore;
            }

            var bet = new Bet
            {
                GameId = inputModel.GameId,
                Prediction = (Prediction)Enum.Parse(typeof(Prediction), inputModel.Prediction),
                Stake = inputModel.Stake,
                BetOdds = betOdds,
                BetType = type,
                BetStatus = BetStatus.Unsettled,
                UserId = inputModel.UserId,
            };

            user.Coins -= inputModel.Stake;

            await this.betsRepository.AddAsync(bet);
            await this.betsRepository.SaveChangesAsync();
            await this.usersRepository.SaveChangesAsync();
        }

        public bool CheckIfValid(AddBetInputModel inputModel, ApplicationUser user)
        {
            if (inputModel.Prediction == Prediction.NoPrediction.ToString())
            {
                return false;
            }

            if (inputModel.Stake < 1 || inputModel.Stake > 1000)
            {
                return false;
            }

            if (inputModel.Stake > user.Coins)
            {
                return false;
            }

            return true;
        }

        public async Task SettleBetsOnGameResultEditAsync(EditGameInputModel input, int gameId)
        {
            var winningFullTimeBet = Prediction.NoPrediction;
            var winningMatchGoalsBet = Prediction.NoPrediction;
            var winningBothTeamsToScoreBet = Prediction.NoPrediction;

            if (input.HomeGoals > input.AwayGoals)
            {
                winningFullTimeBet = Prediction.HomeWin;
            }
            else if (input.HomeGoals < input.AwayGoals)
            {
                winningFullTimeBet = Prediction.AwayWin;
            }
            else
            {
                winningFullTimeBet = Prediction.Draw;
            }

            if ((input.HomeGoals + input.AwayGoals) >= 3)
            {
                winningMatchGoalsBet = Prediction.Over;
            }
            else
            {
                winningMatchGoalsBet = Prediction.Under;
            }

            if (input.HomeGoals > 0 && input.AwayGoals > 0)
            {
                winningBothTeamsToScoreBet = Prediction.BTTSYes;
            }
            else
            {
                winningBothTeamsToScoreBet = Prediction.BTTSNo;
            }

            var gameBets = this.betsRepository
                .All()
                .Where(x => x.GameId == gameId)
                .Where(x => x.BetStatus == BetStatus.Unsettled)
                .ToList();

            foreach (var bet in gameBets)
            {
                var user = this.usersRepository.All().Where(x => x.Id == bet.UserId).SingleOrDefault();

                if (bet.Prediction == winningFullTimeBet || bet.Prediction == winningBothTeamsToScoreBet || bet.Prediction == winningMatchGoalsBet)
                {
                    bet.BetStatus = BetStatus.Won;
                    user.Coins += bet.ReturnOnWin;
                }
                else
                {
                    bet.BetStatus = BetStatus.Lost;
                }
            }

            await this.betsRepository.SaveChangesAsync();
            await this.usersRepository.SaveChangesAsync();
        }

        public IEnumerable<BetsInListViewModel> GetAllBets()
        {
            return this.betsRepository.AllAsNoTracking().OrderByDescending(x => x.CreatedOn).Select(x => new BetsInListViewModel
            {
                AwayTeamGoals = x.Game.AwayGoals,
                AwayTeamName = x.Game.AwayTeam.Name,
                BetOdds = x.BetOdds,
                BetStatus = x.BetStatus.ToString(),
                GameId = x.GameId,
                HomeTeamGoals = x.Game.HomeGoals,
                HomeTeamName = x.Game.HomeTeam.Name,
                Id = x.Id,
                Prediction = x.Prediction.ToString(),
                UserId = x.UserId,
                Username = x.User.UserName,
            }).ToList();
        }

        public IEnumerable<BetsInListViewModel> GetBetsByUserId(string userId)
        {
            return this.betsRepository.AllAsNoTracking().Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).Select(x => new BetsInListViewModel
            {
                AwayTeamGoals = x.Game.AwayGoals,
                AwayTeamName = x.Game.AwayTeam.Name,
                BetOdds = x.BetOdds,
                BetStatus = x.BetStatus.ToString(),
                GameId = x.GameId,
                HomeTeamGoals = x.Game.HomeGoals,
                HomeTeamName = x.Game.HomeTeam.Name,
                Id = x.Id,
                Prediction = x.Prediction.ToString(),
                UserId = x.UserId,
                Username = x.User.UserName,
            }).ToList();
        }

        public BetByIdViewModel GetById(string id)
        {
            return this.betsRepository.AllAsNoTracking().Where(x => x.Id == id).Select(x => new BetByIdViewModel
            {
                AwayTeamGoals = x.Game.AwayGoals,
                AwayTeamName = x.Game.AwayTeam.Name,
                BetOdds = x.BetOdds,
                BetStatus = x.BetStatus.ToString(),
                BetType = x.BetType.ToString(),
                GameId = x.GameId,
                HomeTeamGoals = x.Game.HomeGoals,
                HomeTeamName = x.Game.HomeTeam.Name,
                Id = x.Id,
                Prediction = x.Prediction.ToString(),
                ReturnOnWin = x.ReturnOnWin,
                Stake = x.Stake,
                UserId = x.UserId,
                Username = x.User.UserName,
            }).FirstOrDefault();
        }
    }
}
