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
            <%: Html.Hidden("PrevMessageID", (object)Model.PrevMessage.Id)%>
            <%: Html.Hidden("ToUsers", (object)Model.PrevMessage.Sender.Username)%>
            <%: Html.Hidden("ToGroups", "")%>
            <%= Model.PrevMessage.Sender.Username%>
            
            <h3>Original message</h3>
            <%= Model.PrevMessage.Text%>
            
            <%: Html.ValidationSummary(true) %>
            
            <div class="editor-label">
                <h3><%: Html.LabelFor(m => m.Text)%></h3>
            </div>
            <div class="editor-field">
                <%: Html.TextAreaFor(m => m.Text, 15, 50, "")%> <br />
                <%: Html.ValidationMessageFor(m => m.Text) %> <br />
            </div>
            <input type="submit" value="Send" />
	    <% } %>
    </fieldset>

</asp:Content>
