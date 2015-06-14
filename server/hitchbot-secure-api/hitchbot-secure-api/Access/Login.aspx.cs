using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hitchbot_secure_api.Access
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            using (var db = new Dal.DatabaseContext())
            {
                string userName = this.userName.Text;
                var user = db.LoginAccounts.FirstOrDefault(l => l.Username == userName);

                if (user == null)
                {
                    //lblError.Text = "Username not found!";
                    this.errorAlert.Attributes.Remove("class");
                    this.errorAlert.Attributes.Add("class", "alert alert-danger");
                    this.errorAlert.InnerText = "Username not found!";
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(user.PasswordHash))
                    {
                        var newCred = Helpers.AccountHelper.GetNewHashAndSalt(passWord.Text);
                        user.PasswordHash = newCred.Hash;
                        user.Salt = newCred.Salt;

                        db.SaveChanges();
                    }
                    else if (!Helpers.AccountHelper.VerifyPassword(passWord.Text, user.Salt, user.PasswordHash))
                    {
                        //lblError.Text = "Incorrect PW";
                        this.errorAlert.Attributes.Remove("class");
                        this.errorAlert.Attributes.Add("class", "alert alert-danger");
                        this.errorAlert.InnerText = "Incorrect PW";
                        return;
                    }

                    Session["New"] = user;
                    Response.Redirect("LandingPage.aspx");
                }
            }
        }
    }
}