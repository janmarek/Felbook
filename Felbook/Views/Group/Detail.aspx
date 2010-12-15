<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.GroupViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Group <%: Model.Group.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Group <%: Model.Group.Name%></h2>

	<% if (Model.Group.Parent != null)
	{ %>
	<p>Parent group: <%: Html.ActionLink(Model.Group.Parent.Name, "Detail", new { id = Model.Group.Parent.Id })%></p>
	<% } %>
	
	<% if (Model.Group.Creator != null)
	{ %>
	<p>
		Creator:
		<%: Html.ActionLink(Model.Group.Creator.FullName, "Index", "Profile", new { username = Model.Group.Creator.Username }, null) %>
		<% Html.RenderPartial("FollowLink", new Felbook.Models.FollowLinkViewModel(Model.CurrentUser, Model.Group.Creator)); %>
		</p>
	<% } %>

	<p><%: Model.Group.Description%></p>

	<% if (Request.IsAuthenticated)
	{ %>
	<p>
		<% if (!Model.Group.HasMember(Model.CurrentUser))
		{ %>
		<%: Html.ActionLink("Join group", "Join", new { id = Model.Group.Id })%>
		<% }
		else
		{ %>
		<%: Html.ActionLink("Leave group", "Leave", new { id = Model.Group.Id })%>
		<% } %> |
		<%: Html.ActionLink("Add subgroup", "CreateSubGroup", new { id = Model.Group.Id })%>
		<% if (Model.Group.IsAdminedBy(Model.CurrentUser))
		{ %>
		| <%= Html.ActionLink("Edit group", "Edit", new { id = Model.Group.Id })%>
		<% } %>
		</p>
	<% } %>

	<div class="tabs">
		<ul>
			<li><a href="#wall">Wall</a></li>
			<li><a href="#info">Detailed information</a></li>
			<li><a href="#subgroups">Subgroups</a></li>
			<li><a href="#members">Members</a></li>
            <li><a href="#events">Events</a></li>
		</ul>

		<div id="wall">
			<%
				if (Request.IsAuthenticated && Model.Group.HasMember(Model.CurrentUser))
				{
					using (Html.BeginForm("AddStatus", "Group", new { id = Model.Group.Id }, FormMethod.Post, new { enctype = "multipart/form-data", @class = "statusForm" }))
					{
						Html.RenderPartial("AddStatusFormContent");
					}
				}

				foreach (var status in Model.Group.Statuses.OrderByDescending(s => s.Id))
				{
					Html.RenderPartial("Status", Model.CreateStatusViewModel(status));
				}
			%>
		</div>

		<div id="info">
			<h3>Description</h3>
			<p><%: Model.Group.Description%></p>

			<% if (Model.Group.HasParent())
			{ %>
			<h3>Parent groups</h3>
			<ul>
				<% foreach (var group in Model.Group.Parents) { %>
				<li><%: Html.ActionLink(group.Name, "Detail", new {id = group.Id}) %></li>
				<% } %>
			</ul>
			<% } %>

			<h3>Admins</h3>
			<% Html.RenderPartial("UserList", new Felbook.Models.UserListViewModel(Model.CurrentUser, Model.Group.Administrators)); %>

			<% if (Model.Group.Creator != null)
			{ %>
			<h3>Creator</h3>
			<ul>
			<li>
				<%: Html.ActionLink(Model.Group.Creator.FullName, "Index", new { username = Model.Group.Creator.Username })%>
				<% Html.RenderPartial("FollowLink", new Felbook.Models.FollowLinkViewModel(Model.CurrentUser, Model.Group.Creator)); %>
			</li>
			</ul>
			<% } %>
		</div>

		<div id="subgroups">
			<ul>
			<% foreach (var group in Model.Group.Children)
			{ %>
			<li><%: Html.ActionLink(group.Name, "Detail", new {id = group.Id}) %></li>
			<% } %>
			</ul>
		</div>

        <div id="events">
            <p>
                <%: Html.ActionLink("Create New", "Create", "Event") %>
            </p>
            <ul>
            <% foreach (var ev in Model.Group.Events)
               { %>
               <li>
               <% Html.RenderPartial("Event", new Felbook.Models.EventViewModel{
                      Status = ev.Status,
                      CurrentUser = ev.User,
                      Group = ev.Group,
                      From = ev.From,
                      To = ev.To,
                      Name = ev.Name,
                      Text = ev.Text,
                      ImageOutput = Model.ImageOutput,
                      FileOutput = Model.FileOutput,});
               %>
               </li>
               <% } %>
            </ul>
        </div>

		<div id="members">
			<% Html.RenderPartial("UserList", new Felbook.Models.UserListViewModel(Model.CurrentUser, Model.Group.Users)); %>
		</div>
	</div>

	

</asp:Content>
