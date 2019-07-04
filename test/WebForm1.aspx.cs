using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace test
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ajax panel刷新用户状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UP_userImageAndText_PreRender(object sender, EventArgs e)
        {
            //if (Session["user"] != null)
            //{
            //    Model.User model_user = (Model.User)Session["user"];
            //    userText.Attributes["uh"] = ((int)model_user.rank).ToString();
            //    userText.Text = model_user.name;
            //    userHeadImage.Visible = true;

            //    Response.Cookies["ui"].Value = model_user.id.ToString();
            //    Response.Cookies["up"].Value = model_user.passWord;
            //    Response.Cookies["ui"].Expires = DateTime.Now.AddDays(7);
            //    Response.Cookies["up"].Expires = DateTime.Now.AddDays(7);
            //}
        }
    }
}