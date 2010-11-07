<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Messages (<%= Page.User.Identity.Name%>)</h2>

    <h3>
        Recieved messages,
        <%--<%: Html.ActionLink("Sent messages", "Sent", "Message")%>,--%>
        <%: Html.ActionLink("Send message", "SendMessage", "Message") %>
    </h3>

    <% foreach (var message in Model)
       {
           string style = "style=\"position: relative; left: " + message.Indent + "px; ";

           if (message.Recieved)
           {
               style += "border-color: green\"";
           }
           else
           {
               style += "border-color: blue\"";
           }
           %>

           <div class="message" <%=style %>>
            <p>
                <b><%= String.Format("{0:g}", message.Sent)%></b> 
                <% if (message.Recieved)
                   { %>
                from
                <%
                    }
                   else
                   { %>
                   to
                <% }%> 
                <%= message.SenderOrRecivers%>
                <% if (message.Recieved)
                   { %>
                <%: Html.ActionLink("Reply >>", "ReplyMessage", "Message", new { msgid = message.ID.ToString() }, null)%>
                <% } %>
            </p>
            <p>
                <%= message.TextPreview%>
            </p>
           </div>

    <% } %>
  
</asp:Content>
