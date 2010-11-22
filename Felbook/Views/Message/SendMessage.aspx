<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.SendMessageModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Send Message
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Messages (<%= Page.User.Identity.Name%>)</h2>

    <h3>
    <%: Html.ActionLink("Messages", "Index", "Message", new { page = 1.ToString() }, null)%>, 
    Send message
    </h3>
    
    <fieldset>    
	    <% using (Html.BeginForm("SendMessage", "Message")) { %>
		    <%: Html.AntiForgeryToken() %>
            <h3>Reciervers:</h3>
            <%: Html.Hidden("PrevMessageID", (object)-1)%>
		    
            <%: Html.Hidden("UserCounter", (object)1)%>
            <table id ="userInput">
                <tr>
                    <td>Users:</td>
                    <td><input type="text" id="ToUser1" name="ToUser1" /></td>
                </tr>
            </table>
            <input type="button" value="+" id="addUserBox" />
            <input type="button" value="-" id="removeUserBox" /> <br />

            <%: Html.Hidden("GroupCounter", (object)1)%>
            <table id ="groupInput">
                <tr>
                    <td>Groups:</td>
                    <td><input type="text" id="ToGroup1" name="ToGroup1" /></td>
                </tr>
            </table>
            <input type="button" value="+" id="addGroupBox" />
            <input type="button" value="-" id="removeGroupBox" /> <br />

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

    <script type="text/javascript">

        var availableTagsUsers = [
            <%= Model.AutocompleteUsers%>
		];

        var availableTagsGroups = [
            <%= Model.AutocompleteGroups%>
		];
        
	</script>

</asp:Content>
