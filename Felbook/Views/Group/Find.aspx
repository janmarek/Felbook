<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.FindGroupViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Find group
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Create new group</h2>
	<p><%: Html.ActionLink("Create group", "Create") %></p>

	<h2>Find group</h2>
	<% using (Html.BeginForm())
	{ %>
	<fieldset>
		<legend>Find</legend>
		<p>
			Part of name or description:
			<%: Html.TextBox("search", Model.SearchText) %>
			<input type="submit" value="Find"></p>
	</fieldset>
	<% } %>

	<% if (Model.SearchText != null)
	{ %>
		<h2>Search results</h2>
		<ul>
		<% foreach (var group in Model.Groups)
		{ %>
			<li><%: Html.ActionLink(group.Name, "Detail", new {id = group.Id}) %></li>
		<% } %>
		</ul>
	<% } %>
</asp:Content>
