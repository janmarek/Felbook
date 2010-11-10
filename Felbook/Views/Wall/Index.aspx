<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.WallViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Wall
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Wall</h2>

	<% foreach (var wallItem in Model.WallItems)
	{ %>
	<% Html.RenderPartial("Status", wallItem.Status); %>
	<% } %>

</asp:Content>
