<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.EventViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

        <legend>Fields</legend>
    <fieldset>
        <% Html.RenderPartial("Event", Model); %>

        <% Html.RenderPartial("AddStatusFormContent", new Felbook.Models.StatusFormModel()); %>
    </fieldset>    
    <p>
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

