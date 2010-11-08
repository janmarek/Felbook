<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.Message>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Detail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Message detail</h2>

    <%
        string style;
        if (Model.Sender.Username != Page.User.Identity.Name)
        {
            style = "style=\"border-color: green\"";
        }
        else
        {
            style = "style=\"border-color: blue\"";
        }
    %>

    <fieldset class="messageDetail" <%=style %>>
                
        <b>Sent at <%: String.Format("{0:g}", Model.Created) %></b><br />
        <b>From: <%: Model.Sender.Username %></b><br />
        <b>To:
        <%  IEnumerator<Felbook.Models.User> iterator = Model.Users.GetEnumerator();
            if (iterator.MoveNext())
            { %><%: iterator.Current.Username %><% } while (iterator.MoveNext()) { %>, <%: iterator.Current.Username %><% } %>
        </b><br />
        <br />

        <% if (Model.ReplyTo != null)
           { %>
            <b><%: Html.ActionLink("Reply to message:", "Detail", new { id = Model.ReplyTo.Id.ToString() })%></b>
            <p><%: Model.ReplyTo.Text %></p>
        <% } %>

        <b>Text:</b>
        <p><%: Model.Text %></p>
       
    </fieldset>

    <p>
         <%: Html.ActionLink("Back to List", "Index") %> 
         <% if (Model.Sender.Username != Page.User.Identity.Name)
            { %>
         | <%: Html.ActionLink("Reply message", "ReplyMessage", "Message", new { msgid = Model.Id.ToString() }, null)%>
         <% } %>
    </p>

</asp:Content>

