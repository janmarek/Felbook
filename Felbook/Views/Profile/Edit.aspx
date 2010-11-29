<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Felbook.Models.User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditProfile
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2>Edit your Account</h2>
    <p>
        Use the form below to edit account. 
    </p>
    <p>
        Passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.
    </p>
    <%: Html.ValidationSummary(true, "Account editation was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm("Edit", "Account", FormMethod.Post, new { enctype = "multipart/form-data" }))
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
                        <%: Html.LabelFor(model => model.Username)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Username)%>
                        <%: Html.ValidationMessageFor(m => m.Username)%>
                    </div>
 
                    <!-- Name -->
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Name)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Name)%>
                        <%: Html.ValidationMessageFor(m => m.Name) %>
                    </div>

                    <!-- Surname -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.Surname) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(m => m.Surname)%>
                        <%: Html.ValidationMessageFor(m => m.Surname) %>
                    </div>

                    <!-- Password -->
                    <div class="editor-label">
                        <%: Html.LabelFor(m => m.OldPassword) %>
                    </div>
                    <div class="editor-field">
                        <%: Html.PasswordFor(m => m.OldPassword)%>
                        <%: Html.ValidationMessageFor(m => m.OldPassword)%>
                    </div>

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

                </fieldset>
            </div>

            <div id="education">
                <fieldset>
                <legend>Education</legend>
                    <!-- Study programme -->
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.StudyProgramme)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.StudyProgramme)%>
                    </div>

                    <!-- Specialization -->
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Specialization)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.Specialization)%>
                    </div>

                    <!-- Title -->
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Title)%>
                    </div>
                    <div class="editor-field">                                               
                        <%: Html.TextBoxFor(model => model.Title, new { @readonly = "readonly" })%>
                        <%: Html.ValidationMessageFor(model => model.Title)%>
                        
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
                        <%: Html.LabelFor(model => model.TitleAfter)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.TitleAfter, new { @readonly = "readonly" })%>
                        <%: Html.ValidationMessageFor(model => model.TitleAfter)%>

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
                        <%: Html.LabelFor(model => model.ICQ)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.ICQ)%>
                        <%: Html.ValidationMessageFor(model => model.ICQ)%>
                    </div>

                    <!-- School email address -->
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.SchoolEmail)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.SchoolEmail)%>
                        <%: Html.ValidationMessageFor(model => model.SchoolEmail)%>
                    </div>

                    <!-- Phone -->
                    <div class="editor-label">
                        <%: Html.LabelFor(model => model.Phone)%>
                    </div>
                    <div class="editor-field">
                        <%: Html.TextBoxFor(model => model.Phone)%>
                        <%: Html.ValidationMessageFor(model => model.Phone)%>
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
                <p>&nbsp;<input type="submit" value="Edit profile" /></p>
        </div>
    <% } %>

</asp:Content>

