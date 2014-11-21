using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Windows;
using System.Windows.Forms;
using System.Data;

public partial class Pages_play : System.Web.UI.Page
{
    Random rnd = new Random();

    protected void Page_Load(object sender, EventArgs e)
    {
        DataRow theUser  = getSessionUserData();
        nameInTitle.Text = theUser["username"].ToString();
        checkBday(theUser);
        resetGame();
        updateResultsTable(theUser);

    }

    void checkBday (DataRow theUser)
    {
        // If its the users birthday, show happy birthday message
        DateTime now = DateTime.Now;
        DateTime userBday = (DateTime)theUser["DOB"];
        if ((now.Month == userBday.Month) && (now.Day == userBday.Day))
        {
           happy.Text = "Happy Birthday ! " + theUser["username"].ToString();
        }
    }


    DataRow getSessionUserData ()
    {
        // get the user data from the session
        DataTable theUser = null;

        if (Session["theUser"] != null)
        {
            theUser = (DataTable)Session["theUser"];
            if (theUser.Rows.Count > 0)
                return theUser.Rows[0];
        }
        
            // should not get here - has been a problem in debugging only
            Response.Redirect("default.aspx");
            // MessageBox.Show("should not be in this page at all");
        return null; 
    }

    protected void logoff_Click(object sender, EventArgs e)
    {
        Session["theUser"] = null;
        Response.Redirect("Default.aspx");
    }

    int userWonCalc (int A, int B)
    {
        // returns 1 if A beats B, -1 if A loses to B, 0 if its a tie
        // A and B are [1..5] corrseponding to paper lizard rock scissors spock
        int r;
        int[,] decisionTable = new int[5,5] {   {0, -1, 1, -1, 1 }, 
                                                {1, 0, -1, -1, 1 }, 
                                                {-1, 1, 0, 1, -1 }, 
                                                {1, 1, -1, 0, -1 }, 
                                                {-1, -1, 1, 1, 0 }};

        r = decisionTable[A-1, B-1];
        return r;
    } // userWonCalc

    protected void userSelected (object sender, EventArgs e)
    {
        
        
        int userChoice = Convert.ToInt32(((System.Web.UI.WebControls.ImageButton)sender).CommandArgument);
        //MessageBox.Show("User selected : " + userChoice);
        resetGame();
        int computerChoice = (int)Session["computerChoice"];
        ImageButton imgButton = (ImageButton)FindControlRecursive(Table1, "gameChoice_" + userChoice);
        imgButton.CssClass = "buttonBright";
        imgButton = (ImageButton)FindControlRecursive(Table2, "aspChoice_" + computerChoice);
        imgButton.CssClass = "buttonBright";

        int userWon = userWonCalc(userChoice, computerChoice);

        // Update the session user object with this game
        updateGameStats(userWon, userChoice);
        
        if (userWon == 1)
        {
            winLoseCell.Text = "You WON!";
        }
        else if (userWon == -1)
        {
            winLoseCell.Text = "You LOSE!";
        }
        else
        {
            winLoseCell.Text = "Its a TIE!";
        }

    }

void updateGameStats(int userWon, int userChoice)
{
    DataRow theUser = getSessionUserData();
    int i;
    i = (int)theUser["played"] + 1;
    theUser["played"] = i;
    if (userWon == 1) {
        theUser["won"] = (int)theUser["won"] + 1;
    }

    switch (userChoice)
    {
        case 1:  // paper
            theUser["paper"] = (int)theUser["paper"] + 1;
            break;
        case 2:  // lizard
            theUser["lizard"] = (int)theUser["lizard"] + 1;
            break;
        case 3:  // rock
            theUser["rock"] = (int)theUser["rock"] + 1;
            break;
        case 4:  // scissors
            theUser["scissors"] = (int)theUser["scissors"] + 1;
            break;
        case 5:  // spock
            theUser["spock"] = (int)theUser["spock"] + 1;
            break;
    }

    // update the database with this new data
    updateUserData(theUser);
    updateResultsTable (theUser);
}


protected void updateUserData(DataRow theUser)
{
    SqlCommand comm = new SqlCommand();
    string connectionString = Session["connectionString"].ToString();


    try
    {
        using (SqlConnection cnn = new SqlConnection(connectionString))
        {
            try
            {
                cnn.Open();
                comm.Connection = cnn;
                // first make sure username is  unique
             
                    try
                    {
                        // username is unique, save it
                        SqlCommand addCommand = new SqlCommand();
                        addCommand.Connection = cnn;
                        addCommand.CommandText = "UPDATE usrs SET played=@played, won=@won, rock=@rock, scissors=@scissors, " +
                                            "paper=@paper, lizard=@lizard, spock=@spock, dateLastPlayed=@timeNow " +
                                            "WHERE username=@uname";

                        addCommand.Parameters.AddWithValue("@played", theUser["played"]);
                        addCommand.Parameters.AddWithValue("@won", theUser["won"]);
                        addCommand.Parameters.AddWithValue("@rock", theUser["rock"]);
                        addCommand.Parameters.AddWithValue("@scissors", theUser["scissors"]);
                        addCommand.Parameters.AddWithValue("@paper", theUser["paper"]);
                        addCommand.Parameters.AddWithValue("@lizard", theUser["lizard"]);
                        addCommand.Parameters.AddWithValue("@spock", theUser["spock"]);
                        addCommand.Parameters.AddWithValue("@timeNow", DateTime.Now);
                        addCommand.Parameters.AddWithValue("@uname", theUser["username"]);

                        addCommand.CommandType = CommandType.Text;
                        addCommand.ExecuteNonQuery();
                        cnn.Close();

                        // And reload session data
                        SqlDataReader theUsers;

                        cnn.Open();
                        comm.Connection = cnn;
                        comm.CommandText = "SELECT * FROM usrs WHERE  username=@username"; 
                        comm.Parameters.AddWithValue("@username", theUser["username"]);
                        theUsers = comm.ExecuteReader();
                        if (theUsers.HasRows)
                        {
                            //theUsers.Read();
                            DataTable theUser2 = new DataTable(); // use DataTable in session variable to keep the data in other pages.
                            theUser2.Load(theUsers);
                            Session["theUser"] = theUser2;
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Save of new player FAILED : " + ex);
                    }
                }
            
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection or read database " + ex);
            }
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("SqlConnection error, cannot connect to database: " + ex);
    }

} // updateUserData


void updateResultsTable (DataRow theUser)
{
    rt1.Text = theUser["played"].ToString();
    rt2.Text = theUser["won"].ToString();
    rt3.Text = theUser["paper"].ToString();
    rt4.Text = theUser["rock"].ToString();
    rt5.Text = theUser["scissors"].ToString();
    rt6.Text = theUser["lizard"].ToString();
    rt7.Text = theUser["spock"].ToString();

}

    protected void playClick(object sender, EventArgs e)
    {
        // Player has clicked 'Play!', so we will:
        // 1) Computer makes choice (but keeps it secret for now)
        // 2) Enable the game choice imagebuttons
        // 3) Thats it - wait for user to click one, then another onClick routine takes over

         resetGame(); 

        int computerChoice = rnd.Next(1, 6);
        Session["ComputerChoice"] = computerChoice;  // save for next form put
 
        countdownCell.Text = "CHOOSE!";
        
        for (int i = 1; i <= 5; i++) {
            // enable all the buttons and make them bright
            ImageButton c = (ImageButton) FindControlRecursive(Table1, "gameChoice_" + i);
            c.Enabled = true;
            c.CssClass = "buttonBright";
        }
    
        // thats it until the user clicks one of the selections
    }

 


    void resetGame()
    {
        for (int i = 1; i <= 5; i++)
        {
            ImageButton c = (ImageButton)FindControlRecursive(Table1, "gameChoice_" + i);
            c.Enabled = false;
            c.CssClass = "buttonDim";
            c = (ImageButton)FindControlRecursive(Table2, "aspChoice_" + i);
            c.CssClass = "buttonDim";
        }

        countdownCell.Text = "";
        winLoseCell.Text = "";

    }

    private System.Web.UI.Control FindControlRecursive(System.Web.UI.Control root, string id)
    {
        // thanks StackOverflow !
        return root.ID == id
                   ? root
                   : (root.Controls.Cast<System.Web.UI.Control>()
                         .Select(c => FindControlRecursive(c, id)))
                         .FirstOrDefault(t => t != null);
    }

}