<%@ Page Title="SPRLK" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

    <h1>Scissors Paper Rock Lizard Spock</h1>

    <asp:Panel ID="LoginPanel" runat="server">
        <div class="LoginPanel">
        Existing user? - Log in here:
        <br />
            <asp:Label ID="Label1"  CssClass ="loginLabel" runat ="server" Text="Label">User Name : </asp:Label>
            <asp:TextBox CssClass="loginInput" ID="userName" runat="server" Columns="20" MaxLength="20" TabIndex="1"></asp:TextBox>
            <asp:RequiredFieldValidator CssClass="loginInput"  ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="userName" ErrorMessage="User Name is required" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <br />
            <asp:Label ID="Label2" CssClass="loginLabel" runat="server" Text="fred">Password : </asp:Label>
            <asp:TextBox  CssClass="loginInput" ID="password"  TextMode="Password" runat="server" Columns="20" MaxLength="20" TabIndex="2"></asp:TextBox>
            <asp:RequiredFieldValidator  CssClass="loginInput" ID="RequiredFieldValidator2" runat="server" ControlToValidate="password" ErrorMessage="Password is required"></asp:RequiredFieldValidator>
        <br /><span><br /><asp:Button ID="loginButton"  CssClass ="loginButton" runat="server" Text="Login" OnClick="loginButton_click" AccessKey="L" TabIndex="3" />
        <asp:Button ID="registerButton" CssClass ="loginButton" runat="server" Text="Register" OnClick="registerButton_click" CausesValidation="False" AccessKey="R" TabIndex="4" /></span>
        <br /><asp:Label ID="resultLabel" runat="server" Text=""></asp:Label>
        </div>

        
        
    </asp:Panel>

<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [Id], [username], [password], [fullName], [email], [DOB] FROM [usrs]"></asp:SqlDataSource>
</asp:Content>

