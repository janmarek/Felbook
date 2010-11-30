<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Felbook.Models.StatusViewModel>" %>
<p class="detail">By <%= Html.ActionLink(Model.Status.User.FullName, "Index", "Profile", new {username = Model.Status.User.Username}, null) %>

<%
	// ukáže že je to staus ke skupině nebo k uživateli
	if (Model.Status.Group != null)
	{
    %>
in group <%= Html.ActionLink(Model.Status.Group.Name, "Detail", "Group", new { id = Model.Status.Group.Id }, null) %>
    <%
		}
	%>
at <%= Model.Status.Created%></p>

<p class="big"><%: Model.Status.Text%></p>
<%
	// images
	if (Model.Status.Images.Count > 0)
	{
%>
<div>
	<%
		foreach (var img in Model.Status.Images)
		{
	%>
	<%= Model.ImageOutput.GetHtml(img) %>
	<%
		}
	%>
</div>
<% } %>
        
        
        <% if (Model.Status.Files.Count > 0)
		   { //pokud jsou ve statusu soubory tak se zobrazí %>
        <div>
        <br />
        <span>Files:</span>
        <% foreach (var file in Model.Status.Files)
           { %>
	        <p>
			<%= Model.FileOutput.GetHtml(file) %> - <%= file.Description %>
            </p>
        <% } %>
        </div>
        <% } %>


<%
	// links
	if (Model.Status.Links.Count > 0)
	{
%>
<p>
	<span>Links:</span>
	<% foreach (var link in Model.Status.Links)
	{ %>
	<a href="<%= link.URL %>"><%= link.URL%></a> - <%= link.Description %>
	<% } %>
</p>
<% } %>

<div class="comments">
    <% foreach (var comment in Model.Status.Comments)
       { %>
       <div class="comment">
            from
            <%= Html.ActionLink(comment.Author.FullName, "Index", "Profile", new {username = comment.Author.Username}, null) %>
            at
            <%= comment.Created %> <br />
            <p><%= comment.Text %></p>
       </div>
    <% }
        if (Request.IsAuthenticated)
        { 
            Html.RenderPartial("CommentForm", new Felbook.Models.CommentModel { StatusID = Model.Status.Id });
        }%>
</div>