using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;

namespace hitchbotAPI.ApproveImagesPages
{
    public partial class ConversationTweets : System.Web.UI.Page
    {
        private static Random randy = new Random();
        private int hitchID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["New"] != null)
            {
                var user = (Models.Password)Session["New"];
                using (var db = new Models.Database())
                {
                    this.hitchID = user.hitchBOT.ID;

                    var conversations = db.SpeechEvents.Where(l => DbFunctions.DiffDays(l.OccuredTime, DateTime.UtcNow) <= 1).Where(l => !string.IsNullOrEmpty(l.SpeechSaid)).ToList();

                    for (int i = 0; i < 15; i++)
                    {
                        var convo = conversations[randy.Next(conversations.Count)];
                        TableRow row = new TableRow();
                        TableCell cell1 = new TableCell();
                        TableCell cell2 = new TableCell();

                        cell1.VerticalAlign = VerticalAlign.Middle;
                        cell2.VerticalAlign = VerticalAlign.Middle;

                        TextBox textBox = new TextBox();
                        textBox.Text = convo.SpeechSaid;
                        textBox.ID = "box" + i;
                        textBox.MaxLength = 140;
                        textBox.Width = 400;
                        textBox.TextMode = TextBoxMode.MultiLine;
                        textBox.Rows = 5;

                        var buttonTweet = new Button();
                        buttonTweet.CommandArgument = textBox.ID;
                        buttonTweet.Click += this.Button_Tweet_Text;
                        buttonTweet.Text = "Tweet";

                        cell1.Controls.Add(textBox);
                        cell2.Controls.Add(buttonTweet);
                        row.Controls.Add(cell1);
                        row.Controls.Add(cell2);
                        MainTable.Controls.Add(row);
                    }
                }

            }
            else
            {
                Response.Redirect("Unauthorized.aspx");
            }
        }

        private void Button_Tweet_Text(object sender, System.EventArgs e)
        {
            Button b = (Button)sender;

            string textContent = ((TextBox)FindControl(b.CommandArgument)).Text;

            Helpers.TwitterHelper.PostTweetText(this.hitchID, textContent);

            Response.Redirect("ConversationTweets.aspx", true);
        }
    }
}