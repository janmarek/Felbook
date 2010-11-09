<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.FollowingsViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.User.FullName %> follows
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.User.FullName %> follows</h2>

	<% Html.RenderPartial("UserList", new Felbook.Models.UserListViewModel(Model.CurrentUser, Model.Followings)); %>

</asp:Content>
