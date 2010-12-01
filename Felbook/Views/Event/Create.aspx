<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.Event>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Create
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Create</h2>

    <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary("There were some errors:")%>
        
        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Name) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Name) %>
                <%: Html.ValidationMessageFor(model => model.Name) %>
            </div>
                                 
            <div class="editor-label">
                <%: Html.LabelFor(model => model.From) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.From) %>
                <%: Html.ValidationMessageFor(model => model.From) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.To) %>
            </div>
            <div class="editor-field"> <!-- String.Format("{0:g}", Model.To)-->
                <%: Html.TextBoxFor(model => model.To) %>
                <%: Html.ValidationMessageFor(model => model.To) %>
            </div>
            
            <div class="editor-label">
                <%: Html.LabelFor(model => model.Text) %>
            </div>
            <div class="editor-field">
                <%: Html.TextBoxFor(model => model.Text) %>
                <%: Html.ValidationMessageFor(model => model.Text) %>
            </div>

            <div>
                <p>Group:</p>
                <input type="text" id="Group" name="Group" />
            </div>

            
                <%  
       
       } %>

            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <div>
        <%: Html.ActionLink("Back to Events", "Index") %>
    </div>

</asp:Content>

