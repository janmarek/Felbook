<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.ProfileViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<p>
		<%= Html.ActionLink("Edit profile", "Edit") %></p>
	<h2>
		Profile
		<%= Model.User.Name %>
		<%= Model.User.Surname %>
	</h2>
	<ul>
		<li>E-mail:
			<%= Model.User.Mail %></li>
	</ul>
	<h3>
		Add status</h3>
	<% using (Html.BeginForm("AddStatus", "Profile", FormMethod.Post, new { enctype = "multipart/form-data" }))
	{
		Html.RenderPartial("AddStatusFormContent");
	} %>
	<h3>
		My statuses</h3>
	<% foreach (var status in Model.User.Statuses.OrderByDescending(status => status.Id))
	{ %>
	<% Html.RenderPartial("Status", Model.CreateStatusViewModel(status)); %>
	<% } %>
</asp:Content>
