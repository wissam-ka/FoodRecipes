using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FoodRecipes.Models;

namespace FoodRecipes.Areas.Administrator.Controllers
{
    public class AdminstratorController : Controller
    {
        //
        // GET: /Administrator/Adminstrator/

        public ActionResult Index()
        {
            var roles = Roles.GetAllRoles();
            return View(roles);
            
        }
        public ActionResult UserIndex()
        {
            using (var ctx = new UsersContext())
            {
                return View(ctx.UserProfiles.ToList());
            }
            

        }

        [HttpGet]
        public ActionResult CreatRole()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatRole(string RoleName)
        {
            Roles.CreateRole(Request.Form["RoleName"]);
            return RedirectToAction("Index", "Adminstrator");

        }
        
        public ActionResult RoleDelete(string RoleName)
        {
            Roles.DeleteRole(RoleName);
            return RedirectToAction("Index", "Adminstrator");
        }
        public ActionResult AddUserRole()
        {
            SelectList Rolelist = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = Rolelist;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUserRole(string RoleName, string UserName)
        {

            if (Roles.IsUserInRole(UserName, RoleName))
            {
                ViewBag.ResultMessage = "This user already has the role specified !";
            }
            else
            {
                Roles.AddUserToRole(UserName, RoleName);
                ViewBag.ResultMessage = "Username added  succesfully !";
            }

            SelectList Rolelist = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = Rolelist;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
        
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ViewBag.UsersRoles= Roles.GetRolesForUser(UserName);
                SelectList Rolelist = new SelectList(Roles.GetAllRoles());
                ViewBag.Roles = Rolelist;
            }
            return View("AddUserRole");
        }
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {

            if (Roles.IsUserInRole(UserName, RoleName))
            {
                Roles.RemoveUserFromRole(UserName, RoleName);
                ViewBag.ResultMessage = "Role removed from this user successfully !";
            }
            else
            {
                ViewBag.ResultMessage = "This user doesn't belong to selected role.";
            }
            ViewBag.RolesForThisUser = Roles.GetRolesForUser(UserName);
            SelectList list = new SelectList(Roles.GetAllRoles());
            ViewBag.Roles = list;


            return View("AddUserRole");
        }


    }
}
