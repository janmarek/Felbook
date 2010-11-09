<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Messages (<%= Page.User.Identity.Name%>)</h2>

    <h3>
        Recieved messages,
        <%: Html.ActionLink("Send message", "SendMessage", "Message") %>
    </h3>


   <%-- <div class="tabs">
        <ul>
			<li><a href="#mgs">Messages</a></li>
			<li><a href="#send">Send message</a></li>
		</ul>

        <div id="msg">--%>

            <% foreach (var message in Model.MessageList)
               {
                   string attributes = "style=\"margin-left: " + message.Indent + "px;\" class=\"message ";

                   if (message.Recieved)
                   {
                       attributes += "recieved\"";
                   }
                   else
                   {
                       attributes += "sent\"";
                   }
                   %>

                   <div <%=attributes %>>
                       <p>
                           <strong><%= String.Format("{0:g}", message.Sent)%></strong> 
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
                       <%: Html.ActionLink("Message detail", "Detail", new { id = message.ID.ToString() })%>
                   </div>

            <% } %>

            <div class="msgNavigation">
                <% if (Model.ActualPage == 1)
                   { %>
                  <%-- << Previous page--%>
                <% }
                   else
                   { %>
                    <%: Html.ActionLink("<< Previous page", "Index", "Message", new { page = (Model.ActualPage - 1).ToString() }, null)%>
                <% } %>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <% if (Model.ActualPage == Model.LastPage)
                   { %>
                   <%--Next page >>--%>
                <% }
                   else
                   { %>
                    <%: Html.ActionLink("Next page >>", "Index", "Message", new { page = (Model.ActualPage + 1).ToString() }, null)%>
                <% } %>
            </div>

       <%-- </div>


        <div id="send">

            <% using (Html.BeginForm("SendMessage", "Message")) { %>
	            <%: Html.AntiForgeryToken() %>
                <h3>Reciervers (separator is space):</h3>
                <%: Html.Hidden("PrevMessageID", (object)0)%>
	            Users: <%: Html.TextBox("ToUsers") %> <br />
                Groups: <%: Html.TextBox("ToGroups") %> <br />
                <%: Html.ValidationSummary() %>
                <h3>Text</h3>
                <%: Html.TextArea("Text", "", 15, 50, "") %> <br />
	            <input type="submit" value="Send" />
            <% } %>

--%>
     
</asp:Content>
