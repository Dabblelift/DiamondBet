namespace DiamondBet.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Data.Models;

    public class BettingDataSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Games.Any())
            {
                return;
            }

            await dbContext.Countries.AddAsync(new Country { Name = "England", Capital = "London" });
            await dbContext.Countries.AddAsync(new Country { Name = "Spain", Capital = "Madrid" });
            await dbContext.Countries.AddAsync(new Country { Name = "Germany", Capital = "Berlin" });

            await dbContext.SaveChangesAsync();

            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Manchester United",
                Nickname = "The Red Devils",
                YearFounded = 1878,
                CountryId = 1,
            });
            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Chelsea",
                Nickname = "The Blues",
                YearFounded = 1905,
                CountryId = 1,
            });
            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Liverpool",
                Nickname = "The Reds",
                YearFounded = 1892,
                CountryId = 1,
            });
            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Manchester City",
                Nickname = "The Citizens",
                YearFounded = 1880,
                CountryId = 1,
            });
            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Barcelona",
                Nickname = "Blaugranas",
                YearFounded = 1899,
                CountryId = 2,
            });
            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Real Madrid",
                Nickname = "Los Merengues",
                YearFounded = 1902,
                CountryId = 2,
            });
            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Atletico Madrid",
                Nickname = "Los Colchoneros",
                YearFounded = 1903,
                CountryId = 2,
            });
            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Bayern Munich",
                Nickname = "Die Bayern",
                YearFounded = 1900,
                CountryId = 3,
            });
            await dbContext.Teams.AddAsync(new Team
            {
                Name = "Borussia Dortmund",
                Nickname = "Die Borussen",
                YearFounded = 1909,
                CountryId = 3,
            });

            await dbContext.Stadiums.AddAsync(new Stadium
            {
                Name = "Old Trafford",
                YearFounded = 1910,
                Capacity = 74140,
                CountryId = 1,
            });
            await dbContext.Stadiums.AddAsync(new Stadium
            {
                Name = "Camp Nou",
                YearFounded = 1957,
                Capacity = 99354,
                CountryId = 2,
            });
            await dbContext.Stadiums.AddAsync(new Stadium
            {
                Name = "Santiago Bernabeu",
                YearFounded = 1947,
                Capacity = 81044,
                CountryId = 2,
            });
            await dbContext.Stadiums.AddAsync(new Stadium
            {
                Name = "Signal Iduna Park",
                YearFounded = 1974,
                Capacity = 81365,
                CountryId = 3,
            });

            await dbContext.Competitions.AddAsync(new Competition
            {
                Name = "Premier League",
                NumberOfParticipants = 20,
                CountryId = 1,
            });
            await dbContext.Competitions.AddAsync(new Competition
            {
                Name = "La Liga",
                NumberOfParticipants = 20,
                CountryId = 2,
            });
            await dbContext.Competitions.AddAsync(new Competition
            {
                Name = "Bundesliga",
                NumberOfParticipants = 18,
                CountryId = 3,
            });

            await dbContext.SaveChangesAsync();

            await dbContext.Games.AddAsync(new Game
            {
                StartingTime = new DateTime(2021, 12, 12, 18, 00, 00),
                HomeTeamId = 1,
                AwayTeamId = 2,
                CompetitionId = 1,
                StadiumId = 1,
            });
            await dbContext.Games.AddAsync(new Game
            {
                StartingTime = new DateTime(2021, 8, 5, 16, 30, 00),
                HomeTeamId = 4,
                AwayTeamId = 5,
                CompetitionId = 2,
                StadiumId = 2,
            });
            await dbContext.Games.AddAsync(new Game
            {
                StartingTime = new DateTime(2021, 8, 1, 18, 45, 00),
                HomeTeamId = 1,
                AwayTeamId = 3,
                CompetitionId = 1,
                StadiumId = 1,
                HomeGoals = 1,
                AwayGoals = 1,
            });
            await dbContext.Games.AddAsync(new Game
            {
                StartingTime = new DateTime(2021, 8, 5, 14, 30, 00),
                HomeTeamId = 7,
                AwayTeamId = 6,
                CompetitionId = 3,
                StadiumId = 4,
            });

            await dbContext.SaveChangesAsync();

            await dbContext.Odds.AddAsync(new Odds
            {
                GameId = 3,
                HomeWinOdds = 1.60M,
                DrawOdds = 3.30M,
                AwayWinOdds = 4.50M,
                BTTSNoOdds = 1.95M,
                BTTSYesOdds = 1.90M,
                OverOdds = 1.50M,
                UnderOdds = 2.50M,
            });
            await dbContext.Odds.AddAsync(new Odds
            {
                GameId = 2,
                HomeWinOdds = 1.90M,
                DrawOdds = 3.20M,
                AwayWinOdds = 3.00M,
                BTTSNoOdds = 2.50M,
                BTTSYesOdds = 1.60M,
                OverOdds = 1.35M,
                UnderOdds = 2.90M,
            });
            await dbContext.Odds.AddAsync(new Odds
            {
                GameId = 1,
                HomeWinOdds = 2.37M,
                DrawOdds = 2.90M,
                AwayWinOdds = 2.50M,
                BTTSNoOdds = 2.20M,
                BTTSYesOdds = 1.70M,
                OverOdds = 1.80M,
                UnderOdds = 1.90M,
            });
            await dbContext.Odds.AddAsync(new Odds
            {
                GameId = 4,
                HomeWinOdds = 13.00M,
                DrawOdds = 6.50M,
                AwayWinOdds = 1.15M,
                BTTSNoOdds = 1.20M,
                BTTSYesOdds = 4.00M,
                OverOdds = 1.45M,
                UnderOdds = 2.60M,
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
