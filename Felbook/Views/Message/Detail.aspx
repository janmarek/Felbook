<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.Message>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Message detail</h2>

    <%
        string attributes = "class=\"messageDetail ";
        if (Model.Sender.Username != Page.User.Identity.Name)
        {
            attributes += "recieved\"";
        }
        else
        {
            attributes += "sent\"";
        }
    %>

    <fieldset <%=attributes %>>
                
        <strong>Sent at <%: String.Format("{0:g}", Model.Created) %></strong><br />
        <strong>From: <%: Model.Sender.Username %></strong><br />
        <strong>To:
        <%  IEnumerator<Felbook.Models.User> iterator = Model.Recievers.GetEnumerator();
            if (iterator.MoveNext())
            { %><%: iterator.Current.Username %><% } while (iterator.MoveNext()) { %>, <%: iterator.Current.Username %><% } %>
        </strong><br />
        <br />

        <% if (Model.ReplyTo != null)
           { %>
            <strong><%: Html.ActionLink("Reply to message:", "Detail", new { id = Model.ReplyTo.Id.ToString() })%></strong>
            <p><%: Model.ReplyTo.Text %></p>
        <% } %>

        <strong>Text:</strong>
        <p><%: Model.Text %></p>
       
    </fieldset>

    <p>
         <%: Html.ActionLink("Back to List", "Index", "Message", new { page = 1.ToString() }, null)%> 
         <% if (Model.Sender.Username != Page.User.Identity.Name)
            { %>
         | <%: Html.ActionLink("Reply message", "ReplyMessage", "Message", new { msgid = Model.Id.ToString() }, null)%>
         | <%: Html.ActionLink("Mark as unread", "UnreadMessage", "Message", new { msgid = Model.Id.ToString() }, null)%>
         <% } %>
    </p>

</asp:Content>

