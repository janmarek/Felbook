<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SendMessage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Mail-box (<%= Model.Name %> <%= Model.Surname %>)</h2>

    <h3>
    <%: Html.ActionLink("Recieved messages", "Index", "Message", new { username = Page.User.Identity.Name }, null)%>,
    <%: Html.ActionLink("Sent messages", "Sent", "message", new { username = Page.User.Identity.Name }, null)%>,
    Send message
    </h3>

    
	<% using (Html.BeginForm("SendMessage", "Message")) { %>
		<%: Html.ValidationSummary() %>
		<%: Html.AntiForgeryToken() %>
        <h3>Reciervers:</h3>
		<%: Html.TextBox("To") %> <br />
        <h3>Text</h3> <br />
        <%: Html.TextBox("Text") %> <br />
		<input type="submit" value="Send" />
	<% } %>

</asp:Content>
