using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestCuoi.Models;

namespace TestCuoi.Controllers
{
    public class EmployeeController : Controller
    {
        private Model1 db = new Model1();

        // GET: Employee
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department);
            return View(employees.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View(db.Accouts.ToList());    
        }
        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            var a = db.Accouts.Where(p => p.username == user && p.password == pass).FirstOrDefault();
            if (a != null)
            {
                Session["user"]=user;
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ViewBag.err = "Sai tên đăng nhập hoặc mật khẩu!";
                return View ("Login");
            }
        }
        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Login", "Employee");
        }
        [ChildActionOnly]
        public PartialViewResult CategoryMenu()
        {
            var li = db.Departments.ToList();   
            return PartialView(li);
        }
        [Route("NhanVienTheoPhong/{deptid}")]
        public ActionResult HienThiTheoPhong(int deptid)
        {
            var li = db.Employees.Where(e=>e.deptid== deptid).ToList();
            return View(li);    
        }

        // GET: Employee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname");
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Employee emp)
        {

            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                return Json(new { a = true, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return Json(new { a = false, error=ex.Message });

            }
          
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname", employee.deptid);
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "eid,name,age,addr,salary,image,deptid")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.deptid = new SelectList(db.Departments, "deptid", "deptname", employee.deptid);
            return View(employee);
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
