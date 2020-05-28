using CRUDOperationUsingJQueryAjax_Demo.Data;
using CRUDOperationUsingJQueryAjax_Demo.Models;
using CRUDOperationUsingJQueryAjax_Demo.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUDOperationUsingJQueryAjax_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var departments = db.Departments.ToList();
            ViewBag.ListOfDepartment = new SelectList(departments, "DepartmentId", "DepartmentName");
            return View();
        }

        public JsonResult GetStudentList()
        {
            List<StudentViewModel> students = db.Students.Where(x => x.IsDeleted == false).Select(x => new StudentViewModel
            {
                StudentId = x.StudentId,
                StudentName = x.StudentName,
                Email = x.Email,
                DepartmentName = x.Department.DepartmentName
            }).ToList();

            return Json(students, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentById(int StudentId)
        {
            var model = db.Students.Where(x => x.StudentId == StudentId).SingleOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(model, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveDataInDatabase(StudentViewModel model)
        {
            var result = false;
            try
            {
                if (model.StudentId > 0)
                {
                    var Stu = db.Students.SingleOrDefault(x => x.IsDeleted == false && x.StudentId == model.StudentId);
                    Stu.StudentName = model.StudentName;
                    Stu.Email = model.Email;
                    Stu.DepartmentId = model.DepartmentId;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    var student = new Student
                    {
                        StudentName = model.StudentName,
                        Email = model.Email,
                        DepartmentId = model.DepartmentId,
                        IsDeleted = false
                    };
                    db.Students.Add(student);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStudentRecord(int StudentId)
        {
            bool result = false;
            var Stu = db.Students.SingleOrDefault(x => x.IsDeleted == false && x.StudentId == StudentId);
            if (Stu != null)
            {
                Stu.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}