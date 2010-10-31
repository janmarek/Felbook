<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sent
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Messages (<%= Page.User.Identity.Name%>)</h2>
     
    <h3>
    <%: Html.ActionLink("Recieved messages", "Index", "Message")%>,
    Sent messages,
    <%: Html.ActionLink("Send message", "SendMessage", "Message")%>
    </h3>

    <% foreach (var message in Model) { %>
    <div class="message">
        <p>
            <b><%= String.Format("{0:g}", message.Created)%></b> 
            to
            <% foreach (var user in message.Users) { %>
                <%= user.Username%>, 
            <% } %>
        </p>
        <p><%= message.Text%></p>
    </div>
    <% } %>
</asp:Content>
