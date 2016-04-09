using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        Model1 db = new Model1();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
        public const string _HomeController = "Home";
        public const string _Index = "Index";

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string name, int note)
        {
            Student addStudent = new Student
            {
                Name = name,
                Note = note
            };
            db.Students.Add(addStudent);
            db.SaveChanges();
            return RedirectToAction(_Index);
        }
        public ActionResult Delete(int? Id)
        {
            Student student = db.Students.Find(Id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        public ActionResult Delete(int Id)
        {
            Student student = db.Students.Find(Id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? Id)
        {
            Student student = db.Students.Find(Id);
            return View(student);
        }

        public ActionResult Edit(int Id)
        {
            Student student = db.Students.Find(Id);
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name,Note")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);
        }
    }
}