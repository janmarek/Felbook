<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Felbook.Models.UserListViewModel>" %>
<ul>
<% foreach (var user in Model.Users)
{ %>
	<li>
		<%= Html.ActionLink(user.FullName, "Index", "Profile", new {username = user.Username}, null) %>
		<% Html.RenderPartial("FollowLink", new Felbook.Models.FollowLinkViewModel(Model.CurrentUser, user)); %>

		(<%= Html.ActionLink("Followers", "Followers", "User", new {username = user.Username}, null) %>, 
		<%= Html.ActionLink("Followings", "Followings", "User", new { username = user.Username }, null)%>)
	</li>
<% } %>
</ul>