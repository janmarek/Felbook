<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Felbook.Models.FollowLinkViewModel>" %>

<% if (Model.CurrentUser != null && Model.CurrentUser != Model.Follower)
   { %>
   <% if (Model.Follower.IsFollowedBy(Model.CurrentUser))
	  { %>
   <%= Html.ActionLink("Unfollow", "UnfollowUser", "User", new { id = Model.Follower.Id }, new { @class = "ajax-default" })%>
   <% }
	  else
	  { %>
<%= Html.ActionLink("Follow", "FollowUser", "User", new { id = Model.Follower.Id }, new { @class = "ajax-default" })%>
<% }
   }%>