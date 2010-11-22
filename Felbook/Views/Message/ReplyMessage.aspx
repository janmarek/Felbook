<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.SendMessageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Reply Message
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Reply Message</h2>
    
    <fieldset>
        <% using (Html.BeginForm("SendMessage", "Message")) { %>
		    <%: Html.AntiForgeryToken() %>
            <h3>Recierver:</h3>
            <%: Html.Hidden("PrevMessageID", (object)Model.prevMessage.Id)%>
            <%: Html.Hidden("UserCounter", (object)1)%>
		    <%: Html.Hidden("ToUser1", (object)Model.prevMessage.Sender.Username)%>
            <%: Html.Hidden("GroupCounter", (object)1)%>
            <%: Html.Hidden("ToGroup1", "")%>
            <%= Model.prevMessage.Sender.Username%>
            <h3>Original message</h3>
            <%= Model.prevMessage.Text%>
            
            <%: Html.ValidationSummary(true) %>
            
            <div class="editor-label">
                <h3><%: Html.LabelFor(m => m.Text)%></h3>
            </div>
            <div class="editor-field">
                <%: Html.TextArea("Text", "", 15, 50, "") %> <br />
                <%: Html.ValidationMessageFor(m => m.Text) %> <br />
            </div>
            <input type="submit" value="Send" />
	    <% } %>
    </fieldset>

</asp:Content>
