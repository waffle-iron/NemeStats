﻿#region LICENSE
// NemeStats is a free website for tracking the results of board games.
//     Copyright (C) 2015 Jacob Gordon
//
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
//
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
//
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>
#endregion

using System.Globalization;
using BusinessLogic.DataAccess;
using BusinessLogic.Logic;
using BusinessLogic.Logic.GameDefinitions;
using BusinessLogic.Logic.PlayedGames;
using BusinessLogic.Logic.Players;
using BusinessLogic.Models;
using BusinessLogic.Models.Games;
using BusinessLogic.Models.PlayedGames;
using BusinessLogic.Models.User;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BusinessLogic.Models.Players;
using BusinessLogic.Paging;
using UI.Attributes.Filters;
using UI.Controllers.Helpers;
using UI.Mappers.Interfaces;
using UI.Models;
using UI.Models.GameDefinitionModels;
using UI.Models.PlayedGame;
using UI.Models.Points;
using UI.Transformations;

namespace UI.Controllers
{
    public partial class PlayedGameController : BaseController
    {
        private readonly NemeStatsDataContext _dataContext;
        private readonly IPlayedGameRetriever _playedGameRetriever;
        private readonly IPlayerRetriever _playerRetriever;
        private readonly IPlayedGameDetailsViewModelBuilder _playedGameDetailsBuilder;
        private readonly IPlayedGameCreator _playedGameCreator;
        private readonly IGameDefinitionRetriever _gameDefinitionRetriever;
        private readonly IPlayedGameDeleter _playedGameDeleter;
        private readonly IGameDefinitionSaver _gameDefinitionSaver;
        private readonly IPlayerSaver _playerSaver;
        private readonly IMapperFactory _mapperFactory;

        internal const int NUMBER_OF_RECENT_GAMES_TO_DISPLAY = 25;

        public PlayedGameController(
            NemeStatsDataContext dataContext,
            IPlayedGameRetriever playedGameRetriever,
            IPlayerRetriever playerRetriever,
            IPlayedGameDetailsViewModelBuilder builder,
            IGameDefinitionRetriever gameDefinitionRetriever,
            IPlayedGameCreator playedGameCreator,
            IPlayedGameDeleter playedGameDeleter,
            IGameDefinitionSaver gameDefinitionSaver,
            IPlayerSaver playerSaver,
            IMapperFactory mapperFactory)
        {
            _dataContext = dataContext;
            _playedGameRetriever = playedGameRetriever;
            _playerRetriever = playerRetriever;
            _playedGameDetailsBuilder = builder;
            _gameDefinitionRetriever = gameDefinitionRetriever;
            _playedGameCreator = playedGameCreator;
            _playedGameDeleter = playedGameDeleter;
            _gameDefinitionSaver = gameDefinitionSaver;
            _playerSaver = playerSaver;
            _mapperFactory = mapperFactory;
        }

        // GET: /PlayedGame/Details/5
        [UserContext(RequiresGamingGroup = false)]
        public virtual ActionResult Details(int? id, ApplicationUser currentUser)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var playedGame = _playedGameRetriever.GetPlayedGameDetails(id.Value);
            if (playedGame == null)
            {
                return HttpNotFound();
            }
            var playedGameDetails = _playedGameDetailsBuilder.Build(playedGame, currentUser);
            return View(MVC.PlayedGame.Views.Details, playedGameDetails);
        }

        // GET: /PlayedGame/Create
        [System.Web.Mvc.Authorize]
        [UserContext]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult Create(ApplicationUser currentUser)
        {
            var viewModel = FillCreatePlayedGameViewModel(currentUser, new CreatePlayedGameViewModel());
            return View(MVC.PlayedGame.Views.CreateOrEdit, viewModel);
        }

        private CreatePlayedGameViewModel FillCreatePlayedGameViewModel(ApplicationUser currentUser, CreatePlayedGameViewModel viewModel)
        {
            var mostPlayedGames =
                _gameDefinitionRetriever.GetMostPlayedGames(new GetMostPlayedGamesQuery
                {
                    GamingGroupId = currentUser.CurrentGamingGroupId,
                    Page = 1,
                    PageSize = 5
                });
            var recentPlayedGames =
                _gameDefinitionRetriever.GetRecentGames(new GetRecentPlayedGamesQuery
                {
                    GamingGroupId = currentUser.CurrentGamingGroupId,
                    Page = 1,
                    PageSize = 5
                });

            var players = _playerRetriever.GetPlayersToCreate(currentUser.Id, currentUser.CurrentGamingGroupId);

            viewModel.UserPlayer = players.UserPlayer;
            viewModel.OtherPlayers = players.OtherPlayers;
            viewModel.RecentPlayers = players.RecentPlayers;
            viewModel.RecentPlayedGames = _mapperFactory.GetMapper<GameDefinitionDisplayInfo, GameDefinitionDisplayInfoViewModel>()
                .Map(recentPlayedGames)
                .ToList();
            viewModel.MostPlayedGames = _mapperFactory.GetMapper<GameDefinitionDisplayInfo, GameDefinitionDisplayInfoViewModel>()
                .Map(mostPlayedGames)
                .ToList();
            return viewModel;
        }

        // POST: /PlayedGame/Save
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserContext]
        public virtual ActionResult Save(SavePlayedGameRequest request, ApplicationUser currentUser)
        {
            if (ModelState.IsValid)
            {
                if (request.EditMode && request.PlayedGameId.HasValue)
                {
                    _playedGameDeleter.DeletePlayedGame(request.PlayedGameId.Value, currentUser);
                }

                if (request.GameDefinitionId == null)
                {
                    if (string.IsNullOrEmpty(request.GameDefinitionName))
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    request.GameDefinitionId = _gameDefinitionSaver.CreateGameDefinition(new CreateGameDefinitionRequest
                    {
                        Name = request.GameDefinitionName,
                        GamingGroupId = request.GameDefinitionId ?? currentUser.CurrentGamingGroupId,
                        BoardGameGeekGameDefinitionId = request.BoardGameGeekGameDefinitionId
                    }, currentUser).Id;
                }

                foreach (var newPlayer in request.PlayerRanks.Where(p => !p.PlayerId.HasValue))
                {
                    newPlayer.PlayerId = _playerSaver.CreatePlayer(new CreatePlayerRequest
                    {
                        GamingGroupId = currentUser.CurrentGamingGroupId,
                        Name = newPlayer.PlayerName
                    }, currentUser).Id;
                }


                var playedGame = _playedGameCreator.CreatePlayedGame(_mapperFactory.GetMapper<SavePlayedGameRequest, NewlyCompletedGame>().Map(request), TransactionSource.WebApplication, currentUser);

                return Json(new { success = true, playedGameId = playedGame.Id });
            }

            return Json(new { success = false, errors = ModelState.Errors() }, JsonRequestBehavior.AllowGet);
        }

        // GET: /PlayedGame/Edit
        [Authorize]
        [UserContext]
        [HttpGet]
        public virtual ActionResult Edit(int id, ApplicationUser currentUser)
        {
            var viewModel = new EditPlayedGameViewModel();
            viewModel = (EditPlayedGameViewModel)FillCreatePlayedGameViewModel(currentUser, viewModel);
            viewModel.EditMode = true;
            viewModel.PlayedGameId = id;

            FillCreatePlayedGameViewModel(currentUser, viewModel);

            var playedGame = _playedGameRetriever.GetPlayedGameDetails(id);
            viewModel.DatePlayed = playedGame.DatePlayed;
            viewModel.Notes = playedGame.Notes;
            viewModel.GameDefinitionId = playedGame.GameDefinitionId;

            var gameDefinition = _gameDefinitionRetriever.GetGameDefinitionDisplayInfo(playedGame.GameDefinitionId);
            viewModel.BoardGameGeekGameDefinitionId = gameDefinition.BoardGameGeekGameDefinitionId;
            viewModel.GameDefinitionName = gameDefinition.Name;

            viewModel.PlayerRanks = playedGame.PlayerGameResults.Select(item => new CreatePlayerRankRequest { GameRank = item.GameRank, PlayerId = item.PlayerId, PlayerName = item.Player.Name, PointsScored = item.PointsScored }).ToList();

            return View(MVC.PlayedGame.Views.CreateOrEdit, viewModel);
        }


        [System.Web.Mvc.HttpGet]
        public virtual ActionResult ShowRecentlyPlayedGames()
        {
            var recentlyPlayedGames = _playedGameRetriever.GetRecentPublicGames(NUMBER_OF_RECENT_GAMES_TO_DISPLAY);

            return View(MVC.PlayedGame.Views.RecentlyPlayedGames, recentlyPlayedGames);
        }

        // GET: /PlayedGame/Delete/5
        [System.Web.Mvc.Authorize]
        [UserContext]
        public virtual ActionResult Delete(int? id, ApplicationUser currentUser)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var playedgame = _dataContext.GetQueryable<PlayedGame>().FirstOrDefault(playedGame => playedGame.Id == id.Value);
            if (playedgame == null)
            {
                return HttpNotFound();
            }
            return View(playedgame);
        }

        // POST: /PlayedGame/Delete/5
        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpPost, System.Web.Mvc.ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [UserContext]
        public virtual ActionResult DeleteConfirmed(int id, ApplicationUser currentUser)
        {
            _playedGameDeleter.DeletePlayedGame(id, currentUser);
            //TODO really don't know whether I need to commit here or if it is automatically taken care of when disposing.
            _dataContext.CommitAllChanges();
            return new RedirectResult(Url.Action(MVC.GamingGroup.ActionNames.Index, MVC.GamingGroup.Name)
                            + "#" + GamingGroupController.SECTION_ANCHOR_RECENT_GAMES);
        }



        private IEnumerable<SelectListItem> RemovePlayersFromExistingPlayerRanks(IEnumerable<SelectListItem> players, List<PlayerRank> playerRanks)
        {
            return players.Where(item => playerRanks.Any(p => p.PlayerId.ToString() == item.Value) == false).ToList();
        }

        [System.Web.Mvc.Authorize]
        [UserContext]
        [System.Web.Mvc.HttpGet]
        public virtual ActionResult Search(ApplicationUser currentUser)
        {
            var viewModel = new SearchViewModel
            {
                GameDefinitions = GetAllGameDefinitionsForCurrentGamingGroup(currentUser.CurrentGamingGroupId)
            };
            return View(MVC.PlayedGame.Views.Search, viewModel);
        }

        [System.Web.Mvc.Authorize]
        [UserContext]
        [System.Web.Mvc.HttpPost]
        public virtual ActionResult Search(PlayedGamesFilterViewModel filter, ApplicationUser currentUser)
        {
            var playedGameFilter = new PlayedGameFilter
            {
                EndDateGameLastUpdated = filter.DatePlayedEnd == null ? null : filter.DatePlayedEnd.Value.ToString("yyyy-MM-dd"),
                GamingGroupId = currentUser.CurrentGamingGroupId,
                StartDateGameLastUpdated = filter.DatePlayedStart == null ? null : filter.DatePlayedStart.Value.ToString("yyyy-MM-dd"),
                GameDefinitionId = filter.GameDefinitionId
            };
            var searchResults = _playedGameRetriever.SearchPlayedGames(playedGameFilter);

            var playedGamesDetails = searchResults.Select(searchResult => new PlayedGameDetailsViewModel
            {
                DatePlayed = searchResult.DatePlayed,
                GameDefinitionId = searchResult.GameDefinitionId,
                GameDefinitionName = searchResult.GameDefinitionName,
                GamingGroupId = searchResult.GamingGroupId,
                GamingGroupName = searchResult.GamingGroupName,
                Notes = searchResult.Notes,
                PlayedGameId = searchResult.PlayedGameId,
                UserCanEdit = true,
                WinnerType = searchResult.WinnerType,
                PlayerResults = searchResult.PlayerGameResults.Select(playerResult => new GameResultViewModel
                {
                    DatePlayed = searchResult.DatePlayed,
                    GameDefinitionId = searchResult.GameDefinitionId,
                    GameDefinitionName = searchResult.GameDefinitionName,
                    GameRank = playerResult.GameRank,
                    NemePointsSummary = new NemePointsSummaryViewModel(playerResult.NemeStatsPointsAwarded, playerResult.GameDurationBonusNemePoints, playerResult.GameWeightBonusNemePoints),
                    PlayedGameId = searchResult.PlayedGameId,
                    PlayerId = playerResult.PlayerId,
                    PlayerName = playerResult.PlayerName,
                    WinnerType = searchResult.WinnerType
                }).ToList()
            }).ToList();

            var viewModel = new SearchViewModel
            {
                Filter =
                {
                    DatePlayedEnd = filter.DatePlayedEnd,
                    DatePlayedStart = filter.DatePlayedStart,
                    GameDefinitionId = filter.GameDefinitionId
                },
                GameDefinitions = GetAllGameDefinitionsForCurrentGamingGroup(currentUser.CurrentGamingGroupId),
                PlayedGames = new PlayedGamesViewModel
                {
                    PlayedGameDetailsViewModels = playedGamesDetails,
                    UserCanEdit = true,
                    GamingGroupId = currentUser.CurrentGamingGroupId,
                    ShowSearchLinkInResultsHeader = false
                }
            };
            return View(MVC.PlayedGame.Views.Search, viewModel);
        }

        private IList<SelectListItem> GetAllGameDefinitionsForCurrentGamingGroup(int gamingGroupId)
        {
            var gameDefinitions = _gameDefinitionRetriever.GetAllGameDefinitionNames(gamingGroupId);
            var selectListItems = new List<SelectListItem>
            {
                new SelectListItem
                {
                   Selected = true,
                    Text = "All",
                    Value = string.Empty
                }

            };

            selectListItems.AddRange(gameDefinitions
                .OrderBy(gameDefinition => gameDefinition.Name)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(CultureInfo.InvariantCulture)
                }).ToList());

            return selectListItems;
        }
    }
}