using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Data.Entity;

namespace hitchbotAPI
{
    public partial class ApproveImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (var db = new Models.Database())
            {
                string userName = this.userName.Text;
                var user = db.Passwords.Include(p => p.hitchBOT).Include(p => p.Projects).FirstOrDefault(p => p.Username == userName);

                if (user == null)
                {
                    lblError.Text = "Username not found!";
                }
                else
                {
                    if (user.Hash == Models.PasswordHandler.GetHash(passWord.Text))
                    {
                        Session["New"] = user;
                        Response.Redirect("LandingPage.aspx");
                    }
                    else
                    {
                        lblError.Text = "Incorrect PW";
                    }
                }
            }
        }
    }
}