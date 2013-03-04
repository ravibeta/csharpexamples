<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
		<div id="hm-masthead">
		<%if ((bool)Model == true) { //Show Search Box %>
	    <div id="searchBox">
        <div class="search-text">Enter your location  or <strong><%: Html.ActionLink("View All Upcoming Rides", "Index", "Rides")%></strong>.</div>
					<%: Html.TextBox("Location")%>
					<input id="search" type="submit" value="Search" />
	    </div>
	    <% } %>
		</div>

