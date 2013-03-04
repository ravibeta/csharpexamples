<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<System.String>" %>
<%: Html.DropDownList("", new SelectList(NerdRide.Helpers.PhoneValidator.Countries, Model)) %>