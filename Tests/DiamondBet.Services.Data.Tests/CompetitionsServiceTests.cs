namespace DiamondBet.Services.Data.Tests
{
    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Competitions;
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class CompetitionsServiceTests
    {
        private Mock<IDeletableEntityRepository<Game>> gamesRepository;
        private Mock<IDeletableEntityRepository<Competition>> competitionsRepository;

        public CompetitionsServiceTests()
        {
            this.GamesList = new List<Game>();
            this.CompetitionsList = new List<Competition>();
            this.gamesRepository = new Mock<IDeletableEntityRepository<Game>>();
            this.gamesRepository.Setup(x => x.AllAsNoTracking()).Returns(this.GamesList.AsQueryable());
            this.competitionsRepository = new Mock<IDeletableEntityRepository<Competition>>();
            this.competitionsRepository.Setup(x => x.AddAsync(It.IsAny<Competition>())).Callback(
                (Competition competition) => this.CompetitionsList.Add(competition));
            this.competitionsRepository.Setup(x => x.Delete(It.IsAny<Competition>())).Callback(
                (Competition competition) => this.CompetitionsList.Remove(competition));
            this.competitionsRepository.Setup(x => x.All()).Returns(this.CompetitionsList.AsQueryable());
            this.competitionsRepository.Setup(x => x.AllAsNoTracking()).Returns(this.CompetitionsList.AsQueryable());
            this.CompetitionsService = new CompetitionsService(this.competitionsRepository.Object, this.gamesRepository.Object);
        }

        private List<Competition> CompetitionsList { get; set; }

        private List<Game> GamesList { get; set; }

        private CompetitionsService CompetitionsService { get; set; }

        [Fact]
        public async Task AddCompetitionAsyncShouldWorkProperly()
        {
            var competition = new AddCompetitionInputModel
            {
                Name = "Premier League",
                NumberOfParticipants = 20,
                CountryId = 1,
            };

            await this.CompetitionsService.AddCompetitionAsync(competition);

            Assert.Single(this.CompetitionsList);
        }

        [Fact]
        public async Task DeleteCompetitionAsyncShouldWorkProperly()
        {
            var competition = new Competition
            {
                Name = "Premier League",
                NumberOfParticipants = 20,
                CountryId = 1,
                Id = 1,
            };

            this.CompetitionsList.Add(competition);

            await this.CompetitionsService.DeleteCompetitionAsync(1);

            Assert.Empty(this.CompetitionsList);
        }

        [Fact]
        public async Task EditCompetitionAsyncShouldWorkProperly()
        {
            var competition = new Competition
            {
                Name = "La Liga",
                CountryId = 2,
                NumberOfParticipants = 20,
                Id = 2,
            };

            this.CompetitionsList.Add(competition);

            var competitionInputModel = new EditCompetitionInputModel
            {
                Name = "La Liga",
                CountryId = 3,
                NumberOfParticipants = 20,
            };

            await this.CompetitionsService.EditCompetitionAsync(competitionInputModel, 2);

            Assert.Equal(3, this.CompetitionsList[0].CountryId);
        }

        [Fact]
        public void GetAllCompetitionsShouldWorkProperly()
        {
            var competition1 = new Competition
            {
                Name = "Premier League",
                NumberOfParticipants = 20,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Id = 1,
            };

            var competition2 = new Competition
            {
                Name = "La Liga",
                NumberOfParticipants = 20,
                CountryId = 2,
                Country = new Country { Name = "Spain" },
                Id = 2,
            };

            this.CompetitionsList.Add(competition1);
            this.CompetitionsList.Add(competition2);

            var result = this.CompetitionsService.GetAllCompetitions();

            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void GetByIdShouldWorkProperly()
        {
            var competition1 = new Competition
            {
                Name = "Premier League",
                NumberOfParticipants = 20,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Id = 1,
            };

            var competition2 = new Competition
            {
                Name = "La Liga",
                NumberOfParticipants = 20,
                CountryId = 2,
                Country = new Country { Name = "Spain" },
                Id = 2,
            };

            this.CompetitionsList.Add(competition1);
            this.CompetitionsList.Add(competition2);

            var result = this.CompetitionsService.GetById(1);

            Assert.Equal("Premier League", result.Name);
        }

        [Fact]
        public void GetCompetitionsDataForEditShouldWorkProperly()
        {
            var competition1 = new Competition
            {
                Name = "Premier League",
                NumberOfParticipants = 20,
                CountryId = 1,
                Id = 1,
            };

            this.CompetitionsList.Add(competition1);

            var result = this.CompetitionsService.GetCompetitionDataForEdit(1);

            Assert.Equal("Premier League", result.Name);
        }
    }
}
