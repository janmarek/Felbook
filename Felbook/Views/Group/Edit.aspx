<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.Group>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit group <%: Model.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit group <%: Model.Name %></h2>

	<% Html.RenderPartial("GroupForm"); %>

	<p><%= Html.ActionLink("Back", "Detail", new { id = Model.Id })%></p>

</asp:Content>

