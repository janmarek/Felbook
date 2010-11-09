<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Profile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    

        <script type="text/javascript">

            

        </script>
        
    <p><%= Html.ActionLink("Edit profile", "Edit") %></p>
	
    <h2>Profile <%= Model.Name %> <%= Model.Surname %></h2>
	<ul>
		<li>E-mail: <%= Model.Mail %></li>
	</ul>
	<h3>Add status</h3>
    <% Session["links"]=null; //vymazání session s linky kvůli ajaxovému přidávání linků %>
    <%: Html.ValidationSummary("There were some errors:") %>
    <% using (Html.BeginForm("AddStatus", "Profile", FormMethod.Post, new { enctype = "multipart/form-data" }))
       { %>	
		<%: Html.AntiForgeryToken() %>
<%--		<table id="fileInput">
        <tr>
            <th>
                File:
            </th>
            <th>
                Description:
            </th>
        </tr>
        <tr>
                <td>
                    <input type="button" id="addFile" value="Add file" />
                    <input type="button" id="removeFile" value="Remove file" />
                </td>
                <td>            
                </td>
            </tr>
            <tr>
                <td>
                    <input type="file" id="file1" name="file1" />
                </td>
                <td>
                    <textarea id="descriptionfile1" name="descriptionfile1" rows="4" cols="20">
                    </textarea>                    
                </td>
            </tr>   
        </table>--%>
        <table id="imageInput">
            <tr>
                <td>
                    <%: Html.Label("Status text:") %>
                </td>
                <td>
                    <%: Html.TextArea("status", new { rows = "4", cols = "20" })%>
                </td>
            </tr>
            <tr>
                <th>
                    Picture:
                </th>
                <th>
                    Description:
                </th>
            </tr>
            <tr>
                <td>
                    <input type="button" id="addImg" value="Add image" />
                    <input type="button" id="removeImg" value="Remove image" />
                </td>
                <td>            
                </td>
            </tr>
            <tr>
                <td>
                    <input type="file" id="picture1" name="picture1" />
                </td>
                <td>
                    <textarea id="description1" name="description1" rows="4" cols="20">
                    </textarea>                    
                </td>
            </tr>   
        </table>

        <p>Links for upload:</p>      
        <div>
            <table id="links">
            </table>
        </div>

        <input type="submit" value="Add new status" onclick="formSubmit()" />
        <% } %>
        <h3>Links:</h3>
        <% Ajax.BeginForm("SetLinksContent", "Profile", new AjaxOptions
           {
               OnComplete = "AddLink"
           });
         { %>         
            <%= Html.TextBox("newLink") %>
            <input type="submit" value="Add Link" />
         <%  } %>
         

        <hr />
        <h3>My statuses</h3>
	<% foreach (var status in Model.Statuses.OrderByDescending(status => status.Id)) //seřadí sestupně podle ID statusu
    { %>
	<p><b><%= String.Format("{0:g}", status.Created) %></b> - <%= status.Text %></p>
        
        
        <% foreach (var img in status.Images) { %>
        <a href="/Web_Data/status_images/<%= String.Format("{0:g}", status.User.Id + "/" + img.FileName) %>" class="colorbox"><img src="/Web_Data/status_images/<%= String.Format("{0:g}", status.User.Id + "/" + img.FileName) %>" title="<%= String.Format("{0:g}", img.Description) %>" alt="<%= String.Format("{0:g}", img.Description) %>" width="60" height="80" /></a>    
        <% } %>
        
        <% if(status.Links.Count > 0) { //pokud jsou ve statusu linky tak se zobrazí %>
        <p>
        <span>Links:</span>
        <% foreach (var link in status.Links) { %>
	        <a href="<%= String.Format("{0:g}", link.URL) %>"><%= String.Format("{0:g}", link.URL) %></a>,
        <% } %>
        </p>
        <% } %>
    <% } %>

</asp:Content>

