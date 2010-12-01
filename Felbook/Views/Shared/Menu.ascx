<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<ul id="menu">
	<% if (Request.IsAuthenticated)
	{ %>
		<li><%= Html.ActionLink("Wall", "Index", "Wall", null, new { id = "wall-link" }) %></li>

		<li><%= Html.ActionLink("Messages", "Index", "Message", new { page = 1 }, new { id = "messages-link" })%></li>
    <li><%= Html.ActionLink("My profile", "Index", "Profile", new { username = Page.User.Identity.Name }, null)%></li>
    <li><%= Html.ActionLink("Events", "Index", "Event")%></li>

	<% }
	else
	{ %>
		<li><%= Html.ActionLink("TestData", "TestData", "Home")%></li>
	<% } %>
	<li><%= Html.ActionLink("Groups", "Find", "Group")%></li>
</ul>