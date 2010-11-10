 <%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Felbook.Models.Group>" %>
 
 <% using (Html.BeginForm()) {%>
	<%= Html.AntiForgeryToken() %>

    <%= Html.ValidationSummary(true) %>

    <fieldset>            
        <div class="editor-label">
            <%= Html.LabelFor(model => model.Name) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.Name) %>
            <%= Html.ValidationMessageFor(model => model.Name) %>
        </div>
            
        <div class="editor-label">
            <%= Html.LabelFor(model => model.Description) %>
        </div>
        <div class="editor-field">
            <%= Html.TextBoxFor(model => model.Description) %>
            <%= Html.ValidationMessageFor(model => model.Description) %>
        </div>
            
        <p>
            <input type="submit" value="OK" />
        </p>
    </fieldset>

<% } %>