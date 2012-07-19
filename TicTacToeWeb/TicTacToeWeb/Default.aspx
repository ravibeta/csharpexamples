<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TicTacToeWeb._Default" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
        <asp:TableCell><asp:Button ID="_0" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_1" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_2" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell><asp:Button ID="_3" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_4" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_5" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell><asp:Button ID="_6" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_7" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_8" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
        <asp:TableCell><asp:Button ID="_9" runat="server" Text="Play" Height="59px" OnClick="Reset"/></asp:TableCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <br />
</asp:Content>
