using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Common;
using Test.Enum;
using Test.IBLL;
using Test.Mongo.Model;

namespace Test.WebApp.Controllers
{
    public class UserController : Controller
    {
        #region IOC
        /// <summary>
        /// 用户业务层
        /// </summary>
        public IUserInfoBll UserService { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserInfoBll userService)
        {
            UserService = userService;
        }
        #endregion
       
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.PassWord))
            {
                User model = UserService.Find(u => u.Name == user.Name && u.PassWord == MD5encryption.UserMd5(user.PassWord)).FirstOrDefault<User>();

                if (model != null)
                {
                    Session["User"] = model;

                    if (model.Rank == UserRank.examinee)
                        return RedirectToAction("Test", "TestPaper");
                    else if (model.Rank == UserRank.admin)
                        return RedirectToAction("Index", "Manage");
                    else
                        return RedirectToAction("Test", "TestPaper");
                }
                else//数据库中不存在此账户
                    return Content("用户名或密码错误");
            }
            else
            {
                return Content("请填写完整");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.PassWord) && !string.IsNullOrEmpty(user.EMail))
            {
                if (UserService.Find(u => u.Name == user.Name).FirstOrDefault<User>() == null)//数据库中不存在同名账户
                {
                    user.Rank = UserRank.examinee;
                    user.PassWord = MD5encryption.UserMd5(user.PassWord);//对密码进行加密
                    UserService.InsertOne(user);

                    return RedirectToAction("Index", "TestPaper");
                }
                else
                    return Content("该用户名已被注册，请重新创建");
            }
            else
            {
                return Content("请填写完整");
            }
        }
    }
}