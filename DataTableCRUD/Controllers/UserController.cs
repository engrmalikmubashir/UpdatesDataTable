using DataTableCRUD.Models.DBModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DataTableCRUD.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetData()
        {
            using (DataTableCRUDDbContext db = new DataTableCRUDDbContext())
            {
                List<User> userList = db.Users.ToList<User>();
                return Json(new { data = userList }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
            {
                return View(new User());
            }
            else
            {
                using (DataTableCRUDDbContext db = new DataTableCRUDDbContext())
                {
                    return View(db.Users.Where(x => x.UserID == id).FirstOrDefault<User>());
                }
            }

        }


        [HttpPost]
        public ActionResult AddOrEdit(User user)
        {
            using (DataTableCRUDDbContext db = new DataTableCRUDDbContext())
            {
                if (user.UserID == 0)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Data inserted successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Data updated successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            using (DataTableCRUDDbContext db = new DataTableCRUDDbContext())
            {
                User user = db.Users.Where(x => x.UserID == id).FirstOrDefault<User>();
                db.Users.Remove(user);
                db.SaveChanges();
                return Json(new { success = true, message = "Data deleted successfully" }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
