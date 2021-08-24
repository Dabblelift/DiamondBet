namespace DiamondBet.Services.Data.Tests
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
    using Moq;
    using Xunit;

    public class BetsServiceTests
    {
        private Mock<IDeletableEntityRepository<Bet>> betsRepository;
        private Mock<IDeletableEntityRepository<Odds>> oddsRepository;
        private Mock<IDeletableEntityRepository<ApplicationUser>> usersRepository;

        public BetsServiceTests()
        {
            this.BetsList = new List<Bet>();
            this.OddsList = new List<Odds>();
            this.UsersList = new List<ApplicationUser>();
            this.betsRepository = new Mock<IDeletableEntityRepository<Bet>>();
            this.betsRepository.Setup(x => x.All()).Returns(this.BetsList.AsQueryable());
            this.betsRepository.Setup(x => x.AllAsNoTracking()).Returns(this.BetsList.AsQueryable());
            this.betsRepository.Setup(x => x.AddAsync(It.IsAny<Bet>())).Callback(
                (Bet bet) => this.BetsList.Add(bet));
            this.oddsRepository = new Mock<IDeletableEntityRepository<Odds>>();
            this.oddsRepository.Setup(x => x.AllAsNoTracking()).Returns(this.OddsList.AsQueryable());
            this.usersRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            this.usersRepository.Setup(x => x.All()).Returns(this.UsersList.AsQueryable());
            this.BetsService = new BetsService(this.betsRepository.Object, this.oddsRepository.Object, this.usersRepository.Object);
        }

        private List<Bet> BetsList { get; set; }

        private List<Odds> OddsList { get; set; }

        private List<ApplicationUser> UsersList { get; set; }

        private BetsService BetsService { get; set; }

        [Fact]
        public async Task AddBetAsyncShouldRemoveTheStakeFromUserCoins()
        {
            var odds = new Odds
            {
                GameId = 1,
                Id = 1,
                AwayWinOdds = 1.90M,
                HomeWinOdds = 3.50M,
                DrawOdds = 3.30M,
                BTTSNoOdds = 1.95M,
                BTTSYesOdds = 1.95M,
                OverOdds = 1.70M,
                UnderOdds = 2.10M,
            };

            var user = new ApplicationUser
            {
                Coins = 1000,
                Id = "1",
            };

            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "AwayWin",
                Stake = 10,
            };

            this.OddsList.Add(odds);
            this.UsersList.Add(user);

            await this.BetsService.AddBetAsync(bet);

            Assert.Equal(990, this.UsersList[0].Coins);
        }

        [Fact]
        public async Task AddBetAsyncWorksProperlyOnHomeWinBet()
        {
            var odds = new Odds
            {
                GameId = 1,
                Id = 1,
                AwayWinOdds = 1.90M,
                HomeWinOdds = 3.50M,
                DrawOdds = 3.30M,
                BTTSNoOdds = 1.95M,
                BTTSYesOdds = 1.95M,
                OverOdds = 1.70M,
                UnderOdds = 2.10M,
            };

            var user = new ApplicationUser
            {
                Coins = 1000,
                Id = "1",
            };

            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "HomeWin",
                Stake = 10,
            };

            this.OddsList.Add(odds);
            this.UsersList.Add(user);

            await this.BetsService.AddBetAsync(bet);

            Assert.Equal(3.50M, this.BetsList[0].BetOdds);
            Assert.Equal(BetType.FullTimeResult, this.BetsList[0].BetType);
        }

        [Fact]
        public async Task AddBetAsyncWorksProperlyOnDrawBet()
        {
            var odds = new Odds
            {
                GameId = 1,
                Id = 1,
                AwayWinOdds = 1.90M,
                HomeWinOdds = 3.50M,
                DrawOdds = 3.30M,
                BTTSNoOdds = 1.95M,
                BTTSYesOdds = 1.95M,
                OverOdds = 1.70M,
                UnderOdds = 2.10M,
            };

            var user = new ApplicationUser
            {
                Coins = 1000,
                Id = "1",
            };

            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "Draw",
                Stake = 10,
            };

            this.OddsList.Add(odds);
            this.UsersList.Add(user);

            await this.BetsService.AddBetAsync(bet);

            Assert.Equal(3.30M, this.BetsList[0].BetOdds);
            Assert.Equal(BetType.FullTimeResult, this.BetsList[0].BetType);
        }

        [Fact]
        public async Task AddBetAsyncWorksProperlyOnAwayWinBet()
        {
            var odds = new Odds
            {
                GameId = 1,
                Id = 1,
                AwayWinOdds = 1.90M,
                HomeWinOdds = 3.50M,
                DrawOdds = 3.30M,
                BTTSNoOdds = 1.95M,
                BTTSYesOdds = 1.95M,
                OverOdds = 1.70M,
                UnderOdds = 2.10M,
            };

            var user = new ApplicationUser
            {
                Coins = 1000,
                Id = "1",
            };

            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "AwayWin",
                Stake = 10,
            };

            this.OddsList.Add(odds);
            this.UsersList.Add(user);

            await this.BetsService.AddBetAsync(bet);

            Assert.Equal(1.90M, this.BetsList[0].BetOdds);
            Assert.Equal(BetType.FullTimeResult, this.BetsList[0].BetType);
        }

        [Fact]
        public async Task AddBetAsyncWorksProperlyOnOverBet()
        {
            var odds = new Odds
            {
                GameId = 1,
                Id = 1,
                AwayWinOdds = 1.90M,
                HomeWinOdds = 3.50M,
                DrawOdds = 3.30M,
                BTTSNoOdds = 1.95M,
                BTTSYesOdds = 1.95M,
                OverOdds = 1.70M,
                UnderOdds = 2.10M,
            };

            var user = new ApplicationUser
            {
                Coins = 1000,
                Id = "1",
            };

            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "Over",
                Stake = 10,
            };

            this.OddsList.Add(odds);
            this.UsersList.Add(user);

            await this.BetsService.AddBetAsync(bet);

            Assert.Equal(1.70M, this.BetsList[0].BetOdds);
            Assert.Equal(BetType.MatchGoals, this.BetsList[0].BetType);
        }

        [Fact]
        public async Task AddBetAsyncWorksProperlyOnUnderBet()
        {
            var odds = new Odds
            {
                GameId = 1,
                Id = 1,
                AwayWinOdds = 1.90M,
                HomeWinOdds = 3.50M,
                DrawOdds = 3.30M,
                BTTSNoOdds = 1.95M,
                BTTSYesOdds = 1.95M,
                OverOdds = 1.70M,
                UnderOdds = 2.10M,
            };

            var user = new ApplicationUser
            {
                Coins = 1000,
                Id = "1",
            };

            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "Under",
                Stake = 10,
            };

            this.OddsList.Add(odds);
            this.UsersList.Add(user);

            await this.BetsService.AddBetAsync(bet);

            Assert.Equal(2.10M, this.BetsList[0].BetOdds);
            Assert.Equal(BetType.MatchGoals, this.BetsList[0].BetType);
        }

        [Fact]
        public async Task AddBetAsyncWorksProperlyOnBTTSYesBet()
        {
            var odds = new Odds
            {
                GameId = 1,
                Id = 1,
                AwayWinOdds = 1.90M,
                HomeWinOdds = 3.50M,
                DrawOdds = 3.30M,
                BTTSNoOdds = 1.95M,
                BTTSYesOdds = 1.95M,
                OverOdds = 1.70M,
                UnderOdds = 2.10M,
            };

            var user = new ApplicationUser
            {
                Coins = 1000,
                Id = "1",
            };

            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "BTTSYes",
                Stake = 10,
            };

            this.OddsList.Add(odds);
            this.UsersList.Add(user);

            await this.BetsService.AddBetAsync(bet);

            Assert.Equal(1.95M, this.BetsList[0].BetOdds);
            Assert.Equal(BetType.BothTeamsToScore, this.BetsList[0].BetType);
        }

        [Fact]
        public async Task AddBetAsyncWorksProperlyOnBTTSNoBet()
        {
            var odds = new Odds
            {
                GameId = 1,
                Id = 1,
                AwayWinOdds = 1.90M,
                HomeWinOdds = 3.50M,
                DrawOdds = 3.30M,
                BTTSNoOdds = 1.95M,
                BTTSYesOdds = 1.95M,
                OverOdds = 1.70M,
                UnderOdds = 2.10M,
            };

            var user = new ApplicationUser
            {
                Coins = 1000,
                Id = "1",
            };

            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "BTTSNo",
                Stake = 10,
            };

            this.OddsList.Add(odds);
            this.UsersList.Add(user);

            await this.BetsService.AddBetAsync(bet);

            Assert.Equal(1.95M, this.BetsList[0].BetOdds);
            Assert.Equal(BetType.BothTeamsToScore, this.BetsList[0].BetType);
        }

        [Fact]
        public void CheckIfValidShouldReturnFalseOnNoPrediction()
        {
            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "NoPrediction",
                Stake = 10,
            };

            var user = new ApplicationUser
            {
                Id = "1",
                Coins = 500,
            };

            var result = this.BetsService.CheckIfValid(bet, user);

            Assert.False(result);
        }

        [Fact]
        public void CheckIfValidShouldReturnFalseWhenStakeIsUnder1()
        {
            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "AwayWin",
                Stake = 0,
            };

            var user = new ApplicationUser
            {
                Id = "1",
                Coins = 2000,
            };

            var result = this.BetsService.CheckIfValid(bet, user);

            Assert.False(result);
        }

        [Fact]
        public void CheckIfValidShouldReturnFalseWhenStakeIsOver1000()
        {
            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "AwayWin",
                Stake = 1500,
            };

            var user = new ApplicationUser
            {
                Id = "1",
                Coins = 2000,
            };

            var result = this.BetsService.CheckIfValid(bet, user);

            Assert.False(result);
        }

        [Fact]
        public void CheckIfValidShouldReturnFalseWhenStakeIsMoreThanUserCoins()
        {
            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "AwayWin",
                Stake = 700,
            };

            var user = new ApplicationUser
            {
                Id = "1",
                Coins = 500,
            };

            var result = this.BetsService.CheckIfValid(bet, user);

            Assert.False(result);
        }

        [Fact]
        public void CheckIfValidShouldReturnTrueOnValidData()
        {
            var bet = new AddBetInputModel
            {
                UserId = "1",
                GameId = 1,
                Prediction = "HomeWin",
                Stake = 10,
            };

            var user = new ApplicationUser
            {
                Id = "1",
                Coins = 500,
            };

            var result = this.BetsService.CheckIfValid(bet, user);

            Assert.True(result);
        }

        [Fact]
        public void SettleBetsOnGameResultEditAsyncShouldWorkProperlyOnHomeWinOverAndBTTSYesWinningOdds()
        {
            var bet1 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.HomeWin,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 1.90M,
            };

            var bet2 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.Over,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 1.40M,
            };

            var bet3 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.Draw,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 3.30M,
            };

            var bet4 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.AwayWin,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 3.50M,
            };

            var user = new ApplicationUser
            {
                Id = "1",
                Coins = 500,
            };

            var game = new EditGameInputModel
            {
                AwayGoals = 1,
                HomeGoals = 3,
                AwayTeamId = 1,
                CompetitionId = 1,
                HomeTeamId = 2,
                StadiumId = 1,
            };

            this.BetsList.Add(bet1);
            this.BetsList.Add(bet2);
            this.BetsList.Add(bet3);
            this.BetsList.Add(bet4);

            this.UsersList.Add(user);

            var result = this.BetsService.SettleBetsOnGameResultEditAsync(game, 1);

            Assert.Equal(BetStatus.Won, this.BetsList[0].BetStatus);
            Assert.Equal(BetStatus.Won, this.BetsList[1].BetStatus);
            Assert.Equal(BetStatus.Lost, this.BetsList[2].BetStatus);
            Assert.Equal(BetStatus.Lost, this.BetsList[3].BetStatus);
            Assert.Equal(533, this.UsersList[0].Coins);
        }

        [Fact]
        public void SettleBetsOnGameResultEditAsyncShouldWorkProperlyOnAwayWinUnderAndBTTSNoOdds()
        {
            var bet1 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.HomeWin,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 1.90M,
            };

            var bet2 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.Over,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 1.40M,
            };

            var bet3 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.Draw,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 3.30M,
            };

            var bet4 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.AwayWin,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 3.50M,
            };

            var user = new ApplicationUser
            {
                Id = "1",
                Coins = 500,
            };

            var game = new EditGameInputModel
            {
                AwayGoals = 1,
                HomeGoals = 0,
                AwayTeamId = 1,
                CompetitionId = 1,
                HomeTeamId = 2,
                StadiumId = 1,
            };

            this.BetsList.Add(bet1);
            this.BetsList.Add(bet2);
            this.BetsList.Add(bet3);
            this.BetsList.Add(bet4);

            this.UsersList.Add(user);

            var result = this.BetsService.SettleBetsOnGameResultEditAsync(game, 1);

            Assert.Equal(BetStatus.Lost, this.BetsList[0].BetStatus);
            Assert.Equal(BetStatus.Lost, this.BetsList[1].BetStatus);
            Assert.Equal(BetStatus.Lost, this.BetsList[2].BetStatus);
            Assert.Equal(BetStatus.Won, this.BetsList[3].BetStatus);
            Assert.Equal(535, this.UsersList[0].Coins);
        }

        [Fact]
        public void SettleBetsOnGameResultEditAsyncShouldWorkProperlyOnDrawUnderAndBTTSNOOdds()
        {
            var bet1 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.HomeWin,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 1.90M,
            };

            var bet2 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.Over,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 1.40M,
            };

            var bet3 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.Draw,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 3.30M,
            };

            var bet4 = new Bet
            {
                UserId = "1",
                GameId = 1,
                Prediction = Prediction.AwayWin,
                Stake = 10,
                BetStatus = BetStatus.Unsettled,
                BetOdds = 3.50M,
            };

            var user = new ApplicationUser
            {
                Id = "1",
                Coins = 500,
            };

            var game = new EditGameInputModel
            {
                AwayGoals = 0,
                HomeGoals = 0,
                AwayTeamId = 1,
                CompetitionId = 1,
                HomeTeamId = 2,
                StadiumId = 1,
            };

            this.BetsList.Add(bet1);
            this.BetsList.Add(bet2);
            this.BetsList.Add(bet3);
            this.BetsList.Add(bet4);

            this.UsersList.Add(user);

            var result = this.BetsService.SettleBetsOnGameResultEditAsync(game, 1);

            Assert.Equal(BetStatus.Lost, this.BetsList[0].BetStatus);
            Assert.Equal(BetStatus.Lost, this.BetsList[1].BetStatus);
            Assert.Equal(BetStatus.Won, this.BetsList[2].BetStatus);
            Assert.Equal(BetStatus.Lost, this.BetsList[3].BetStatus);
            Assert.Equal(533, this.UsersList[0].Coins);
        }

        [Fact]
        public void GetAllBetsShouldWorkProperly()
        {
            var bet1 = new Bet
            {
                BetOdds = 1.40M,
                BetStatus = BetStatus.Unsettled,
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeam = new Team { Name = "Chelsea"},
                    AwayTeam = new Team { Name = "Arsenal"},
                    AwayGoals = 0,
                    HomeGoals = 2,
                    AwayTeamId = 2,
                    HomeTeamId = 1,
                },
                Id = "1",
                Prediction = Prediction.HomeWin,
                UserId = "1",
                User = new ApplicationUser { UserName = "TestUser" },
            };

            var bet2 = new Bet
            {
                BetOdds = 3.00M,
                BetStatus = BetStatus.Unsettled,
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeam = new Team { Name = "Chelsea" },
                    AwayTeam = new Team { Name = "Arsenal" },
                    AwayGoals = 0,
                    HomeGoals = 2,
                    AwayTeamId = 2,
                    HomeTeamId = 1,
                },
                Id = "1",
                Prediction = Prediction.Draw,
                UserId = "1",
                User = new ApplicationUser { UserName = "TestUser" },
            };

            var bet3 = new Bet
            {
                BetOdds = 2.00M,
                BetStatus = BetStatus.Unsettled,
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeam = new Team { Name = "Chelsea" },
                    AwayTeam = new Team { Name = "Arsenal" },
                    AwayGoals = 0,
                    HomeGoals = 2,
                    AwayTeamId = 2,
                    HomeTeamId = 1,
                },
                Id = "1",
                Prediction = Prediction.Over,
                UserId = "1",
                User = new ApplicationUser { UserName = "TestUser" },
            };

            this.BetsList.Add(bet1);
            this.BetsList.Add(bet2);
            this.BetsList.Add(bet3);

            var result = this.BetsService.GetAllBets();

            Assert.Equal(3, result.ToList().Count);
        }

        [Fact]
        public void GetBetsByUserIdShouldWorkProperly()
        {
            var bet1 = new Bet
            {
                BetOdds = 1.40M,
                BetStatus = BetStatus.Unsettled,
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeam = new Team { Name = "Chelsea" },
                    AwayTeam = new Team { Name = "Arsenal" },
                    AwayGoals = 0,
                    HomeGoals = 2,
                    AwayTeamId = 2,
                    HomeTeamId = 1,
                },
                Id = "1",
                Prediction = Prediction.HomeWin,
                UserId = "1",
                User = new ApplicationUser { UserName = "TestUser" },
            };

            var bet2 = new Bet
            {
                BetOdds = 3.00M,
                BetStatus = BetStatus.Unsettled,
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeam = new Team { Name = "Chelsea" },
                    AwayTeam = new Team { Name = "Arsenal" },
                    AwayGoals = 0,
                    HomeGoals = 2,
                    AwayTeamId = 2,
                    HomeTeamId = 1,
                },
                Id = "1",
                Prediction = Prediction.Draw,
                UserId = "1",
                User = new ApplicationUser { UserName = "TestUser" },
            };

            var bet3 = new Bet
            {
                BetOdds = 2.00M,
                BetStatus = BetStatus.Unsettled,
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeam = new Team { Name = "Chelsea" },
                    AwayTeam = new Team { Name = "Arsenal" },
                    AwayGoals = 0,
                    HomeGoals = 2,
                    AwayTeamId = 2,
                    HomeTeamId = 1,
                },
                Id = "1",
                Prediction = Prediction.Over,
                UserId = "2",
                User = new ApplicationUser { UserName = "TestUser2" },
            };

            this.BetsList.Add(bet1);
            this.BetsList.Add(bet2);
            this.BetsList.Add(bet3);

            var result = this.BetsService.GetBetsByUserId("1");

            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void GetBetByIdShouldWorkProperly()
        {
            var bet1 = new Bet
            {
                BetOdds = 1.40M,
                BetStatus = BetStatus.Unsettled,
                BetType = BetType.FullTimeResult,
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeam = new Team { Name = "Chelsea" },
                    AwayTeam = new Team { Name = "Arsenal" },
                    AwayGoals = 0,
                    HomeGoals = 2,
                    AwayTeamId = 2,
                    HomeTeamId = 1,
                },
                Id = "1",
                Prediction = Prediction.HomeWin,
                UserId = "1",
                User = new ApplicationUser { UserName = "TestUser" },
                Stake = 10,
            };

            var bet2 = new Bet
            {
                BetOdds = 3.00M,
                BetStatus = BetStatus.Unsettled,
                BetType = BetType.FullTimeResult,
                GameId = 1,
                Game = new Game
                {
                    Id = 1,
                    HomeTeam = new Team { Name = "Chelsea" },
                    AwayTeam = new Team { Name = "Arsenal" },
                    AwayGoals = 0,
                    HomeGoals = 2,
                    AwayTeamId = 2,
                    HomeTeamId = 1,
                },
                Id = "1",
                Prediction = Prediction.Draw,
                UserId = "1",
                User = new ApplicationUser { UserName = "TestUser" },
                Stake = 10,
            };

            this.BetsList.Add(bet1);
            this.BetsList.Add(bet2);

            var result = this.BetsService.GetById("1");

            Assert.Equal("HomeWin", result.Prediction.ToString());
        }
    }
}
