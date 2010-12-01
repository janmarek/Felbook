<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.Event>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">From:</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.From) %></div>
        
        <div class="display-label">To:</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.To) %></div>
        
        <div class="display-label">Name:</div>
        <div class="display-field"><%: Model.Name %></div>
        
        <div class="display-label">Text:</div>
        <div class="display-field"><%: Model.Text %></div>
        
    </fieldset>
        <% foreach (var stat in Model.Status)
           {
               Html.RenderPartial("Status");
           }
            %>


        <% Html.RenderPartial("AddStatusFormContent", new Felbook.Models.StatusFormModel()); %>
    <p>
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

