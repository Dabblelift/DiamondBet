namespace DiamondBet.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Teams;
    using Moq;
    using Xunit;

    public class TeamsServiceTests
    {
        private Mock<IDeletableEntityRepository<Game>> gamesRepository;

        private Mock<IDeletableEntityRepository<Team>> teamsRepository;

        public TeamsServiceTests()
        {
            this.GamesList = new List<Game>();
            this.TeamsList = new List<Team>();
            this.gamesRepository = new Mock<IDeletableEntityRepository<Game>>();
            this.gamesRepository.Setup(x => x.AllAsNoTracking()).Returns(this.GamesList.AsQueryable());
            this.teamsRepository = new Mock<IDeletableEntityRepository<Team>>();
            this.teamsRepository.Setup(x => x.All()).Returns(this.TeamsList.AsQueryable());
            this.teamsRepository.Setup(x => x.AllAsNoTracking()).Returns(this.TeamsList.AsQueryable());
            this.teamsRepository.Setup(x => x.AddAsync(It.IsAny<Team>())).Callback(
                (Team team) => this.TeamsList.Add(team));
            this.teamsRepository.Setup(x => x.Delete(It.IsAny<Team>())).Callback(
                (Team team) => this.TeamsList.Remove(team));
            this.TeamsService = new TeamsService(this.teamsRepository.Object, this.gamesRepository.Object);
        }

        private List<Game> GamesList { get; set; }

        private List<Team> TeamsList { get; set; }

        private TeamsService TeamsService { get; set; }

        [Fact]
        public async Task AddTeamAsyncWorksproperly()
        {
            var team1 = new AddTeamInputModel
            {
                Name = "Manchester United",
                YearFounded = 1878,
                Nickname = "The Red Devils",
                CountryId = 1,
            };

            var team2 = new AddTeamInputModel
            {
                Name = "Liverpool",
                CountryId = 1,
                Nickname = "The Reds",
                YearFounded = 1892,
            };

            await this.TeamsService.AddTeamAsync(team1);
            await this.TeamsService.AddTeamAsync(team2);

            Assert.Equal(2, this.TeamsList.Count);
        }

        [Fact]
        public async Task DeleteTeamShouldWorkProperly()
        {
            var team1 = new Team
            {
                Id = 1,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Manchester United",
                Nickname = "The Red Devils",
                YearFounded = 1878,
            };

            var team2 = new Team
            {
                Id = 2,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Liverpool",
                Nickname = "The Reds",
                YearFounded = 1892,
            };

            this.TeamsList.Add(team1);
            this.TeamsList.Add(team2);

            await this.TeamsService.DeleteTeamAsync(2);

            Assert.Single(this.TeamsList);
        }

        [Fact]
        public async Task EditTeamShouldWorkProperly()
        {
            var team1 = new Team
            {
                Id = 1,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Manchester United",
                Nickname = "The Red Devils",
                YearFounded = 1878,
            };

            var team2 = new Team
            {
                Id = 2,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Liverpool",
                Nickname = "The Reds",
                YearFounded = 1892,
            };

            this.TeamsList.Add(team1);
            this.TeamsList.Add(team2);

            var team1EditedData = new EditTeamInputModel
            {
                CountryId = 1,
                Name = "Manchester United",
                Nickname = "Manchester Reds",
                YearFounded = 1878,
            };

            await this.TeamsService.EditTeamAsync(team1EditedData, 1);

            Assert.Equal("Manchester Reds", TeamsList[0].Nickname);
        }

        [Fact]
        public void GetAllTeamsShouldWorkProperly()
        {
            var team1 = new Team
            {
                Id = 1,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Manchester United",
                Nickname = "The Red Devils",
                YearFounded = 1878,
            };

            var team2 = new Team
            {
                Id = 2,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Liverpool",
                Nickname = "The Reds",
                YearFounded = 1892,
            };

            this.TeamsList.Add(team1);
            this.TeamsList.Add(team2);

            var result = this.TeamsService.GetAllTeams();

            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void GetByIdShouldWorkProperly()
        {
            var team1 = new Team
            {
                Id = 1,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Manchester United",
                Nickname = "The Red Devils",
                YearFounded = 1878,
            };

            var team2 = new Team
            {
                Id = 2,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Liverpool",
                Nickname = "The Reds",
                YearFounded = 1892,
            };

            this.TeamsList.Add(team1);
            this.TeamsList.Add(team2);

            var result = this.TeamsService.GetById(2);

            Assert.Equal("Liverpool", result.Name);
        }

        [Fact]
        public void GetTeamDataForEditShouldWorkProperly()
        {
            var team1 = new Team
            {
                Id = 1,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Manchester United",
                Nickname = "The Red Devils",
                YearFounded = 1878,
            };

            var team2 = new Team
            {
                Id = 2,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Liverpool",
                Nickname = "The Reds",
                YearFounded = 1892,
            };

            this.TeamsList.Add(team1);
            this.TeamsList.Add(team2);

            var result = this.TeamsService.GetTeamDataForEdit(2);

            Assert.Equal("Liverpool", result.Name);
        }

        [Fact]
        public void GetTeamsByCountryShouldWorkProperly()
        {
            var team1 = new Team
            {
                Id = 1,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Manchester United",
                Nickname = "The Red Devils",
                YearFounded = 1878,
            };

            var team2 = new Team
            {
                Id = 2,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Liverpool",
                Nickname = "The Reds",
                YearFounded = 1892,
            };

            var team3 = new Team
            {
                Id = 3,
                CountryId = 2,
                Country = new Country { Name = "Spain" },
                Name = "Barcelona",
                Nickname = "Blaugranas",
                YearFounded = 1905,
            };

            this.TeamsList.Add(team1);
            this.TeamsList.Add(team2);
            this.TeamsList.Add(team3);

            var result = this.TeamsService.GetTeamsByCountry(1);

            Assert.Equal(2, result.ToList().Count);
        }
    }
}
