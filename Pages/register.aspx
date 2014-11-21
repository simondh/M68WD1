<%@ Page Title="SPRLK" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="Pages_register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <h1>Scissors Paper Rock Lizard Spock</h1>

<asp:FormView ID="FormView1" runat="server"></asp:FormView>
        <table class="style1">
            <tr>
                <td>Username:</td>
                <td>
                    <asp:TextBox ID="userName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="User Name is required" ControlToValidate="userName" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Username must be Letters and Numbers, only one word" ValidationExpression="^\w+$" ControlToValidate="userName" Display="Dynamic"></asp:RegularExpressionValidator>

                </td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>
                    <asp:TextBox ID="password" TextMode="Password" runat="server"></asp:TextBox>
                </td>
            </tr>
           <tr>
                <td>Confirm Password:</td>
                <td>
                    <asp:TextBox ID="confirmPassword" TextMode="Password"  runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="password" ControlToValidate="confirmPassword" Display="Dynamic" ErrorMessage="Passwords do not match"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="confirmPassword" Display="Dynamic" ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td>Full Name:</td>
                <td>
                    <asp:TextBox ID="fullName" Text='<%#Bind("fullName") %>'   runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>email:</td>
                <td>
                    <asp:TextBox ID="email" TextMode="Email" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Date of Birth:</td>
                <td>
                    <asp:TextBox ID="DOB" TextMode="Date" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="A DOB is required"
                         Display="Dynamic" ControlToValidate="DOB"></asp:RequiredFieldValidator>
                    <asp:CustomValidator runat="server"
                                 ID="valDateRange" 
                                 ControlToValidate="DOB"
                                 onservervalidate="valDateRange_ServerValidate" 
                                 ErrorMessage="Must be at least 18 years old" Display="Dynamic"></asp:CustomValidator>
                </td>
            </tr>


            </table>
    <asp:Label ID="notificationLabel" runat="server" Text=""></asp:Label><br />
    <asp:Button ID="saveRegistration" runat="server" Text="Register" OnClick="registerSaveButton_click" />

        <asp:Button ID="cancel" runat="server" Text="Cancel" OnClick="cancel_Click" CausesValidation="False" />

</asp:Content>

