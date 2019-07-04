using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace Web
{
    /// <summary>
    /// userAccountProcessing 的摘要说明
    /// </summary>
    public class userAccountProcessing : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 用户业务层对象
        /// </summary>
        //BLL.User user = new BLL.User();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            switch (context.Request["play"])
            {
                //检测 【用户名】 是否存在
                case "0":
                    //if (user.Exists(context.Request["name"]))
                    //    context.Response.Write("1");
                    //else
                    //    context.Response.Write("0");
                    //【存在】 输出【1】 【不存在】输出【0】
                    break;

                //检测 【用户名密码组合】 是否存在
                case "1":
                    //用户密码【加密】
                    //string pwd = Common.MD5encryption.UserMd5(context.Request["pwd"]);

                    //Model.User _user = user.GetModel(context.Request["name"], pwd);
                    //if (_user != null)
                    //{
                    //    //存储用户【登录状态】
                    //    context.Session["user"] = _user;

                    //    //【记忆】用户【账号密码组合】
                    //    if (context.Request["whetherSave"] == "1")
                    //    {
                    //        context.Response.Cookies["ui"].Value = context.Request["name"];
                    //        context.Response.Cookies["up"].Value = pwd;

                    //        context.Response.Cookies["ui"].Expires = DateTime.Now.AddDays(7);
                    //        context.Response.Cookies["up"].Expires = DateTime.Now.AddDays(7);
                    //    }
                    //    break;
                    //    context.Response.Write("1");
                    //}
                    //else
                    //    context.Response.Write("0");
                    break;

                //【注册账号】
                case "2":
                    //Model.User model = new Model.User()
                    //{
                    //    name = context.Request["name"],
                    //    passWord = Common.MD5encryption.UserMd5(context.Request["pwd"]),
                    //    question = context.Request["question"],
                    //    answer = context.Request["answer"],
                    //    hint = context.Request["hint"],
                    //    rank = Enum.User.normal
                    //};
                    //if (user.Add(model))
                    //{
                    //    //存储用户【登录状态】
                    //    context.Session["user"] = model;

                    //    context.Response.Write("1");    //注册成功
                    //}
                    //else
                    //    context.Response.Write("0");    //注册失败
                    break;

                //【注销账号】
                case "3":
                    context.Session["user"] = null;      //用户
                    context.Response.Cookies["ui"].Expires = DateTime.Now.AddDays(-1);  //用户名
                    context.Response.Cookies["up"].Expires = DateTime.Now.AddDays(-1);  //密码

                    context.Response.Cookies["sc"].Expires = DateTime.Now.AddDays(-1);  //删购物车
                    break;
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}