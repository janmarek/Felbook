<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<fieldset>

<legend>Add status</legend>

<%= Html.AntiForgeryToken() %>
<%= Html.ValidationSummary("There were some errors:") %>
<p>
	<label for="statusText">Status text:</label><br />
	<textarea name="Status" id="statusText" rows="4" cols="20"></textarea>
</p>

<p>
	<a href="" id="status-pictures-btn">Add picture</a> | 
	<a href="" id="status-links-btn">Add link</a> | 
	<a href="" id="status-files-btn">Add file</a>
</p>

<p><input type="submit" value="Add new status" /></p>

<div id="status-pictures-cont">
<h4>Pictures</h4>
<div id="status-pictures"></div>
</div>

<div id="status-links-cont">
<h4>Links</h4>
<div id="status-links"></div>
</div>

<div id="status-files-cont">
<h4>Files</h4>
<div id="status-files"></div>
</div>

<!--
<p>
	<input type="file" name="Files[0]" />
	<input type="text" name="FileDescriptions[0]"/></p>
<p>
	Pictures</p>
<p>
	<input type="file" name="Images[0]" />
	<input type="text" name="ImageDescriptions[0]"/>
</p>
<p>
	Links:</p>
<p>
	Link:
	<input type="text" name="Links[0]" />
	<input type="text" name="LinkDescriptions[0]" /></p>

-->

</fieldset>
