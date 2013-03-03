<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<NerdRide.Models.FlairViewModel>" ContentType="application/x-javascript" %>
<%@ Import Namespace="NerdRide.Helpers" %>
<%@ Import Namespace="NerdRide.Models" %>
document.write('<script>var link = document.createElement(\"link\");link.href = \"http://nerdRide.com/content/Flair.css\";link.rel = \"stylesheet\";link.type = \"text/css\";var head = document.getElementsByTagName(\"head\")[0];head.appendChild(link);</script>');
document.write('<div id=\"nd-wrapper\"><h2 id=\"nd-header\">NerdRide.com</h2><div id=\"nd-outer\">');
<% if (Model.Rides.Count == 0) { %>
    document.write('<div id=\"nd-bummer\">Looks like there\'s no Nerd Rides near <%:Model.LocationName %> in the near future. Why not <a target=\"_blank\" href=\"http://www.nerdRide.com/Rides/Create\">host one</a>?</div>');
<% } else { %>
document.write('<h3>  Rides Near You</h3><ul>');
    <% foreach (var item in Model.Rides) { %>
document.write('<li><a target=\"_blank\" href=\"http://nerdride.cloudapp.net/<%: item.RideID %>\"><%: item.Title.Truncate(20) %> with <%: item.HostedBy %> on <%: item.EventDate.ToShortDateString() %></a></li>');
    <% } %>
document.write('</ul>');
<% } %>
document.write('<div id=\"nd-footer\">  More Rides and fun at <a target=\"_blank\" href=\"http://nerdride.cloudapp.net\">http://nerdride.cloudapp.net</a></div></div></div>');