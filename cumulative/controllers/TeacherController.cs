using System.Collections.Generic;
using System.Web.Mvc;
using YourNamespace.Models;

namespace YourNamespace.Controllers
{
    public class TeacherController : Controller
    {
        private readonly TeacherDataController _teacherDataController;

        public TeacherController()
        {
            _teacherDataController = new TeacherDataController(new SchoolDbContext());
        }

        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Teacher/List
        public ActionResult List()
        {
            IEnumerable<Teacher> teachers = _teacherDataController.GetTeachers();
            return View(teachers);
        }

        // GET: /Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            Teacher teacher = _teacherDataController.GetTeacher(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }

            return View(teacher);
        }
    }
}
