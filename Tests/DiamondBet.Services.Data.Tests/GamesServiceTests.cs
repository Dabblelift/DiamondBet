namespace DiamondBet.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Games;
    using Moq;
    using Xunit;

    public class GamesServiceTests
    {
        private Mock<IDeletableEntityRepository<Game>> gamesRepository;

        private Mock<IDeletableEntityRepository<Odds>> oddsRepository;

        public GamesServiceTests()
        {
            this.GamesList = new List<Game>();
            this.OddsList = new List<Odds>();
            this.gamesRepository = new Mock<IDeletableEntityRepository<Game>>();
            this.gamesRepository.Setup(x => x.AddAsync(It.IsAny<Game>())).Callback(
                (Game game) => this.GamesList.Add(game));
            this.gamesRepository.Setup(x => x.All()).Returns(this.GamesList.AsQueryable());
            this.gamesRepository.Setup(x => x.AllAsNoTracking()).Returns(this.GamesList.AsQueryable());
            this.gamesRepository.Setup(x => x.Delete(It.IsAny<Game>())).Callback(
                (Game game) => this.GamesList.Remove(game));
            this.oddsRepository = new Mock<IDeletableEntityRepository<Odds>>();
            this.oddsRepository.Setup(x => x.All()).Returns(this.OddsList.AsQueryable());
            this.GamesService = new GamesService(this.gamesRepository.Object, this.oddsRepository.Object);
        }

        private List<Game> GamesList { get; set; }

        private List<Odds> OddsList { get; set; }

        private GamesService GamesService { get; set; }

        [Fact]
        public async Task AddGamesShouldWorkProperly()
        {
            var pastGame = new AddGameInputModel
            {
                HomeTeamId = 1,
                AwayTeamId = 2,
                CompetitionId = 1,
                StadiumId = 1,
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = new OddsInputModel
                {
                    HomeWinOdds = 1.80M,
                    DrawOdds = 3.00M,
                    AwayWinOdds = 3.50M,
                    OverOdds = 1.95M,
                    UnderOdds = 1.95M,
                    BTTSNoOdds = 1.90M,
                    BTTSYesOdds = 2.00M,
                },
            };

            var upcomingGame = new AddGameInputModel
            {
                HomeTeamId = 4,
                AwayTeamId = 5,
                CompetitionId = 3,
                StadiumId = 5,
                StartingTime = DateTime.Now.AddDays(3),
                Odds = new OddsInputModel
                {
                    HomeWinOdds = 1.80M,
                    DrawOdds = 3.00M,
                    AwayWinOdds = 3.50M,
                    OverOdds = 1.95M,
                    UnderOdds = 1.95M,
                    BTTSNoOdds = 1.90M,
                    BTTSYesOdds = 2.00M,
                },
            };

            await this.GamesService.AddGameAsync(pastGame);
            await this.GamesService.AddGameAsync(upcomingGame);

            Assert.Equal(2, this.GamesList.Count);
        }

        [Fact]
        public async Task DeleteGameShouldWorkProperly()
        {
            var pastGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                CompetitionId = 1,
                StadiumId = 1,
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = new Odds
                {
                    HomeWinOdds = 1.80M,
                    DrawOdds = 3.00M,
                    AwayWinOdds = 3.50M,
                    OverOdds = 1.95M,
                    UnderOdds = 1.95M,
                    BTTSNoOdds = 1.90M,
                    BTTSYesOdds = 2.00M,
                },
            };

            var upcomingGame = new Game
            {
                Id = 2,
                HomeTeamId = 4,
                AwayTeamId = 5,
                CompetitionId = 3,
                StadiumId = 5,
                StartingTime = DateTime.Now.AddDays(3),
                Odds = new Odds
                {
                    HomeWinOdds = 1.80M,
                    DrawOdds = 3.00M,
                    AwayWinOdds = 3.50M,
                    OverOdds = 1.95M,
                    UnderOdds = 1.95M,
                    BTTSNoOdds = 1.90M,
                    BTTSYesOdds = 2.00M,
                },
            };

            this.GamesList.Add(pastGame);
            this.GamesList.Add(upcomingGame);

            await this.GamesService.DeleteGameAsync(1);

            Assert.Single(this.GamesList);
        }

        [Fact]
        public async Task EditGameShouldWorkProperly()
        {
            var pastGameOdds = new Odds
            {
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var pastGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                AwayTeamId = 2,
                CompetitionId = 1,
                StadiumId = 1,
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = pastGameOdds,
            };

            var upcomingGameOdds = new Odds
            {
                GameId = 2,
                Game = new Game
                {
                    Id = 2,
                    HomeTeamId = 4,
                    AwayTeamId = 5,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var upcomingGame = new Game
            {
                Id = 2,
                HomeTeamId = 4,
                AwayTeamId = 5,
                CompetitionId = 3,
                StadiumId = 5,
                StartingTime = DateTime.Now.AddDays(3),
                Odds = upcomingGameOdds,
            };

            this.GamesList.Add(pastGame);
            this.GamesList.Add(upcomingGame);

            this.OddsList.Add(upcomingGameOdds);
            this.OddsList.Add(pastGameOdds);

            var gameEditedData = new EditGameInputModel
            {
                HomeTeamId = 10,
                AwayTeamId = 2,
                CompetitionId = 1,
                StadiumId = 1,
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = new OddsInputModel
                {
                    HomeWinOdds = 1.80M,
                    DrawOdds = 3.00M,
                    AwayWinOdds = 3.50M,
                    OverOdds = 1.95M,
                    UnderOdds = 1.95M,
                    BTTSNoOdds = 1.90M,
                    BTTSYesOdds = 2.00M,
                },
            };

            await this.GamesService.EditGameAsync(gameEditedData, 1);

            Assert.Equal(gameEditedData.HomeTeamId, this.GamesList[0].HomeTeamId);
        }

        [Fact]
        public void GetGameDataForEditShouldWorkProperly()
        {
            var upcomingGameOdds = new Odds
            {
                GameId = 2,
                Game = new Game
                {
                    Id = 2,
                    HomeTeamId = 4,
                    AwayTeamId = 5,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };
            var upcomingGame = new Game
            {
                Id = 2,
                HomeTeamId = 4,
                AwayTeamId = 5,
                CompetitionId = 3,
                StadiumId = 5,
                StartingTime = DateTime.Now.AddDays(3),
                AwayGoals = 0,
                HomeGoals = 1,
                Odds = upcomingGameOdds,
            };

            this.GamesList.Add(upcomingGame);
            this.OddsList.Add(upcomingGameOdds);

            var result = this.GamesService.GetGameDataForEdit(2);

            Assert.Equal(4, result.HomeTeamId);
        }

        [Fact]

        public void GetAllUpcomingGamesShouldWorkProperly()
        {
            var pastGameOdds = new Odds
            {
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var pastGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                HomeTeam = new Team { Id = 1, Name = "Barcelona" },
                AwayTeamId = 2,
                AwayTeam = new Team { Id = 2, Name = "Real Madrid" },
                CompetitionId = 1,
                Competition = new Competition { Id = 1, Name = "La Liga" },
                StadiumId = 1,
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = pastGameOdds,
            };

            var upcomingGameOdds = new Odds
            {
                GameId = 2,
                Game = new Game
                {
                    Id = 2,
                    HomeTeamId = 4,
                    AwayTeamId = 5,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var upcomingGame = new Game
            {
                Id = 2,
                HomeTeamId = 4,
                HomeTeam = new Team { Id = 4, Name = "Manchester United" },
                AwayTeamId = 5,
                AwayTeam = new Team { Id = 5, Name = "Chelsea" },
                CompetitionId = 3,
                Competition = new Competition { Id = 3, Name = "Premier League"},
                StadiumId = 5,
                StartingTime = DateTime.Now.AddDays(3),
                Odds = upcomingGameOdds,
            };

            this.GamesList.Add(pastGame);
            this.GamesList.Add(upcomingGame);

            this.OddsList.Add(upcomingGameOdds);
            this.OddsList.Add(pastGameOdds);

            var result = this.GamesService.GetAllUpcomingGames();

            Assert.Single(result);
            Assert.Equal(5, result.First().AwayTeamId);
        }

        [Fact]

        public void GetPreviousGamesShouldWorkProperly()
        {
            var pastGameOdds = new Odds
            {
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var pastGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                HomeTeam = new Team { Id = 1, Name = "Barcelona" },
                AwayTeamId = 2,
                AwayTeam = new Team { Id = 2, Name = "Real Madrid" },
                CompetitionId = 1,
                Competition = new Competition { Id = 1, Name = "La Liga" },
                StadiumId = 1,
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = pastGameOdds,
            };

            var upcomingGameOdds = new Odds
            {
                GameId = 2,
                Game = new Game
                {
                    Id = 2,
                    HomeTeamId = 4,
                    AwayTeamId = 5,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var upcomingGame = new Game
            {
                Id = 2,
                HomeTeamId = 4,
                HomeTeam = new Team { Id = 4, Name = "Manchester United" },
                AwayTeamId = 5,
                AwayTeam = new Team { Id = 5, Name = "Chelsea" },
                CompetitionId = 3,
                Competition = new Competition { Id = 3, Name = "Premier League" },
                StadiumId = 5,
                StartingTime = DateTime.Now.AddDays(3),
                Odds = upcomingGameOdds,
            };

            this.GamesList.Add(pastGame);
            this.GamesList.Add(upcomingGame);

            this.OddsList.Add(upcomingGameOdds);
            this.OddsList.Add(pastGameOdds);

            var result = this.GamesService.GetPreviousGames();

            Assert.Single(result);
            Assert.Equal(2, result.First().AwayTeamId);
        }

        [Fact]
        public void GetUpcomingGamesTodayShouldOnlyReturnUpcomingGamesInNext24Hours()
        {
            var pastGameOdds = new Odds
            {
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var pastGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                HomeTeam = new Team { Id = 1, Name = "Barcelona" },
                AwayTeamId = 2,
                AwayTeam = new Team { Id = 2, Name = "Real Madrid" },
                CompetitionId = 1,
                Competition = new Competition { Id = 1, Name = "La Liga" },
                StadiumId = 1,
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = pastGameOdds,
            };

            var upcomingGameOdds = new Odds
            {
                GameId = 2,
                Game = new Game
                {
                    Id = 2,
                    HomeTeamId = 4,
                    AwayTeamId = 5,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var upcomingGame = new Game
            {
                Id = 2,
                HomeTeamId = 4,
                HomeTeam = new Team { Id = 4, Name = "Manchester United" },
                AwayTeamId = 5,
                AwayTeam = new Team { Id = 5, Name = "Chelsea" },
                CompetitionId = 3,
                Competition = new Competition { Id = 3, Name = "Premier League" },
                StadiumId = 5,
                StartingTime = DateTime.Now.AddDays(3),
                Odds = upcomingGameOdds,
            };

            var todayGameOdds = new Odds
            {
                GameId = 3,
                Game = new Game
                {
                    Id = 3,
                    HomeTeamId = 10,
                    AwayTeamId = 11,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var todayGame = new Game
            {
                Id = 3,
                HomeTeamId = 10,
                HomeTeam = new Team { Id = 10, Name = "Inter" },
                AwayTeamId = 11,
                AwayTeam = new Team { Id = 11, Name = "Milan" },
                CompetitionId = 4,
                Competition = new Competition { Id = 4, Name = "Serie A" },
                StadiumId = 5,
                StartingTime = DateTime.Now.AddHours(10),
                Odds = upcomingGameOdds,
            };

            this.GamesList.Add(pastGame);
            this.GamesList.Add(upcomingGame);
            this.GamesList.Add(todayGame);

            this.OddsList.Add(upcomingGameOdds);
            this.OddsList.Add(pastGameOdds);
            this.OddsList.Add(todayGameOdds);

            var result = this.GamesService.GetUpcomingGamesToday();

            Assert.Single(result);
            Assert.Equal(11, result.First().AwayTeamId);
        }

        [Fact]
        public void GetPreviousGamesByTeamShouldWorkProperly()
        {
            var pastGameOdds = new Odds
            {
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var pastGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                HomeTeam = new Team { Id = 1, Name = "Barcelona" },
                HomeGoals = 1,
                AwayGoals = 1,
                AwayTeamId = 2,
                AwayTeam = new Team { Id = 2, Name = "Real Madrid" },
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = pastGameOdds,
            };

            var upcomingGameOdds = new Odds
            {
                GameId = 2,
                Game = new Game
                {
                    Id = 2,
                    HomeTeamId = 1,
                    AwayTeamId = 3,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var upcomingGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                HomeTeam = new Team { Id = 1, Name = "Barcelona" },
                HomeGoals = 1,
                AwayGoals = 1,
                AwayTeamId = 3,
                AwayTeam = new Team { Id = 2, Name = "Atletico Madrid" },
                StartingTime = DateTime.Now.AddDays(3),
                Odds = upcomingGameOdds,
            };

            this.GamesList.Add(pastGame);
            this.GamesList.Add(upcomingGame);

            this.OddsList.Add(upcomingGameOdds);
            this.OddsList.Add(pastGameOdds);

            var result = this.GamesService.GetPreviousGamesByTeam(1);

            Assert.Single(result);
            Assert.Equal(2, result.First().AwayTeamId);
        }

        [Fact]
        public void GetUpcomingGamesByTeamShouldWorkProperly()
        {
            var pastGameOdds = new Odds
            {
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var pastGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                HomeTeam = new Team { Id = 1, Name = "Barcelona" },
                HomeGoals = 1,
                AwayGoals = 1,
                AwayTeamId = 2,
                AwayTeam = new Team { Id = 2, Name = "Real Madrid" },
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = pastGameOdds,
            };

            var upcomingGameOdds = new Odds
            {
                GameId = 2,
                Game = new Game
                {
                    Id = 2,
                    HomeTeamId = 1,
                    AwayTeamId = 3,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var upcomingGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                HomeTeam = new Team { Id = 1, Name = "Barcelona" },
                HomeGoals = 1,
                AwayGoals = 1,
                AwayTeamId = 3,
                AwayTeam = new Team { Id = 2, Name = "Atletico Madrid" },
                StartingTime = DateTime.Now.AddDays(3),
                Odds = upcomingGameOdds,
            };

            this.GamesList.Add(pastGame);
            this.GamesList.Add(upcomingGame);

            this.OddsList.Add(upcomingGameOdds);
            this.OddsList.Add(pastGameOdds);

            var result = this.GamesService.GetUpcomingGamesByTeam(1);

            Assert.Single(result);
            Assert.Equal(3, result.First().AwayTeamId);
        }

        [Fact]
        public void GetByIdShouldWorkProperly()
        {
            var pastGameOdds = new Odds
            {
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var pastGame = new Game
            {
                Id = 1,
                HomeTeamId = 1,
                HomeTeam = new Team { Id = 1, Name = "Barcelona" },
                HomeGoals = 1,
                AwayGoals = 1,
                AwayTeamId = 2,
                AwayTeam = new Team { Id = 2, Name = "Real Madrid" },
                CompetitionId = 1,
                Competition = new Competition { Name = "La Liga" },
                StadiumId = 1,
                Stadium = new Stadium { Name = "Camp Nou" },
                StartingTime = DateTime.ParseExact("05/05/2021 14:30", "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                Odds = pastGameOdds,
            };

            var upcomingGameOdds = new Odds
            {
                GameId = 2,
                Game = new Game
                {
                    Id = 2,
                    HomeTeamId = 1,
                    AwayTeamId = 4,
                },
                HomeWinOdds = 1.80M,
                DrawOdds = 3.00M,
                AwayWinOdds = 3.50M,
                OverOdds = 1.95M,
                UnderOdds = 1.95M,
                BTTSNoOdds = 1.90M,
                BTTSYesOdds = 2.00M,
            };

            var upcomingGame = new Game
            {
                Id = 2,
                HomeTeamId = 1,
                HomeTeam = new Team { Id = 1, Name = "Barcelona" },
                HomeGoals = 1,
                AwayGoals = 1,
                AwayTeamId = 4,
                AwayTeam = new Team { Id = 4, Name = "Villarreal" },
                CompetitionId = 1,
                Competition = new Competition { Name = "La Liga" },
                StadiumId = 1,
                Stadium = new Stadium { Name = "Camp Nou" },
                StartingTime = DateTime.Now.AddDays(3),
                Odds = pastGameOdds,
            };

            this.GamesList.Add(pastGame);
            this.GamesList.Add(upcomingGame);

            this.OddsList.Add(upcomingGameOdds);
            this.OddsList.Add(pastGameOdds);

            var result = this.GamesService.GetById(2);

            Assert.Equal(4, result.AwayTeamId);
        }
    }
}
