<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.Group>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Group <%: Model.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Group <%: Model.Name %></h2>

	<% if (Model.Parent != null)
	{ %>
	<p>Parent group: <%: Html.ActionLink(Model.Parent.Name, "Detail", new {id = Model.Parent.Id}) %></p>
	<% } %>
	
	<% if (Model.Creator != null)
	{ %>
	<p>Creator: <%: Html.ActionLink(Model.Creator.FullName, "Index", "Profile", new { username = Model.Creator.Username }, null)%></p>
	<% } %>

	<p><%: Model.Description %></p>

	<p>Join/leave group | Add subgroup</p>

	<div class="tabs">
		<ul>
			<li><a href="#wall">Wall</a></li>
			<li><a href="#info">Detailed information</a></li>
			<li><a href="#subgroups">Subgroups</a></li>
			<li><a href="#members">Members</a></li>
		</ul>

		<div id="wall">
			<% foreach (var status in Model.Statuses) { %>
			<p><%: status.Text %></p>
			<p>Author: <%: Html.ActionLink(status.User.FullName, "Index", new {username = status.User.Username}) %>, time: <%= String.Format("{0:g}", status.Created) %></p>
			<hr>
			<% } %>
		</div>

		<div id="info">
			<p><%: Model.Description %></p>
		</div>

		<div id="subgroups">
			<ul>
			<% foreach (var group in Model.Children) { %>
			<li><%: Html.ActionLink(group.Name, "Detail", new {id = group.Id}) %></li>
			<% } %>
			</ul>
		</div>

		<div id="members">
			<ul>
			<% foreach (var member in Model.Users) { %>
			<li><%: Html.ActionLink(member.FullName, "Index", new {username = member.Username}) %></li>
			<% } %>
			</ul>		
		</div>
	</div>

	

</asp:Content>
