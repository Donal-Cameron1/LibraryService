using LibraryService.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace LibraryService
{
    public class InitAdminController : Controller
    {        
        public ApplicationDbContext db;
        public DropCreateDatabaseIfModelChanges<DbContext> dbInit;

        public InitAdminController()
        {
            db = new ApplicationDbContext();
            dbInit = new DropCreateDatabaseIfModelChanges<DbContext>();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string adminUserEmail = "admin@example.com", string adminPassword = "Admin@123456")
        {
            bool init = Init(db, adminUserEmail, adminPassword);
            if (init)
            {
                return RedirectToAction("Success");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        public bool Init(ApplicationDbContext db, string adminUserEmail, string adminPassword)
        {
            try
            {
                dbInit.InitializeDatabase(db);

                ApplicationUserManager userManager = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

                RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(db);
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(roleStore);

                string name = adminUserEmail;
                string password = adminPassword;
                string roleName = "Admin";

                //Create Role Admin if it does not exist
                IdentityRole role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    IdentityResult roleresult = roleManager.Create(role);
                }

                ApplicationUser user = userManager.FindByName(name);
                if (user == null)
                {
                    user = new ApplicationUser { UserName = name, Email = name };
                    IdentityResult result = userManager.Create(user, password);
                    result = userManager.SetLockoutEnabled(user.Id, false);
                }

                // Add user admin to Role Admin if not already added
                System.Collections.Generic.IList<string> rolesForUser = userManager.GetRoles(user.Id);
                if (!rolesForUser.Contains(role.Name))
                {
                    IdentityResult result = userManager.AddToRole(user.Id, role.Name);
                }
                return true;
            }
            catch(Exception )
            {
                return false;
            }
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}