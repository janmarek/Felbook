<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.Event>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete</h2>
    
    <div>
        <p>Are you shure you want to delete event:" <%: Model.Name %> "?</p>
    </div>
    <% using (Html.BeginForm()) {%>
    <%: Html.Hidden("EventID", (object)Model.Id)%>
    <p>
       <input type="submit" value="Delete" />
    </p>
    <% } %>

    <div>
        <%: Html.ActionLink("Back to Events", "Index") %>
    </div>
</asp:Content>
