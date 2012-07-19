<%@ Page Title="Tic Tac Toe" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TicTacToeWeb._Default" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow>
        <asp:TableCell></asp:TableCell>
        <asp:TableCell><asp:Label runat="server" Text="Tic Tac Toe" Height="39px" Width="69px" Font-Size="Small"></asp:Label></asp:TableCell>
        <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell><asp:Button ID="_0" Width="69px" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_1" Width="69px" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_2" Width="69px" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell><asp:Button ID="_3" Width="69px" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_4" Width="69px" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_5" Width="69px" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
        <asp:TableCell><asp:Button ID="_6" Width="69px" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_7" Width="69px" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        <asp:TableCell><asp:Button ID="_8" Width="69px" runat="server" Text="click me" Height="59px" OnClick="Play"/></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
        <asp:TableCell><asp:Button ID="_9" Width="69px" runat="server" Text="Play" Height="59px" OnClick="Reset" Enabled="false"/></asp:TableCell>
            <asp:TableCell></asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <br />
</asp:Content>
