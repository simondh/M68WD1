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

public partial class Pages_register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }




    protected void registerSaveButton_click(object sender, EventArgs e)
    {
        // user clicked to register
        SqlCommand comm = new SqlCommand();
        string connectionString = Session["connectionString"].ToString();
        bool allGood = false;

        if (!Page.IsValid)
        {
            return;  // make sure all validation has succeeded, bomb out if not
        }

        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();  // open database
                    comm.Connection = cnn;
                    // first make sure username is  unique
                   
                    comm.CommandText = "SELECT * FROM usrs WHERE  username=@username";
                    comm.Parameters.AddWithValue("@username", userName.Text);
                    SqlDataReader anyUser = comm.ExecuteReader();
                    
                    if (anyUser.HasRows)
                    {
                        // this user already exists
                        notificationLabel.Text = "this userName is already taken, please chose another";
                        userName.Focus();
                        cnn.Close();
                    }
                    else
                    {
                        cnn.Close();
                        try
                        {
                            // username is unique, save it
                            SqlCommand addCommand = new SqlCommand();
                            cnn.Open();
                            addCommand.Connection = cnn;
                            addCommand.CommandText = "INSERT INTO usrs (username, password, fullName, email, DOB, dateJoined) VALUES (@username, @password, @fullName, @email, @DOB, @dateJoined)";
                            addCommand.Parameters.AddWithValue("@username", userName.Text);
                            addCommand.Parameters.AddWithValue("@password", password.Text);
                            addCommand.Parameters.AddWithValue("@fullName", fullName.Text);
                            addCommand.Parameters.AddWithValue("@email", email.Text);
                            addCommand.Parameters.AddWithValue("@DOB", DOB.Text);
                            addCommand.Parameters.AddWithValue("@dateJoined", DateTime.Now);
                            addCommand.CommandType = CommandType.Text;
                            addCommand.ExecuteNonQuery();
                            cnn.Close();
                            notificationLabel.Text = "SAVED! ";
                            allGood = true;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Save of new player FAILED : " + ex);
                        }
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

        if (allGood)
        {
            loadUserIntoSession(userName.Text);
            Response.Redirect("/Pages/play.aspx"); // all good, go play
        }

    } // registerSaveButton_click



    protected void cancel_Click(object sender, EventArgs e)
    {
        // cancel - go back to default, the login screen
        Session["theUser"] = null;  // erase current user data in session
        Response.Redirect("/Default.aspx");
    }

    protected void valDateRange_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // check a valid DOB has been entered and player is over 18
        DateTime minDate = DateTime.Now.AddYears(-18);
        DateTime dt;
        args.IsValid = false;
        if (DateTime.TryParse(args.Value, out dt))
        {
            args.IsValid = (dt <= minDate);
        }   
    } // valDateRange_ServerValidate

    
    private void loadUserIntoSession(string newUsername)
    {
        SqlDataReader theUsers;
        SqlCommand comm = new SqlCommand();
        string connectionString = Session["connectionString"].ToString();

        try
        {
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                comm.Connection = cnn;
                comm.CommandText = "SELECT * FROM usrs WHERE  username=@username"; // we have already checked usernames are unique, so only 1 row will be returned
                comm.Parameters.AddWithValue("@username", userName.Text);
                theUsers = comm.ExecuteReader();
                if (theUsers.HasRows)
                {
                    theUsers.Read();
                    DataTable theUser = new DataTable(); // use DataTable in session variable to keep the data in other pages.
                    theUser.Load(theUsers);
                    Session["theUser"] = theUser;
                }
                else
                {
                    MessageBox.Show("loadUserIntoSession Internal error, cannot read newly registered user");
                }
                cnn.Close();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("loadUserIntoSession error : " + ex);
        }
     
    } // loadUserIntoSession
    



}