﻿#region LICENSE

// NemeStats is a free website for tracking the results of board games. Copyright (C) 2015 Jacob Gordon
// 
// This program is free software: you can redistribute it and/or modify it under the terms of the
// GNU General Public License as published by the Free Software Foundation, either version 3 of the
// License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without
// even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along with this program. If
// not, see <http://www.gnu.org/licenses/>

#endregion LICENSE

using BusinessLogic.Logic.GamingGroups;
using BusinessLogic.Models;
using BusinessLogic.Models.GamingGroups;
using BusinessLogic.Models.Utility;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UI.Controllers;
using UI.Models.GamingGroup;
using UI.Transformations;

namespace UI.Tests.UnitTests.ControllerTests.GamingGroupControllerTests
{
    [TestFixture]
    public class IndexTests : GamingGroupControllerTestBase
    {
        private GamingGroupSummary gamingGroupSummary;
        private GamingGroupViewModel gamingGroupViewModel;
        private BasicDateRangeFilter dateRangeFilter;

        [Test]
        public override void SetUp()
        {
            base.SetUp();

            gamingGroupSummary = new GamingGroupSummary()
            {
                PlayedGames = new List<PlayedGame>()
            };
            gamingGroupViewModel = new GamingGroupViewModel();
            dateRangeFilter = new BasicDateRangeFilter();

            autoMocker.ClassUnderTest.Expect(mock => mock.GetGamingGroupSummary(
                Arg<int>.Is.Anything,
                Arg<IDateRangeFilter>.Is.Anything))
                .Repeat.Once()
                .Return(gamingGroupSummary);

            autoMocker.Get<IGamingGroupViewModelBuilder>().Expect(mock => mock.Build(gamingGroupSummary, currentUser))
                .Return(gamingGroupViewModel);
        }

        [Test]
        public void ItReturnsTheIndexView()
        {
            ViewResult viewResult = autoMocker.ClassUnderTest.Index(dateRangeFilter, currentUser) as ViewResult;

            Assert.AreEqual(MVC.GamingGroup.Views.Index, viewResult.ViewName);
        }

        [Test]
        public void ItAddsAGamingGroupViewModelToTheView()
        {
            ViewResult viewResult = autoMocker.ClassUnderTest.Index(dateRangeFilter, currentUser) as ViewResult;

            Assert.AreSame(gamingGroupViewModel, viewResult.Model);
        }

        [Test]
        public void ItPreservesTheDateRangeFilter()
        {
            ViewResult viewResult = autoMocker.ClassUnderTest.Index(dateRangeFilter, currentUser) as ViewResult;

            GamingGroupViewModel model = viewResult.Model as GamingGroupViewModel;
            Assert.AreSame(dateRangeFilter, model.DateRangeFilter);
        }

        [Test]
        public void ItShowsTheSearchPlayedGamesLinkInThePlayedGamePanelHeader()
        {
            var viewResult = autoMocker.ClassUnderTest.Index(dateRangeFilter, currentUser) as ViewResult;

            var viewModel = (GamingGroupViewModel)viewResult.Model;
            Assert.That(viewModel.PlayedGames.ShowSearchLinkInResultsHeader, Is.True);
        }

        [Test]
        public void ItAddsAModelErrorIfTheFromDateIsLaterThanTheToDate()
        {
            var basicDateRangeFilter = new BasicDateRangeFilter
            {
                FromDate = DateTime.UtcNow,
                ToDate = DateTime.UtcNow.AddDays(-1)
            };
            var viewResult = autoMocker.ClassUnderTest.Index(basicDateRangeFilter, currentUser) as ViewResult;

            Assert.True(viewResult.ViewData.ModelState.ContainsKey("dateRangeFilter"));
            var modelErrorsForKey = viewResult.ViewData.ModelState["dateRangeFilter"].Errors;
            Assert.That(modelErrorsForKey.Count, Is.EqualTo(1));
            Assert.That(modelErrorsForKey[0].ErrorMessage, Is.EqualTo("The 'From Date' cannot be greater than the 'To Date'."));
        }
    }
}