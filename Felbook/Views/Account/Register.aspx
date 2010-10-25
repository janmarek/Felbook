<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.RegisterModel>" %>

<asp:Content ID="registerTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Register
</asp:Content>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Create a New Account</h2>
    <p>
        Use the form below to create a new account. 
    </p>
    <p>
        Passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.
    </p>

    <% using (Html.BeginForm()) { %>
        <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
        <div>
            <fieldset>
                <legend>Account Information</legend>

                <div class="leftRegister">

                    <!-- User name -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.UserName) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.UserName) %>
                        <%: Html.ValidationMessageFor(m => m.UserName) %>
                    </div>

                    <!-- Name -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Name) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Name) %>
                        <%: Html.ValidationMessageFor(m => m.Name) %>
                    </div>

                    <!-- Surname -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Surname) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Surname) %>
                        <%: Html.ValidationMessageFor(m => m.Surname) %>
                    </div>

                    <!-- Title -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Title) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Title) %>
                    </div>

                    <!-- Title after name-->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.TitleAfter) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.TitleAfter) %>
                    </div>


                    <!-- Password -->                    
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Password) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.PasswordFor(m => m.Password) %>
                        <%: Html.ValidationMessageFor(m => m.Password) %>
                    </div>

                    <!-- Confirm Password -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.ConfirmPassword) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.PasswordFor(m => m.ConfirmPassword) %>
                        <%: Html.ValidationMessageFor(m => m.ConfirmPassword) %>
                    </div>


                    <!-- Faculty -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Faculty) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Faculty) %>
                    </div>

                    <!-- Role -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Role) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Role) %>
                    </div>

                    <!-- Study programme -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.StudyProgramme) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.StudyProgramme) %>
                    </div>

                    <!-- Specialization -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Specialization)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Specialization)%>
                    </div>

                </div>

                <div class="rightRegister">

                    <!-- E-mail address -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Email) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Email) %>
                        <%: Html.ValidationMessageFor(m => m.Email) %>
                    </div>

                    <!-- ICQ -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.ICQ)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.ICQ)%>
                    </div>

                    <!-- School email address -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.SchoolEmail)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.SchoolEmail)%>
                    </div>

                    <!-- Phone -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Phone)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Phone)%>
                    </div>
                </div>
                
                <p>
                    &nbsp;<input type="submit" value="Register" /></p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
