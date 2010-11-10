<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

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
            <%: Html.Hidden("PrevMessageID", (object)0)%>
		    
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

            <%: Html.ValidationSummary() %>
            <h3>Text</h3>
            <%: Html.TextArea("Text", "", 15, 50, "") %> <br />
		    <input type="submit" value="Send" />
	    <% } %>
    </fieldset>

    <script type="text/javascript">

        var availableTagsUsers = [
            <%--= Model.AutocompleteUsers--%>
			"novakjakub",
            "novakjan",
            "bedrich",
            "ondrej",
            "jiri"
		];

        var availableTagsGroups = [
            <%--= Model.AutocompleteGroups--%>
			"Svět",
            "Asie",
            "Evropa",
            "Česká republika",
            "Slunce"
		];
        
	</script>

</asp:Content>
