namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Games;

    public class GamesService : IGamesService
    {
        private readonly IDeletableEntityRepository<Game> gamesRepository;
        private readonly IDeletableEntityRepository<Odds> oddsRepository;

        public GamesService(
            IDeletableEntityRepository<Game> gamesRepository,
            IDeletableEntityRepository<Odds> oddsRepository)
        {
            this.gamesRepository = gamesRepository;
            this.oddsRepository = oddsRepository;
        }

        public async Task AddGameAsync(AddGameInputModel inputModel)
        {
            var odds = new Odds
            {
                HomeWinOdds = inputModel.Odds.HomeWinOdds,
                AwayWinOdds = inputModel.Odds.AwayWinOdds,
                DrawOdds = inputModel.Odds.DrawOdds,
                OverOdds = inputModel.Odds.OverOdds,
                UnderOdds = inputModel.Odds.UnderOdds,
                BTTSNoOdds = inputModel.Odds.BTTSNoOdds,
                BTTSYesOdds = inputModel.Odds.BTTSYesOdds,
            };

            var game = new Game
            {
                StartingTime = inputModel.StartingTime.ToUniversalTime(),
                HomeTeamId = inputModel.HomeTeamId,
                AwayTeamId = inputModel.AwayTeamId,
                CompetitionId = inputModel.CompetitionId,
                StadiumId = inputModel.StadiumId,
                Odds = odds,
            };

            await this.oddsRepository.AddAsync(odds);
            await this.gamesRepository.AddAsync(game);
            await this.gamesRepository.SaveChangesAsync();
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = this.gamesRepository.All().FirstOrDefault(g => g.Id == id);
            this.gamesRepository.Delete(game);
            await this.gamesRepository.SaveChangesAsync();
        }

        public EditGameInputModel GetGameDataForEdit(int id)
        {
            return this.gamesRepository.AllAsNoTracking().Where(g => g.Id == id).Select(g => new EditGameInputModel
            {
                AwayGoals = g.AwayGoals,
                AwayTeamId = g.AwayTeamId,
                CompetitionId = g.CompetitionId,
                HomeGoals = g.HomeGoals,
                HomeTeamId = g.HomeTeamId,
                StadiumId = g.StadiumId,
                StartingTime = g.StartingTime,
                Odds = new OddsInputModel
                {
                    AwayWinOdds = g.Odds.AwayWinOdds,
                    BTTSNoOdds = g.Odds.BTTSNoOdds,
                    BTTSYesOdds = g.Odds.BTTSYesOdds,
                    DrawOdds = g.Odds.DrawOdds,
                    HomeWinOdds = g.Odds.HomeWinOdds,
                    OverOdds = g.Odds.OverOdds,
                    UnderOdds = g.Odds.UnderOdds,
                },
            }).FirstOrDefault();
        }

        public async Task EditGameAsync(EditGameInputModel inputModel, int id)
        {
            var oddsResult = this.oddsRepository.All().Where(x => x.Game.Id == id).SingleOrDefault();
            oddsResult.HomeWinOdds = inputModel.Odds.HomeWinOdds;
            oddsResult.AwayWinOdds = inputModel.Odds.AwayWinOdds;
            oddsResult.DrawOdds = inputModel.Odds.DrawOdds;
            oddsResult.OverOdds = inputModel.Odds.OverOdds;
            oddsResult.UnderOdds = inputModel.Odds.UnderOdds;
            oddsResult.BTTSNoOdds = inputModel.Odds.BTTSNoOdds;
            oddsResult.BTTSYesOdds = inputModel.Odds.BTTSYesOdds;

            var gameResult = this.gamesRepository.All().Where(x => x.Id == id).SingleOrDefault();
            gameResult.StartingTime = inputModel.StartingTime.ToUniversalTime();
            gameResult.HomeTeamId = inputModel.HomeTeamId;
            gameResult.AwayTeamId = inputModel.AwayTeamId;
            gameResult.CompetitionId = inputModel.CompetitionId;
            gameResult.StadiumId = inputModel.StadiumId;
            gameResult.HomeGoals = inputModel.HomeGoals;
            gameResult.AwayGoals = inputModel.AwayGoals;

            await this.oddsRepository.SaveChangesAsync();
            await this.gamesRepository.SaveChangesAsync();
        }

        public IEnumerable<GamеsInListViewModel> GetAllUpcomingGames()
        {
            return this.gamesRepository.AllAsNoTracking()
                .Where(g => g.StartingTime > DateTime.UtcNow)
                .OrderBy(g => g.Competition.Name)
                .Select(g => new GamеsInListViewModel
                {
                    Id = g.Id,
                    StartingTime = g.StartingTime.ToLocalTime(),
                    AwayTeamId = g.AwayTeamId,
                    AwayTeamName = g.AwayTeam.Name,
                    HomeTeamId = g.HomeTeamId,
                    HomeTeamName = g.HomeTeam.Name,
                    Odds = new OddsViewModel
                    {
                        AwayWinOdds = g.Odds.AwayWinOdds,
                        BTTSNoOdds = g.Odds.BTTSNoOdds,
                        BTTSYesOdds = g.Odds.BTTSYesOdds,
                        DrawOdds = g.Odds.DrawOdds,
                        HomeWinOdds = g.Odds.HomeWinOdds,
                        OverOdds = g.Odds.OverOdds,
                        UnderOdds = g.Odds.UnderOdds,
                    },
                }).ToList();
        }

        public GameByIdViewModel GetById(int id)
        {
            return this.gamesRepository.AllAsNoTracking().Where(g => g.Id == id).Select(g => new GameByIdViewModel
            {
                Id = g.Id,
                AwayGoals = g.AwayGoals,
                AwayTeamId = g.AwayTeamId,
                AwayTeamName = g.AwayTeam.Name,
                CompetitionId = g.CompetitionId,
                CompetitionName = g.Competition.Name,
                HomeGoals = g.HomeGoals,
                HomeTeamId = g.HomeTeamId,
                HomeTeamName = g.HomeTeam.Name,
                StadiumId = g.StadiumId,
                StadiumName = g.Stadium.Name,
                StartingTime = g.StartingTime.ToLocalTime(),
                Odds = new OddsViewModel
                {
                    AwayWinOdds = g.Odds.AwayWinOdds,
                    BTTSNoOdds = g.Odds.BTTSNoOdds,
                    BTTSYesOdds = g.Odds.BTTSYesOdds,
                    DrawOdds = g.Odds.DrawOdds,
                    HomeWinOdds = g.Odds.HomeWinOdds,
                    OverOdds = g.Odds.OverOdds,
                    UnderOdds = g.Odds.UnderOdds,
                },
            }).FirstOrDefault();
        }

        public IEnumerable<GamеsInListViewModel> GetPreviousGames()
        {
            return this.gamesRepository.AllAsNoTracking()
                .Where(g => g.StartingTime <= DateTime.UtcNow)
                .OrderByDescending(g => g.StartingTime)
                .Select(g => new GamеsInListViewModel
                {
                    Id = g.Id,
                    StartingTime = g.StartingTime.ToLocalTime(),
                    AwayTeamId = g.AwayTeamId,
                    AwayTeamName = g.AwayTeam.Name,
                    HomeTeamId = g.HomeTeamId,
                    HomeTeamName = g.HomeTeam.Name,
                    HomeTeamGoals = g.HomeGoals,
                    AwayTeamGoals = g.AwayGoals,
                }).ToList();
        }

        public IEnumerable<GamеsInListViewModel> GetUpcomingGamesToday()
        {
            return this.gamesRepository.AllAsNoTracking()
                .Where(g => g.StartingTime > DateTime.UtcNow && g.StartingTime <= DateTime.UtcNow.AddHours(24))
                .OrderBy(g => g.StartingTime)
                .Select(g => new GamеsInListViewModel
                {
                    Id = g.Id,
                    StartingTime = g.StartingTime.ToLocalTime(),
                    AwayTeamId = g.AwayTeamId,
                    AwayTeamName = g.AwayTeam.Name,
                    HomeTeamId = g.HomeTeamId,
                    HomeTeamName = g.HomeTeam.Name,
                    Odds = new OddsViewModel
                    {
                        AwayWinOdds = g.Odds.AwayWinOdds,
                        BTTSNoOdds = g.Odds.BTTSNoOdds,
                        BTTSYesOdds = g.Odds.BTTSYesOdds,
                        DrawOdds = g.Odds.DrawOdds,
                        HomeWinOdds = g.Odds.HomeWinOdds,
                        OverOdds = g.Odds.OverOdds,
                        UnderOdds = g.Odds.UnderOdds,
                    },
                }).ToList();
        }

        IEnumerable<GamеsInListViewModel> IGamesService.GetPreviousGamesByTeam(int teamId)
        {
            return this.gamesRepository
                .AllAsNoTracking()
                .Where(x => x.HomeTeamId == teamId || x.AwayTeamId == teamId)
                .Where(x => x.StartingTime < DateTime.UtcNow)
                .OrderByDescending(x => x.StartingTime)
                .Select(x => new GamеsInListViewModel
                {
                    AwayTeamGoals = x.AwayGoals,
                    AwayTeamId = x.AwayTeamId,
                    AwayTeamName = x.AwayTeam.Name,
                    Id = x.Id,
                    HomeTeamGoals = x.HomeGoals,
                    HomeTeamId = x.HomeTeamId,
                    HomeTeamName = x.HomeTeam.Name,
                    StartingTime = x.StartingTime,
                    Odds = new OddsViewModel
                    {
                        AwayWinOdds = x.Odds.AwayWinOdds,
                        BTTSNoOdds = x.Odds.BTTSNoOdds,
                        BTTSYesOdds = x.Odds.BTTSYesOdds,
                        DrawOdds = x.Odds.DrawOdds,
                        HomeWinOdds = x.Odds.HomeWinOdds,
                        OverOdds = x.Odds.OverOdds,
                        UnderOdds = x.Odds.UnderOdds,
                    },
                }).ToList();
        }

        IEnumerable<GamеsInListViewModel> IGamesService.GetUpcomingGamesByTeam(int teamId)
        {
            return this.gamesRepository
                .AllAsNoTracking()
                .Where(x => x.HomeTeamId == teamId || x.AwayTeamId == teamId)
                .Where(x => x.StartingTime >= DateTime.UtcNow)
                .OrderBy(x => x.StartingTime)
                .Select(x => new GamеsInListViewModel
                {
                    AwayTeamGoals = x.AwayGoals,
                    AwayTeamId = x.AwayTeamId,
                    AwayTeamName = x.AwayTeam.Name,
                    Id = x.Id,
                    HomeTeamGoals = x.HomeGoals,
                    HomeTeamId = x.HomeTeamId,
                    HomeTeamName = x.HomeTeam.Name,
                    StartingTime = x.StartingTime,
                    Odds = new OddsViewModel
                    {
                        AwayWinOdds = x.Odds.AwayWinOdds,
                        BTTSNoOdds = x.Odds.BTTSNoOdds,
                        BTTSYesOdds = x.Odds.BTTSYesOdds,
                        DrawOdds = x.Odds.DrawOdds,
                        HomeWinOdds = x.Odds.HomeWinOdds,
                        OverOdds = x.Odds.OverOdds,
                        UnderOdds = x.Odds.UnderOdds,
                    },
                }).ToList();
        }
    }
}
