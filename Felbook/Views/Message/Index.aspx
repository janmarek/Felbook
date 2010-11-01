<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Messages (<%= Page.User.Identity.Name%>)</h2>

    <h3>
        Recieved messages,
        <%: Html.ActionLink("Sent messages", "Sent", "Message")%>,
        <%: Html.ActionLink("Send message", "SendMessage", "Message")%>
    </h3>
    <% foreach (var message in Model.Messages)
       { %>
    <div class="message">
        <p>
            <b><%= String.Format("{0:g}", message.Created)%></b> from
            <%= message.Sender.Username%>
            <%: Html.ActionLink("Reply >>", "ReplyMessage", "Message", new { msgid = message.Id.ToString() }, null)%>
        </p>
        <p>
            <%= message.Text%>
        </p>
    </div>
    <% } %>
</asp:Content>
