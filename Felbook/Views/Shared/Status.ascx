<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Felbook.Models.Status>" %>
<p><b><%= String.Format("{0:g}", Model.Created) %></b> - <%= Model.Text %></p>
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
