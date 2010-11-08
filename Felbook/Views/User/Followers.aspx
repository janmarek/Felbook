<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.FollowersViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.User.FullName %> is followed by
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.User.FullName %> is followed by</h2>

	<% Html.RenderPartial("UserList", new Felbook.Models.UserListViewModel(Model.CurrentUser, Model.Followers)); %>

</asp:Content>
