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
     <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm("Register", "Account", FormMethod.Post, new { enctype = "multipart/form-data" }))
       { %> 
        <%: Html.AntiForgeryToken() %>
        <div>

	<div class="tabs">
		<ul>
			<li><a href="#account">Accout information</a></li>
			<li><a href="#education">Education</a></li>
			<li><a href="#othercontacts">Other user contact</a></li>
            <li><a href="#profilepicture">Profile picture</a></li>
		</ul>

        <div id="account">
            <fieldset>
                <legend>Accout information</legend>

                    <!-- User name -->
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.UserName)%>
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

                    <!-- E-mail address -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Email) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Email) %>
                        <%: Html.ValidationMessageFor(m => m.Email) %>
                    </div>
                </fieldset>
</div>

<div id="education">
                <fieldset>
                <legend>Education</legend>
                    <!-- Faculty -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Faculty) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Faculty) %>
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

                    <!-- Title -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Title) %>
                    </div>
                    <div class="editor-field">                                               
                        <%: Html.TextBoxFor(m => m.Title, new { @readonly = "readonly" })%>
                        <%: Html.ValidationMessageFor(m => m.Title) %>
                        
                         <select id="selectTitle">
                            <option value="bc" selected="selected">Bc.</option>
                            <option value="bca">BcA.</option>
                            <option value="ing">Ing.</option>
                            <option value="ingarch">Ing.arch.</option>
                            <option value="mudr">MUDr.</option>                           
                            <option value="mvdr">MVDr.</option>
                            <option value="mga">MgA.</option>
                            <option value="mgr">Mgr.</option>
                            <option value="judr">JUDr.</option>
                            <option value="phdr">PhDr.</option>
                            <option value="rndr">RNDr.</option>
                            <option value="pharmdr">PharmDr.</option>
                            <option value="thlic">ThLic.</option>
                            <option value="thdr">ThDr.</option>
                            <option value="prof">prof.</option>
                            <option value="doc">doc.</option>
                            <option value="peadr">PaedDr.</option>
                            <option value="dr">Dr.</option>
                            <option value="phmr">PhMr.</option>
                        </select>
                        <input type="button" id="addTitle" value="Add Title" />
                        <input type="button" id="resetTitle" value="Reset" />                      
                    </div>

                    <!-- Title after name-->
                    <div class="editor-label">               
                        <%: Html.LabelFor(m => m.TitleAfter) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.TitleAfter, new { @readonly = "readonly" })%>
                        <%: Html.ValidationMessageFor(m => m.TitleAfter)%>

                        <select id="selectTitleAfter">               
                            <option value="phd" selected="selected">Ph.D.</option>
                            <option value="thd" selected="selected">Th.D.</option>
                            <option value="csc" selected="selected">CSc.</option>
                            <option value="drsc" selected="selected">DrSc.</option>
                            <option value="drhc" selected="selected">dr. h. c.</option>
                            <option value="dr" selected="selected">Dr.</option>
                            <option value="phmr" selected="selected">PhMr.</option>
                            <option value="dis" selected="selected">DiS.</option>                           
                        </select>

                        <input type="button" id="addTitleAfter" value="Add Title" />
                        <input type="button" id="resetTitleAfter" value="Reset" />

                    </div>
                </fieldset>
</div>

<div id="othercontacts">
                <fieldset>
                <legend>Other user contact</legend>

                    <!-- ICQ -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.ICQ)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.ICQ)%>
                        <%: Html.ValidationMessageFor(m => m.ICQ) %>
                    </div>

                    <!-- School email address -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.SchoolEmail)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.SchoolEmail)%>
                        <%: Html.ValidationMessageFor(m => m.SchoolEmail) %>
                    </div>

                    <!-- Phone -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Phone)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Phone)%>
                        <%: Html.ValidationMessageFor(m => m.Phone)%>
                    </div>
                    </fieldset>
                    </div>

              <div id="profilepicture">
                        <fieldset>
                        <legend>Profile picture</legend>
                        <ul>
                            <li>Picture profile image can be only <b>jpg, gif or png</b> type</li>
                            <li>Your picture will be truncated into size 90 * 120 pixels</li>
                            <li>It's just as big as this black box<div style="width: 90px; height: 120px; background-color: Black;"></div></li>
                        </ul>
                        <p>
                          <input type="file" id="profileimage" name="profileimage" />
                        </p>

                        </fieldset>
                    </div>

                </div>
                <p>&nbsp;<input type="submit" value="Register" /></p>
        </div>
    <% } %>
</asp:Content>
