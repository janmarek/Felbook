<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.GroupViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Group <%: Model.Group.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Group <%: Model.Group.Name%></h2>

	<% if (Model.Group.Parent != null)
	{ %>
	<p>Parent group: <%: Html.ActionLink(Model.Group.Parent.Name, "Detail", new { id = Model.Group.Parent.Id })%></p>
	<% } %>
	
	<% if (Model.Group.Creator != null)
	{ %>
	<p>Creator: <%: Html.ActionLink(Model.Group.Creator.FullName, "Index", "Profile", new { username = Model.Group.Creator.Username }, null)%></p>
	<% } %>

	<p><%: Model.Group.Description%></p>

	<% if (Request.IsAuthenticated)
	{ %>
	<p>
		<% if (!Model.Group.HasMember(Model.CurrentUser))
		{ %>
		<%: Html.ActionLink("Join group", "Join", new { id = Model.Group.Id })%>
		<% }
		else
		{ %>
		<%: Html.ActionLink("Leave group", "Leave", new { id = Model.Group.Id })%>
		<% } %> |
		<%: Html.ActionLink("Add subgroup", "CreateSubGroup", new { id = Model.Group.Id })%></p>
	<% } %>

	<div class="tabs">
		<ul>
			<li><a href="#wall">Wall</a></li>
			<li><a href="#info">Detailed information</a></li>
			<li><a href="#subgroups">Subgroups</a></li>
			<li><a href="#members">Members</a></li>
		</ul>

		<div id="wall">
			<% foreach (var status in Model.Group.Statuses)
			{ %>
			<p><%: status.Text %></p>
			<p>Author: <%: Html.ActionLink(status.User.FullName, "Index", new {username = status.User.Username}) %>, time: <%= String.Format("{0:g}", status.Created) %></p>
			<hr>
			<% } %>
		</div>

		<div id="info">
			<h3>Description</h3>
			<p><%: Model.Group.Description%></p>
			<% if (Model.Group.HasParent())
			{ %>
			<h3>Parent groups</h3>
			<ul>
				<% foreach (var group in Model.Group.Parents) { %>
				<li><%: Html.ActionLink(group.Name, "Detail", new {id = group.Id}) %></li>
				<% } %>
			</ul>
			<% } %>
			<h3>Admins</h3>
			<ul>
			<% foreach (var admin in Model.Group.Administrators)
			{ %>
			<li><%: Html.ActionLink(admin.FullName, "Index", new {username = admin.Username}) %></li>
			<% } %>
			</ul>
			<% if (Model.Group.Creator != null)
			{ %>
			<h3>Creator</h3>
			<ul>
			<li><%: Html.ActionLink(Model.Group.Creator.FullName, "Index", new { username = Model.Group.Creator.Username })%></li>
			</ul>
			<% } %>
		</div>

		<div id="subgroups">
			<ul>
			<% foreach (var group in Model.Group.Children)
			{ %>
			<li><%: Html.ActionLink(group.Name, "Detail", new {id = group.Id}) %></li>
			<% } %>
			</ul>
		</div>

		<div id="members">
			<ul>
			<% foreach (var member in Model.Group.Users)
			{ %>
			<li><%: Html.ActionLink(member.FullName, "Index", new {username = member.Username}) %></li>
			<% } %>
			</ul>		
		</div>
	</div>

	

</asp:Content>
