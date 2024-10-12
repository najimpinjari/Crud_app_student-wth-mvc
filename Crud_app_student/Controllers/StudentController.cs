using Crud_app_student.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Crud_app_student.Controllers
{
    public class StudentController : Controller
    {

        newpracticeEntities db = new newpracticeEntities();
        // GET: Student
        public ActionResult Index()
        {
            var data = db.Students.ToList();
            return View(data);
        }
        public ActionResult CreateStudent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateStudent( Student s )
        {

           if(ModelState.IsValid)
           {
                db.Students.Add(s);
                int a = db.SaveChanges();
                if (a > 0)
                {
                    TempData["success"] = "Data Added Succssfull";
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["success"] = "Data Added faild";
                }

           }
            return View("index");

        }


        public ActionResult UpdateStudent( int id )
        {
            var student = db.Students.Where( s => s.Id == id ).FirstOrDefault();
            return View(student);
        }

        [HttpPost]
        public ActionResult UpdateStudent( Student s )
        {
            if (ModelState.IsValid)
            {
                db.Entry(s).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                if (db.SaveChanges() > 0)
                {
                    TempData["Update"] = "Data Update Succssfull";
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["Update"] = "Data Update faild";
                }
            }
            return RedirectToAction("index");

        }

        public ActionResult Details(int id)
        {
            var row = db.Students.Find(id);
           if(row == null)
           {
                TempData["detals"] = "data not avalable";
                return RedirectToAction("index");
           }
           return View(row);
           
        }
        public ActionResult Delete(int id)
        {
            var row = db.Students.Find(id);
            if (row == null)
            {
                TempData["detals"] = "data not avalable";
                return RedirectToAction("index");
            }
            return View(row);

        }
        [HttpPost]
        public ActionResult Delete(Student s )
        {
            var row = db.Students.Find(s.Id);
            if( row != null)
            {
                db.Students.Remove(row);
                if(db.SaveChanges() > 0)
                {
                    TempData["delete"] = "data delete succssully";
                    return RedirectToAction("index");
                }
                else
                {
                    TempData["delete"] = "data not avalable";
                }
            }
            return RedirectToAction("index");

        }





    }
}