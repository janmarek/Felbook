<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditProfile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>EditProfile</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>

		<%: Html.AntiForgeryToken() %>
        
        <fieldset>            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Surname) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Surname) %>
                <%: Html.ValidationMessageFor(model => model.Surname) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Mail) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Mail) %>
                <%: Html.ValidationMessageFor(model => model.Mail) %>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

</asp:Content>

