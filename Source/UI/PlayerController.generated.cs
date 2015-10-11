// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments and CLS compliance
// 0108: suppress "Foo hides inherited member Foo. Use the new keyword if hiding was intended." when a controller and its abstract parent are both processed
#pragma warning disable 1591, 3008, 3009, 0108
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace UI.Controllers
{
    public partial class PlayerController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected PlayerController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Details()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Details);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult InvitePlayer()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InvitePlayer);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Save()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Save);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Edit()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public PlayerController Actions { get { return MVC.Player; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Player";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Player";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Details = "Details";
            public readonly string SavePlayer = "SavePlayer";
            public readonly string Create = "Create";
            public readonly string InvitePlayer = "InvitePlayer";
            public readonly string ShowTopPlayers = "ShowTopPlayers";
            public readonly string ShowRecentNemesisChanges = "ShowRecentNemesisChanges";
            public readonly string Save = "Save";
            public readonly string Edit = "Edit";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Details = "Details";
            public const string SavePlayer = "SavePlayer";
            public const string Create = "Create";
            public const string InvitePlayer = "InvitePlayer";
            public const string ShowTopPlayers = "ShowTopPlayers";
            public const string ShowRecentNemesisChanges = "ShowRecentNemesisChanges";
            public const string Save = "Save";
            public const string Edit = "Edit";
        }


        static readonly ActionParamsClass_Details s_params_Details = new ActionParamsClass_Details();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Details DetailsParams { get { return s_params_Details; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Details
        {
            public readonly string id = "id";
            public readonly string currentUser = "currentUser";
        }
        static readonly ActionParamsClass_InvitePlayer s_params_InvitePlayer = new ActionParamsClass_InvitePlayer();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_InvitePlayer InvitePlayerParams { get { return s_params_InvitePlayer; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_InvitePlayer
        {
            public readonly string id = "id";
            public readonly string currentUser = "currentUser";
            public readonly string playerInvitationViewModel = "playerInvitationViewModel";
        }
        static readonly ActionParamsClass_Save s_params_Save = new ActionParamsClass_Save();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Save SaveParams { get { return s_params_Save; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Save
        {
            public readonly string model = "model";
            public readonly string currentUser = "currentUser";
        }
        static readonly ActionParamsClass_Edit s_params_Edit = new ActionParamsClass_Edit();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Edit EditParams { get { return s_params_Edit; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Edit
        {
            public readonly string id = "id";
            public readonly string player = "player";
            public readonly string currentUser = "currentUser";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string _CreateOrUpdatePartial = "_CreateOrUpdatePartial";
                public readonly string _PlayersPartial = "_PlayersPartial";
                public readonly string _PlayersSummaryPartial = "_PlayersSummaryPartial";
                public readonly string _PlayerVersusPlayersPartial = "_PlayerVersusPlayersPartial";
                public readonly string _RecentNemesisChangesPartial = "_RecentNemesisChangesPartial";
                public readonly string _TopPlayersPartial = "_TopPlayersPartial";
                public readonly string Create = "Create";
                public readonly string Details = "Details";
                public readonly string Edit = "Edit";
                public readonly string InvitePlayer = "InvitePlayer";
                public readonly string RecentNemesisChanges = "RecentNemesisChanges";
                public readonly string TopPlayers = "TopPlayers";
            }
            public readonly string _CreateOrUpdatePartial = "~/Views/Player/_CreateOrUpdatePartial.cshtml";
            public readonly string _PlayersPartial = "~/Views/Player/_PlayersPartial.cshtml";
            public readonly string _PlayersSummaryPartial = "~/Views/Player/_PlayersSummaryPartial.cshtml";
            public readonly string _PlayerVersusPlayersPartial = "~/Views/Player/_PlayerVersusPlayersPartial.cshtml";
            public readonly string _RecentNemesisChangesPartial = "~/Views/Player/_RecentNemesisChangesPartial.cshtml";
            public readonly string _TopPlayersPartial = "~/Views/Player/_TopPlayersPartial.cshtml";
            public readonly string Create = "~/Views/Player/Create.cshtml";
            public readonly string Details = "~/Views/Player/Details.cshtml";
            public readonly string Edit = "~/Views/Player/Edit.cshtml";
            public readonly string InvitePlayer = "~/Views/Player/InvitePlayer.cshtml";
            public readonly string RecentNemesisChanges = "~/Views/Player/RecentNemesisChanges.cshtml";
            public readonly string TopPlayers = "~/Views/Player/TopPlayers.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_PlayerController : UI.Controllers.PlayerController
    {
        public T4MVC_PlayerController() : base(Dummy.Instance) { }

        [NonAction]
        partial void DetailsOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? id, BusinessLogic.Models.User.ApplicationUser currentUser);

        [NonAction]
        public override System.Web.Mvc.ActionResult Details(int? id, BusinessLogic.Models.User.ApplicationUser currentUser)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Details);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "currentUser", currentUser);
            DetailsOverride(callInfo, id, currentUser);
            return callInfo;
        }

        [NonAction]
        partial void SavePlayerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult SavePlayer()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.SavePlayer);
            SavePlayerOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult Create()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            CreateOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InvitePlayerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id, BusinessLogic.Models.User.ApplicationUser currentUser);

        [NonAction]
        public override System.Web.Mvc.ActionResult InvitePlayer(int id, BusinessLogic.Models.User.ApplicationUser currentUser)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InvitePlayer);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "currentUser", currentUser);
            InvitePlayerOverride(callInfo, id, currentUser);
            return callInfo;
        }

        [NonAction]
        partial void ShowTopPlayersOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ShowTopPlayers()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ShowTopPlayers);
            ShowTopPlayersOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void ShowRecentNemesisChangesOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult ShowRecentNemesisChanges()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.ShowRecentNemesisChanges);
            ShowRecentNemesisChangesOverride(callInfo);
            return callInfo;
        }

        [NonAction]
        partial void InvitePlayerOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, UI.Models.Players.PlayerInvitationViewModel playerInvitationViewModel, BusinessLogic.Models.User.ApplicationUser currentUser);

        [NonAction]
        public override System.Web.Mvc.ActionResult InvitePlayer(UI.Models.Players.PlayerInvitationViewModel playerInvitationViewModel, BusinessLogic.Models.User.ApplicationUser currentUser)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.InvitePlayer);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "playerInvitationViewModel", playerInvitationViewModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "currentUser", currentUser);
            InvitePlayerOverride(callInfo, playerInvitationViewModel, currentUser);
            return callInfo;
        }

        [NonAction]
        partial void SaveOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, BusinessLogic.Models.Player model, BusinessLogic.Models.User.ApplicationUser currentUser);

        [NonAction]
        public override System.Web.Mvc.ActionResult Save(BusinessLogic.Models.Player model, BusinessLogic.Models.User.ApplicationUser currentUser)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Save);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "currentUser", currentUser);
            SaveOverride(callInfo, model, currentUser);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int? id);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(int? id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            EditOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void EditOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, BusinessLogic.Models.Player player, BusinessLogic.Models.User.ApplicationUser currentUser);

        [NonAction]
        public override System.Web.Mvc.ActionResult Edit(BusinessLogic.Models.Player player, BusinessLogic.Models.User.ApplicationUser currentUser)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Edit);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "player", player);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "currentUser", currentUser);
            EditOverride(callInfo, player, currentUser);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591, 3008, 3009, 0108
