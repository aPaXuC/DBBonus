﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class aspx_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        KGManager.log("Login button was clicked");
        if (tbLogin.Text.Equals("Admin") && tbPassword.Text.Equals("Admin"))
        {
            KGManager.userLogin = "Admin";
            Response.Redirect("/Default.aspx");
            Context.ApplicationInstance.CompleteRequest();
        }
        else
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("parentID", tbLogin.Text);
            //check if ID exists in parents table
            System.Data.DataTable dt = DBConnection.runProcWithResults("checkIfParentExists", data);
            if (dt.Rows.Count == 1)
            {
                //ID in parents table
                litLogin.Text = "";
                data.Add("parentPass", tbPassword.Text);
                //check if entered correct password
                dt = DBConnection.runProcWithResults("checkLoginData", data);
                if (dt.Rows.Count == 1)
                {
                    //password is correct
                    litPass.Text = "";
                    KGManager.userLogin = tbLogin.Text;
                    Response.Redirect("/Default.aspx");
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    //incorect password
                    litPass.Text = "Wrong password";
                }
            }
            else
            {
                //ID not in parents table
                litLogin.Text = "Please register first!";
            }
        }
    }
}