<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SendMessage
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Messages (<%= Page.User.Identity.Name%>)</h2>

    <h3>
    <%: Html.ActionLink("Recieved messages", "Index", "Message")%>,
    <%: Html.ActionLink("Sent messages", "Sent", "Message")%>,
    Send message
    </h3>
        
	<% using (Html.BeginForm("SendMessage", "Message")) { %>
		<%: Html.AntiForgeryToken() %>
        <h3>Reciervers (separator is space):</h3>
        <%: Html.Hidden("PrevMessageID", (object)0)%>
		<%: Html.TextBox("To") %> <br />
        <%: Html.ValidationSummary() %>
        <h3>Text</h3>
        <%: Html.TextArea("Text", "", 15, 50, "") %> <br />
		<input type="submit" value="Send" />
	<% } %>

</asp:Content>
