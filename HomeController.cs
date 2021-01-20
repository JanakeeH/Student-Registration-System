using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Spwin.Models;


namespace Spwin.Controllers
{
    public class HomeController : Controller
    {
        
        SpwinEntities1 SpwinEntities1 = new SpwinEntities1();

        // GET: Home
        public ActionResult Index()
        {
            return View(SpwinEntities1.Students.ToList());
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(Student student)
        {
            if(SpwinEntities1.Students.Any(x=>x.UserName == student.UserName))
            {
                ViewBag.Notification = "This account has already Exsits!";
                return View();
            }
            else
            {
                SpwinEntities1.Students.Add(student);
                SpwinEntities1.SaveChanges();

                Session["UserNameSS"] = student.UserName.ToString();
                Session["BirthdaySS"] = student.Birthday.ToString();
                Session["RegDaySS"] = student.RegDay.ToString();
                Session["StreamSS"] = student.Stream.ToString();
                return RedirectToAction("Index", "Home");

            }

            Session["StuIDSS"] = student.StuID.ToString();
            Session["PasswordSS"] = student.Password.ToString();
            Session["RePasswordSS"] = student.RePassword.ToString();

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Student student)
        {
           var checklogin = SpwinEntities1.Students.Where(x=>x.UserName.Equals(student.UserName) && x.Password.Equals(student.Password)).FirstOrDefault();
           if(checklogin !=null)
           {

                Session["StuIDSS"] = student.StuID.ToString();
                Session["UserNameSS"] = student.UserName.ToString();
                return RedirectToAction("Index", "Home");
           }
           else
           {
                ViewBag.Notification = "Username or password wrong!";
           }

            return View(); 
        }

        public ActionResult Create()
        {          
            return RedirectToAction("Signup", "Home");
        }
  
        public ActionResult Details(int id)
        {            
          return View(SpwinEntities1.Students.Where(x=>x.StuID ==id).FirstOrDefault());
            
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(SpwinEntities1.Students.Where(x => x.StuID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Edit(int id, Student student)
        {
            try
            {

                //student.Password = Session["PasswordSS"].ToString();
                //student.RePassword = Session["RePasswordSS"].ToString();              

                {
                   
                    SpwinEntities1.Entry(student).State = EntityState.Modified;
                    SpwinEntities1.SaveChanges();

                }
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
            
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View(SpwinEntities1.Students.Where(x => x.StuID == id).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                {
                    Student student = SpwinEntities1.Students.Where(x=>x.StuID == id).FirstOrDefault();
                    SpwinEntities1.Students.Remove(student);
                    SpwinEntities1.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            
        }
    }
}