<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Felbook.Models.EventViewModel>" %>

        <div class="display-label">Name:</div>
        <div class="display-field"><%: Model.Name %></div>

        <div class="display-label">From:</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.From) %></div>
        
        <div class="display-label">To:</div>
        <div class="display-field"><%: String.Format("{0:g}", Model.To) %></div>
        
        <div class="display-label">Text:</div>
        <div class="display-field"><%: Model.Text %></div>

        <% foreach (var stat in Model.Status)
           {
               Html.RenderPartial("Status", Model.CreateStatusViewModel(stat));
           }
            %>
    <p>
        <%: Html.ActionLink("Back to List", "Index") %>
    </p>