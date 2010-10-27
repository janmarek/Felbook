<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Sent
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Mail-box (<%--<%= Model.Name %> <%= Model.Surname %>--%>)</h2>
     
    <h3>
    <%: Html.ActionLink("Recieved messages", "Index", "Message", new { username = Page.User.Identity.Name }, null)%>,
    Sent messages,
    <%: Html.ActionLink("Send message", "SendMessage", "Message", new { username = Page.User.Identity.Name }, null)%>
    </h3>

    <% foreach (var message in Model) { %>
    <div class="message"><p><b><%= String.Format("{0:g}", message.Created)%></b> from <%= message.Sender.Username%></p> 
    <p><%= message.Text%></p><//div>
    <% } %> 

</asp:Content>
