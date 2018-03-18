using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using testSessionUsage.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace testSessionUsage.Controllers
{
    public class FirstController : Controller
    {
        OurFirstDatabaseContext ORM = null;
        public FirstController(OurFirstDatabaseContext _ORM)
        {
            ORM = _ORM;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateStudent()
        {
            UserDetail U = null;
            try
            {
                U = JsonConvert.DeserializeObject<UserDetail>(HttpContext.Session.GetString("LoggedInUser"));
                if(U ==null)
                {
                    return RedirectToAction(nameof(FirstController.Login));
                }
            }
            catch
            {
                return RedirectToAction(nameof(FirstController.Login));
            }
            

            ViewBag.Username = U.Username;

            return View();
        }

        [HttpPost]
        public IActionResult CreateStudent(Student S)
        {
            ORM.Student.Add(S);
            ORM.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateUser(UserDetail oUser)
        {
            ORM.UserDetail.Add(oUser);
            ORM.SaveChanges();

            ViewBag.Message = "User added successfully!";

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserDetail oUser)
        {
           UserDetail FoundUserInDB =  ORM.UserDetail.Where(abc => abc.Username == oUser.Username && abc.Password == oUser.Password).FirstOrDefault<UserDetail>();

            if(FoundUserInDB == null)
            {
                ViewBag.Message = "Invalid details, please try again";
                return View();
            }

          string SerializedFoundUserInDB  = JsonConvert.SerializeObject(FoundUserInDB);
            // HttpContext.Session.("LoggedInUser", SerializedFoundUserInDB);

            HttpContext.Session.SetString("LoggedInUser", SerializedFoundUserInDB);
   

            return RedirectToAction(nameof(FirstController.CreateStudent));
        }


        public IActionResult AllStudents()
        {
            UserDetail U = null;
            try
            {
                U = JsonConvert.DeserializeObject<UserDetail>(HttpContext.Session.GetString("LoggedInUser"));
                if (U == null)
                {
                    return RedirectToAction(nameof(FirstController.Login));
                }
            }
            catch
            {
                return RedirectToAction(nameof(FirstController.Login));
            }

            ViewBag.UserRole = U.Role;
            return View(ORM.Student.ToList<Student>());
        }

        public IActionResult Logout()
        {
           // HttpContext.Session.Remove("a");


            HttpContext.Session.Clear();

            return RedirectToAction(nameof(FirstController.Login));
        }

    }
}