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
            <h3>Reciervers (separator is semicolon):</h3>
            <%: Html.Hidden("PrevMessageID", (object)0)%>
		    Users: <%: Html.TextBox("ToUsers") %> <br />
            Groups: <%: Html.TextBox("ToGroups") %> <br />
            <%: Html.ValidationSummary() %>
            <h3>Text</h3>
            <%: Html.TextArea("Text", "", 15, 50, "") %> <br />
		    <input type="submit" value="Send" />
	    <% } %>
    </fieldset>

    <script type="text/javascript">
        $(function () {
            var availableTags = [
			"novakjakub",
            "novakjan",
            "bedrich",
            "ondrej",
            "jiri"
		];
            $("#ToUsers").autocomplete({
                source: availableTags
            });
        });

        $(function () {
            var availableTags = [
			"Svět",
            "Asie",
            "Evropa",
            "Česká republika",
            "Slunce"
		];
            $("#ToGroups").autocomplete({
                source: availableTags
            });
        });
	</script>

</asp:Content>
