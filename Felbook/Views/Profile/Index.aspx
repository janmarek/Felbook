<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.ProfileViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <div class="tabs">
		<ul>
			<li><a href="#statuses">Statuses</a></li>
			<li><a href="#profile">Profile details</a></li>
		</ul>
        <div id="statuses">   
	<h2>
		Profile
		<%= Model.User.Name %>
		<%= Model.User.Surname %>
		<% Html.RenderPartial("FollowLink", new Felbook.Models.FollowLinkViewModel(Model.CurrentUser, Model.User)); %>
	</h2>
	<h3>
		Add status</h3>
	<% using (Html.BeginForm("AddStatus", "Profile", FormMethod.Post, new { enctype = "multipart/form-data" }))
	{
		Html.RenderPartial("AddStatusFormContent");
	} %>
	<h3>
		My statuses</h3>
	<% foreach (var status in Model.User.Statuses.OrderByDescending(status => status.Id))
	{ %>
	<% Html.RenderPartial("Status", Model.CreateStatusViewModel(status)); %>
	<% } %>
            </div>
            <div id="profile">   
                <p>
                    <%= Html.ActionLink("Edit profile", "Edit", new { username = Page.User.Identity.Name }, null)%>
                </p>
			<h2>Main information:</h2>
            <ul>
                <li>Name: <%= Model.User.FullName %></li>
            <% if (!String.IsNullOrEmpty(Model.User.TitleAfter))
               { %>
               <li>Name with titles: <%= Model.User.Title %> <%= Model.User.Username %>, <%= Model.User.TitleAfter %></li>              
            <% }
               else if (!String.IsNullOrEmpty(Model.User.Title))
               { %> 
               <li>Name with titles: <%= Model.User.Title %> <%= Model.User.Username %></li>
            <% } %> 
		        <li>Username: <%= Model.User.Username %></li>
                <li>Email: <%= Model.User.Mail %></li>
			</ul>
            
            <h2>Education</h2>
			<ul>
                <li>Study programme: <%= Model.User.StudyProgramme %></li>
                <li>Specialization: <%= Model.User.Specialization %></li>
            </ul>

            <h2>Other</h2>
            <ul>	    
                <li>School email: <%= Model.User.SchoolEmail %></li>
                <li>ICQ: <%= Model.User.ICQ %></li>
                <li>Phone: <%= Model.User.Phone %></li>
			</ul>
            Profile image:
            <br />
            <img alt="profile image" title="profile image" src="../Web_Data/profile_images/<%= Model.User.Id %>/profileimage.png" />
            </div>
      </div>
</asp:Content>
