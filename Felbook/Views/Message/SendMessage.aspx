﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.SendMessageModel>" %>

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
		    
            <table>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelFor(m => m.ToUsers)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(m => m.ToUsers)%>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="editor-label">
                            <%: Html.LabelFor(m => m.ToGroups)%>
                        </div>
                    </td>
                    <td>
                        <div class="editor-field">
                            <%: Html.TextBoxFor(m => m.ToGroups)%>
                        </div>
                    </td>
                </tr>
            </table>
            
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

    <script type="text/javascript">

        var availableTagsUsers = [
            <%= Model.AutocompleteUsers%>
		];

        var availableTagsGroups = [
            <%= Model.AutocompleteGroups%>
		];
        
	</script>

</asp:Content>
