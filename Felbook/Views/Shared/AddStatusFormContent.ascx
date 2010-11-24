<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>

<%= Html.AntiForgeryToken() %>
<%= Html.ValidationSummary("There were some errors:") %>
<p>
	<label for="statusText">Status text:</label><br />
	<textarea name="Status" id="statusText" rows="4" cols="20"></textarea>
</p>
<p>
	File</p>
<p>
	<input type="file" name="Files[0]" />
	<input type="text" name="FileDescriptions[0]"></p>
<p>
	Pictures</p>
<p>
	<input type="file" name="Images[0]" />
	<input type="text" name="ImageDescriptions[0]">
</p>
<p>
	Links:</p>
<p>
	Link:
	<input type="text" name="Links[0]" />
	<input type="text" name="LinkDescriptions[0]" /></p>
<p>
	<input type="submit" value="Add new status" /></p>

