<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Profile <%= Model.Name %> <%= Model.Surname %></h2>

	<ul>
		<li>E-mail: <%= Model.Mail %></li>
	</ul>

	<h3>Add status</h3>
	<% using (Html.BeginForm("AddStatus", "Profile")) { %>
		<%: Html.ValidationSummary() %>
		<%: Html.AntiForgeryToken() %>
		<%: Html.TextBox("status") %>
		<input type="submit" value="Send">
	<% } %>

	<h3>My statuses</h3>
	<% foreach (var status in Model.Statuses) { %>
	<p><b><%= String.Format("{0:g}", status.Created) %></b> - <%= status.Text %></p>
	<% } %>

</asp:Content>
