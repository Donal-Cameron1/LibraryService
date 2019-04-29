using LibraryService.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LibraryService
{
    [Authorize(Roles = "Admin")]
    public class RolesAdminController : Controller
    {
        ApplicationDbContext context;

        public RolesAdminController()
        {
            context = new ApplicationDbContext();
        }

        //displays all the existing roles in a list
        public ActionResult Index()
        {
            var Roles = context.Roles.ToList();
            return View(Roles);
        }

        //returns a view that lets you create a new role
        public ActionResult Create()
        {
            return View();
        }

        //saves the created role to the database
        [HttpPost]
        public ActionResult Create(IdentityRole Role)
        {
            context.Roles.Add(Role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //returns a view that lets you edit a role
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var role = context.Roles.Find(id);
            return View(role);
        }

        //saves the edited role to the database
        [HttpPost]
        public ActionResult Edit(IdentityRole Role)
        {
            context.Entry(Role).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //removes a role form the database
        public ActionResult Delete(string id)
        {
            var role = context.Roles.Find(id);
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}