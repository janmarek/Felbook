<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="testDataContent" ContentPlaceHolderID="TitleContent" runat="server">
    Test data
</asp:Content>

<asp:Content ID="testData" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Test Data</h2>
    <p>Vygenerovaly se testovací data.</p>
    
	<p>Je možné se přihlásit na dva uživatele.</p>

	<ul>
		<li>login: novakjan</li>
		<li>heslo: 123456</li>
	</ul>

    <ul>
		<li>login: novakjakub</li>
		<li>heslo: 123456</li>
	</ul>
</asp:Content>
