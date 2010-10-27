<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Profile <%= Model.Name %> <%= Model.Surname %></h2>
	<ul>
		<li>E-mail: <%= Model.Mail %></li>
	</ul>

	<h3>Add status</h3>
	   
    <%: Html.ValidationSummary("Please correct the errors and try again.") %>
    <% using (Html.BeginForm("AddStatus", "Profile", FormMethod.Post, new { enctype = "multipart/form-data" }))
       { %>	
		<%: Html.AntiForgeryToken() %>
		<p>
        <%: Html.TextBox("status") %>
        </p>
        <p>
        <label for="Picture">Picture:</label>
        <input type="file" id="picture" name="picture" />
		<%=Html.ValidationMessage("picture", "*")%>
        </p>
        <p>
        <%: Html.Label("Description:") %>
        <br />
        <%: Html.TextArea("description", new { rows = "4", cols = "20" })%>
        </p>
        <input type="submit" value="Send" />
	<% } %>

	<h3>My statuses</h3>
	<% foreach (var status in Model.Statuses) { %>
	<p><b><%= String.Format("{0:g}", status.Created) %></b> - <%= status.Text %></p>
        <% foreach (var img in status.Images) { %>
        <p><img src="/Web_Data/status_images/<%= String.Format("{0:g}", status.User.Id + "/" + img.FileName) %>" alt="<%= String.Format("{0:g}", img.Description) %>" title="<%= String.Format("{0:g}", img.Description) %>" width="60" height="80" /></p>
        <% } %>
	<% } %>

</asp:Content>

