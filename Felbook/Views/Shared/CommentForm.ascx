<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Felbook.Models.CommentModel>" %>

<% using (Html.BeginForm("AddComment", "Status"))
    { %>
    <%: Html.TextBoxFor(m => m.Text) %>
    <%: Html.Hidden("PrevUrl", Request.RawUrl) %>
    <%: Html.Hidden("StatusID", Model.StatusID) %>
    <input type="submit" value="Add comment" />
<%} %>

