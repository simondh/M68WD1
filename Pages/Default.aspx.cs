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



public partial class Pages_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Only save this data on 1st use
        if (Session["firstTime"] == null)
        {
          //  Session["connectionString"] = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\simon\OneDrive\MSc\M88WebDev\SPRLK1\App_Data\Database.mdf; Integrated Security=True; Connect Timeout=30";
            Session["connectionString"] = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=\App_Data\Database.mdf; Integrated Security=True; Connect Timeout=30";
            Session["firstTime"] = "OK";
            registerButton.PostBackUrl = "Pages/register.aspx";
        }
    }


    protected void loginButton_click(object sender, EventArgs e)
    {
        SqlDataReader theUsers;
        SqlCommand comm = new SqlCommand();
        string connectionString = Session["connectionString"].ToString();
        bool allGood = false;

        try
        {
           
            using (SqlConnection cnn = new SqlConnection(connectionString))
            {
                try
                {
                    cnn.Open();
                    comm.Connection = cnn;
                    comm.CommandText = "SELECT * FROM usrs WHERE  username=@username and password=@password ";
                    comm.Parameters.AddWithValue("@username", userName.Text);
                    comm.Parameters.AddWithValue("@password", password.Text);
                    theUsers = comm.ExecuteReader();  // read the database for users with these parameters (username and password)
                    if (theUsers.HasRows)
                    {
                        // the user exists with that name and password
                        // there can only be one row as username is checked for unique in register.aspx.cs
                        DataTable theUser = new DataTable(); // use DataTable in session variable to keep the data in other pages.
                        theUser.Load(theUsers);
                        Session["theUser"] = theUser;
                        allGood = true;
                    }
                    else
                    {
                        resultLabel.Text = "Invalid username / password";
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
            Response.Redirect("play.aspx"); // OK lets go play the game
    }

    protected void registerButton_click(object sender, EventArgs e)
    {
        Response.Redirect("register.aspx");
    }


} // Pages_Default


