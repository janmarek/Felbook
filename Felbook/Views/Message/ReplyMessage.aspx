<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reply Message
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Reply Message</h2>
    
    <fieldset>
        <% using (Html.BeginForm("SendMessage", "Message")) { %>
		    <%: Html.AntiForgeryToken() %>
            <h3>Recierver:</h3>
            <%: Html.Hidden("PrevMessageID", (object)Model.Id)%>
            <%: Html.Hidden("UserCounter", (object)1)%>
		    <%: Html.Hidden("ToUser1", (object)Model.Sender.Username)%>
            <%: Html.Hidden("GroupCounter", (object)1)%>
            <%: Html.Hidden("ToGroup1", "")%>
            <%= Model.Sender.Username%>
            <h3>Original message</h3>
            <%= Model.Text%>
            <%: Html.ValidationSummary() %>
            <h3>Text</h3>
            <%: Html.TextArea("Text", "", 15, 50, "") %> <br />
		    <input type="submit" value="Send" />
	    <% } %>
    </fieldset>

</asp:Content>
