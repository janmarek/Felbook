<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<% if (Request.IsAuthenticated) { %>
		<%: Html.ActionLink("My profile", "Index", "Profile", new {username = User.Identity.Name}, null)	%>
	<% } else { %>
		<p>You are not logged in!</p>
	<% } %>
</asp:Content>
