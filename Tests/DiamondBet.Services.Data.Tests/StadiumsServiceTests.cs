namespace DiamondBet.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Common.Repositories;
    using DiamondBet.Data.Models;
    using DiamondBet.Web.ViewModels.Stadiums;
    using Moq;
    using Xunit;

    public class StadiumsServiceTests
    {
        private Mock<IDeletableEntityRepository<Game>> gamesRepository;
        private Mock<IDeletableEntityRepository<Stadium>> stadiumsRepository;

        public StadiumsServiceTests()
        {
            this.GamesList = new List<Game>();
            this.StadiumsList = new List<Stadium>();
            this.gamesRepository = new Mock<IDeletableEntityRepository<Game>>();
            this.gamesRepository.Setup(x => x.AllAsNoTracking()).Returns(this.GamesList.AsQueryable());
            this.stadiumsRepository = new Mock<IDeletableEntityRepository<Stadium>>();
            this.stadiumsRepository.Setup(x => x.AddAsync(It.IsAny<Stadium>())).Callback(
                (Stadium stadium) => this.StadiumsList.Add(stadium));
            this.stadiumsRepository.Setup(x => x.Delete(It.IsAny<Stadium>())).Callback(
                (Stadium stadium) => this.StadiumsList.Remove(stadium));
            this.stadiumsRepository.Setup(x => x.All()).Returns(this.StadiumsList.AsQueryable());
            this.stadiumsRepository.Setup(x => x.AllAsNoTracking()).Returns(this.StadiumsList.AsQueryable());
            this.StadiumsService = new StadiumsService(this.stadiumsRepository.Object, this.gamesRepository.Object);
        }

        private List<Stadium> StadiumsList { get; set; }

        private List<Game> GamesList { get; set; }

        private StadiumsService StadiumsService { get; set; }

        [Fact]
        public async Task AddStadiumAsyncShouldWorkProperly()
        {
            var stadium = new AddStadiumInputModel
            {
                Capacity = 90000,
                CountryId = 1,
                Name = "Wembley",
                YearFounded = 2007,
            };

            await this.StadiumsService.AddStadiumAsync(stadium);

            Assert.Single(this.StadiumsList);
        }

        [Fact]
        public async Task DeleteStadiumAsyncShouldWorkProperly()
        {
            var stadium = new Stadium
            {
                Capacity = 90000,
                CountryId = 1,
                Name = "Wembley",
                YearFounded = 2007,
                Id = 1,
            };

            StadiumsList.Add(stadium);

            await this.StadiumsService.DeleteStadiumAsync(1);

            Assert.Empty(this.StadiumsList);
        }

        [Fact]
        public async Task EditStadiumAsyncShouldWorkProperly()
        {
            var stadium = new Stadium
            {
                Id = 1,
                Capacity = 85000,
                CountryId = 1,
                Name = "Wembley",
                YearFounded = 2007,
            };

            this.StadiumsList.Add(stadium);

            var stadiumInputModel = new EditStadiumInputModel
            {
                Name = "Wembley",
                Capacity = 90000,
                YearFounded = 2007,
                CountryId = 1,
            };

            await this.StadiumsService.EditStadiumAsync(stadiumInputModel, 1);

            Assert.Equal(90000, this.StadiumsList[0].Capacity);
        }

        [Fact]
        public void GetStadiumDataForEditShouldWorkProperly()
        {
            var stadium = new Stadium
            {
                Id = 1,
                Capacity = 85000,
                CountryId = 1,
                Name = "Wembley",
                YearFounded = 2007,
            };

            this.StadiumsList.Add(stadium);

            var result = this.StadiumsService.GetStadiumDataForEdit(1);

            Assert.Equal("Wembley", result.Name);
        }

        [Fact]
        public void GetAllStadiumsShouldWorkProperly()
        {
            var stadium1 = new Stadium
            {
                Id = 1,
                Capacity = 90000,
                CountryId = 1,
                Name = "Wembley",
                YearFounded = 2007,
            };

            var stadium2 = new Stadium
            {
                Id = 2,
                Capacity = 75000,
                CountryId = 1,
                Name = "Old Trafford",
                YearFounded = 1908,
            };

            this.StadiumsList.Add(stadium1);
            this.StadiumsList.Add(stadium2);

            var result = this.StadiumsService.GetAllStadiums();

            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void GetByIdShouldWorkProperly()
        {
            var stadium1 = new Stadium
            {
                Id = 1,
                Capacity = 90000,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Wembley",
                YearFounded = 2007,
            };

            var stadium2 = new Stadium
            {
                Id = 2,
                Capacity = 75000,
                CountryId = 1,
                Country = new Country { Name = "England" },
                Name = "Old Trafford",
                YearFounded = 1908,
            };

            this.StadiumsList.Add(stadium1);
            this.StadiumsList.Add(stadium2);

            var result = this.StadiumsService.GetById(2);

            Assert.Equal("Old Trafford", result.Name);
        }
    }
}
