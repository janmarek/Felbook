<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% if (!Request.IsAuthenticated) { %>
		<h1 class="msgNavigation">Welcome in Felbook</h1>
    <p>
    You can simply communicate with your classmates and teachers, share files and debate about school courses. 
    Friendship is established through follow someone. There is possibility of creating groups and subgroups you can read about
    many Felbook features in the <%= Html.ActionLink("Help", "../Help/")%>.
    <hr />
    If you have not yet registered you can register <%= Html.ActionLink("here", "../Account/Register/")%>.
    </p>
	<% } else {
    //jinak přesměruje uživatele do jeho profilu
        Response.Redirect("/Profile?username=" + Page.User.Identity.Name); 
    } %>
</asp:Content>
