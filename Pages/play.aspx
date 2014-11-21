<%@ Page Title="SPRLK" Language="C#" MasterPageFile="~/Pages/MasterPage.master" AutoEventWireup="true" CodeFile="play.aspx.cs" Inherits="Pages_play" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Scissors Paper Rock Lizard Spock</h1>
    <h2>Ready to Play? <asp:Label ID="nameInTitle" runat="server" Text=""></asp:Label></h2>

    <div id="table_container">

    <asp:Table ID="Table1" runat="server" CssClass="pickTable">
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="gameChoice_1" runat="server" onClick="userSelected" 
                    ImageUrl="~/Resources/hand.png" AlternateText="Hand" CssClass="buttonDim" Enabled="False" CommandArgument="1" />
            </asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="gameChoice_2" runat="server" onClick="userSelected" 
                    ImageUrl="~/Resources/lizard.png" AlternateText="Lizard" CssClass="buttonDim" Enabled="False" CommandArgument="2" />
            </asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="gameChoice_3" runat="server" onClick="userSelected" 
                    ImageUrl="~/Resources/rock.png" AlternateText="Rock" CssClass="buttonDim" Enabled="False" CommandArgument="3" />
            </asp:TableCell>
        </asp:TableRow>       
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="gameChoice_4" runat="server" onClick="userSelected" 
                    ImageUrl="~/Resources/scissors.png" AlternateText="Scissors" CssClass="buttonDim" Enabled="False" CommandArgument="4" />
            </asp:TableCell>
        </asp:TableRow>       
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="gameChoice_5" runat="server" onClick="userSelected"
                     ImageUrl="~/Resources/spock.png" AlternateText="Spock" CssClass="buttonDim" Enabled="False" CommandArgument="5" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Table ID="actionTable" CssClass="pickTable" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="playButton" runat="server" ImageUrl="~/Resources/play.jpg" onClick="playClick" /></asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow>
            <asp:TableCell ID ="countdownCell" CssClass="countdownClass"></asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow>
            <asp:TableCell ID="winLoseCell" CssClass="happyBirthday"></asp:TableCell>
        </asp:TableRow>       
        <asp:TableRow>
            <asp:TableCell ID="happy" CssClass="happyBirthday"></asp:TableCell>
        </asp:TableRow>              
   
    </asp:Table>


     <asp:Table ID="Table2" CssClass="pickTable" runat="server">
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="aspChoice_1" runat="server" ImageUrl="~/Resources/hand.png" CssClass="buttonDim" Enabled="False" />
            </asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="aspChoice_2" runat="server" ImageUrl="~/Resources/lizard.png" CssClass="buttonDim" Enabled="False" />

            </asp:TableCell>
        </asp:TableRow>        
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="aspChoice_3" runat="server" ImageUrl="~/Resources/rock.png" CssClass="buttonDim" Enabled="False"/>

            </asp:TableCell>
        </asp:TableRow>       
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="aspChoice_4" runat="server" ImageUrl="~/Resources/scissors.png" CssClass="buttonDim" Enabled="False"/>

            </asp:TableCell>
        </asp:TableRow>       
        <asp:TableRow>
            <asp:TableCell>
                <asp:ImageButton ID="aspChoice_5" runat="server" ImageUrl="~/Resources/spock.png" CssClass="buttonDim" Enabled="False"/>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

        <div class="instructions">
            <img src="../Resources/Theory.png" />
        </div>

    </div>

    <div class="resultsTable">
        <asp:Table ID="resultsTable" runat="server" CssClass="resultsTable">
            <asp:TableRow>
                <asp:TableHeaderCell>Played</asp:TableHeaderCell>
                <asp:TableHeaderCell>Won</asp:TableHeaderCell>
                <asp:TableHeaderCell>Paper</asp:TableHeaderCell> 
                <asp:TableHeaderCell>Rock</asp:TableHeaderCell>
                <asp:TableHeaderCell>Scissors</asp:TableHeaderCell>
                <asp:TableHeaderCell>Lizard</asp:TableHeaderCell>
                <asp:TableHeaderCell>Spock</asp:TableHeaderCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell ID="rt1"></asp:TableCell>
                <asp:TableCell ID="rt2"></asp:TableCell>
                <asp:TableCell ID="rt3"></asp:TableCell>
                <asp:TableCell ID="rt4"></asp:TableCell>
                <asp:TableCell ID="rt5"></asp:TableCell>
                <asp:TableCell ID="rt6"></asp:TableCell>
                <asp:TableCell ID="rt7"></asp:TableCell>

            </asp:TableRow>
        </asp:Table>
        <br /><asp:Button ID="logoff" runat="server" Text="Log Off" BorderStyle="Dotted" OnClick="logoff_Click" CssClass="loginButton" />

    </div>



</asp:Content>

