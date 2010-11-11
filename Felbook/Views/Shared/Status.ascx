<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Felbook.Models.Status>" %>
<p>
<b>
<%
	// ukáže že je to staus ke skupině nebo k uživateli
	if (Model.Group == null)
	{
    %>
User: <%= Model.User.Username %>,
	<%
		} else {
	%>
Group: <%= Model.Group.Name %>,
    <%
		}
	%>
<%= Model.Created %></b> - <%: Model.Text %></p>
<%
	// images
	if (Model.Images.Count > 0)
	{
%>
<div>
	<%
		foreach (var img in Model.Images)
		{
	%>
	<a href="/Web_Data/status_images/<%= Model.User.Id + "/" + img.FileName %>" class="colorbox"
		rel="status<%= Model.Id %>">
		<img src="/Web_Data/status_images/<%= Model.User.Id + "/" + img.FileName %>" title="<%= img.Description %>"
			alt="<%= img.Description %>" width="60" height="80" />
	</a>
	<%
		}
	%>
</div>
<% } %>
        
        
        <% if(Model.Files.Count > 0) { //pokud jsou ve statusu soubory tak se zobrazí %>
        <div>
        <br />
        <span>Files:</span>
        <% foreach (var file in Model.Files)
           { %>
	        <p>
            <a href="/Web_Data/status_files/<%=  Model.User.Id + "/" + file.FileName %>"><%= file.FileName %></a>,
            -
            <%= file.Description %>
            </p>
        <% } %>
        </div>
        <% } %>


<%
	// links
	if (Model.Links.Count > 0)
	{
%>
<p>
	<span>Links:</span>
	<% for (int i = 0; i < Model.Links.Count; i++)
	{ %>
	<a href="<%= Model.Links.ElementAt(i).URL %>"><%= Model.Links.ElementAt(i).URL %></a>
	<% } %>
</p>
<% } %>
