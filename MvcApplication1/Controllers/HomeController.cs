﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MvcApplication1.Models;
namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Login()
        {
            if (Session["username"] == null)
                return View("Login");
            else
                return RedirectToAction("HomePage");
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult StartUp()
        {
            return View();
        }
        public ActionResult Loading()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session["username"] = null;
            return View("Login");
        }
        public ActionResult Messages()
        {
            return View("Messages");
        }
        public ActionResult Settings()
        {
            return View("Settings");
        }
        public ActionResult Notifications()
        {
            return View("Notifications");
        }


        public ActionResult authenticate(String username, String password)
        {
            int result = CRUD.Login(username, password);

            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Login", (object)data);
            }
            else if (result == 0)
            {

                String data = "Incorrect Credentials";
                return View("Login", (object)data);
            }
            Session["username"] = username;

            return RedirectToAction("HomePage");
            //return RedirectToAction("HomePage", new { username = username });
        }

        public ActionResult HomePage(String username)
        {
            if (Session["username"] == null)
                return View("login");
            else
            {
                User users = CRUD.view_user(Session["username"].ToString());
                List<User> People_U_Should_Follow_list = CRUD.People_U_Should_Follow(Session["username"].ToString());
                List<hashtag_trending> trendingHashtags = CRUD.trending_hashtag();

                dynamic model = new ExpandoObject();
                model.User = users;
                model.Suggested_people = People_U_Should_Follow_list;
                model.trending_hashtags = trendingHashtags;

                return View(model);
            }
        }

        public ActionResult ProfilePage(String username)
        {
            if (Session["username"] == null)
                return View("login");
            else
            {
                User users = CRUD.view_user(Session["username"].ToString());
                if (users == null)
                {
                    RedirectToAction("Login");
                }
                Console.Write(users);
                return View(users);
            }
        }

        public ActionResult Add_User(String username, String password, String first_name, String last_name, String email, String country, String gender/*,String BirthDay, String BirthMonth, String BirthYear*/ )
        {
            int result = CRUD.SignUp(username, password, first_name, last_name, email, country, gender);
            if (result == -1)
            {
                String data = "Something went wrong while connecting with the database.";
                return View("Login", (object)data);
            }
            else if (result == 0)
            {
                String data = "This Username is already in use";
                return View("SignUp", (object)data);
            }
            Session["username"] = username;
            return RedirectToAction("HomePage");
            //return RedirectToAction("HomePage", new { username = username });
        }
    }
}
