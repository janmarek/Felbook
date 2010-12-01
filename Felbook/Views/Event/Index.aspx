<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Felbook.Models.Event>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Index</h2>

    <table>
        <tr>
            <th></th>

            <th>
                From
            </th>
            <th>
                To
            </th>
            <th>
                Name
            </th>
            <th>
                Text
            </th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>                
                <%: Html.ActionLink("Delete", "Delete", new { id=item.Id })%>
                <%: Html.ActionLink("Details", "Details", new { id = item.Id })%>
            </td>

            <td>
                <%: String.Format("{0:g}", item.From) %>
            </td>
            <td>
                <%: String.Format("{0:g}", item.To) %>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: item.Text %>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

