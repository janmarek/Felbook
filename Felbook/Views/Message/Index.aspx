<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Messages (<%= Page.User.Identity.Name%>)</h2>

    <h3>
        Recieved messages,
        <%: Html.ActionLink("Sent messages", "Sent", "Message")%>,
        <%: Html.ActionLink("Send message", "SendMessage", "Message") %>
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
 
    <%--<% int indent = 0;
       string style;
       Felbook.Models.Message prevMessage = null;
       Stack<Felbook.Models.Message> messageStack = new Stack<Felbook.Models.Message>();
       
    foreach (Felbook.Models.Message message in Model.Messages)
    {
        
        if (message.ReplyTo == prevMessage)
        {
            messageStack.Push(prevMessage);
            indent++;
        }

        if (message.ReplyTo != messageStack.Peek())
        {
            messageStack.Pop();
            indent--;
        }
           
        style = "style=\"left: " + (indent * 10) + "px\"";

        if (message.Sender.Username == Page.User.Identity.Name)
        {
            style = "border: green";
        }
        else 
        {
            style = "border: blue";
        }%>
        
        <div class="message" <%=style %>>
            <p>
                <b><%= String.Format("{0:g}", message.Created)%></b> 
                from
                <%= message.Sender.Username%>
                <%
                if (message.Sender.Username != Page.User.Identity.Name)
                { %>
                <%: Html.ActionLink("Reply >>", "ReplyMessage", "Message", new { msgid = message.Id.ToString() }, null)%>
                <% } %>
            </p>
            <p>
                <%= message.Text%>
            </p>
        </div>
        <%
        prevMessage = message;

    } %>--%>

    <%--<%
    class MessageViewer {
        public void viewListOfMessages(List<Felbook.Models.Message> list, string username, int indent) 
            {
                string style;
            
                foreach (var message in list) 
                { 
                    if (message.Sender.Username == username)
                    {
                        style = "border: green";
                    }
                    else 
                    {
                        style = "border: blue";
                    }
                
                    %>
                    <div class="message" style="<%= style%>">
                        <p>
                            <b><%= String.Format("{0:g}", message.Created)%></b> from
                            <%= message.Sender.Username%>
                            <%: Html.ActionLink("Reply >>", "ReplyMessage", "Message", new { msgid = message.Id.ToString() }, null)%>
                        </p>
                        <p>
                            <%= message.Text%>
                        </p>
                    </div>

                    <% if (message.Replies.Count != 0) {
                        viewListOfMessages(message.Replies.ToList(), username, indent + 2);
                    } %>
             
            <% } %>
        <% } %>
   <% } %>--%>
     
</asp:Content>
