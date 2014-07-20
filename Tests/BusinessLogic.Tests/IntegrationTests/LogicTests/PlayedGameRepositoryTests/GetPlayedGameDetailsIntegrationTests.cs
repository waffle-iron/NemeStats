﻿using BusinessLogic.DataAccess;
using BusinessLogic.DataAccess.Repositories;
using BusinessLogic.Logic;
using BusinessLogic.Models;
using NUnit.Framework;
using System.Linq;

namespace BusinessLogic.Tests.IntegrationTests.LogicTests.PlayedGameRepositoryTests
{
    [TestFixture]
    public class GetPlayedGameDetailsIntegrationTests : IntegrationTestBase
    {
        private PlayedGame GetTestSubjectPlayedGame(NemeStatsDbContext dbContextToTestWith)
        {
            return new EntityFrameworkPlayedGameRepository(dbContextToTestWith)
                .GetPlayedGameDetails(testPlayedGames[0].Id, testUserWithDefaultGamingGroup);
        }

        [Test]
        public void ItRetrievesThePlayedGame()
        {
            using(NemeStatsDbContext dbContext = new NemeStatsDbContext())
            {
                PlayedGame playedGame = GetTestSubjectPlayedGame(dbContext);
                Assert.NotNull(playedGame);
            }
        }

        [Test]
        public void ItEagerlyFetchesTheGameResults()
        {
            using(NemeStatsDbContext dbContext = new NemeStatsDbContext())
            {
                dbContext.Configuration.LazyLoadingEnabled = false;
                PlayedGame playedGame = GetTestSubjectPlayedGame(dbContext);
                Assert.GreaterOrEqual(testPlayedGames[0].PlayerGameResults.Count, playedGame.PlayerGameResults.Count());
            }
        }

        [Test]
        public void ItEagerlyFetchesTheGameDefinition()
        {
            using (NemeStatsDbContext dbContext = new NemeStatsDbContext())
            {
                dbContext.Configuration.LazyLoadingEnabled = false;
                PlayedGame playedGame = GetTestSubjectPlayedGame(dbContext);
                Assert.NotNull(playedGame.GameDefinition);
            }
        }

        [Test]
        public void ItReturnsNullIfNoPlayedGameFound()
        {
            using (NemeStatsDbContext dbContext = new NemeStatsDbContext())
            {
                EntityFrameworkPlayedGameRepository playedGameRepository = new EntityFrameworkPlayedGameRepository(dbContext);

                PlayedGame notFoundPlayedGame = playedGameRepository.GetPlayedGameDetails(-1, testUserWithDefaultGamingGroup);
                Assert.Null(notFoundPlayedGame);
            }
        }

        [Test]
        public void ItThrowsANotAuthorizedExceptionIfTheGameIsntInThePlayersGamingGroup()
        {
            using (NemeStatsDbContext dbContext = new NemeStatsDbContext())
            {
                PlayedGame gameWithMismatchedGamingGroupId = testPlayedGames.First(
                    playedGame => playedGame.GamingGroupId != testUserWithDefaultGamingGroup.CurrentGamingGroupId);

                EntityFrameworkPlayedGameRepository playedGameRepository = new EntityFrameworkPlayedGameRepository(dbContext);

                PlayedGame notFoundPlayedGame = playedGameRepository.GetPlayedGameDetails(gameWithMismatchedGamingGroupId.Id, testUserWithDefaultGamingGroup);
                Assert.Null(notFoundPlayedGame);
            }
        }
    }
}
