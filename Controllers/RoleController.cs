using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quintrix_Web_App_Core_MVC.Models;
using System.ComponentModel.DataAnnotations;

namespace Quintrix_Web_App_Core_MVC.Controllers
{
	public class RoleController : Controller
	{
        private RoleManager<IdentityRole> roleManager;
        private UserManager<Player> userManager;
        IQueryable<IdentityRole> roles;
        IQueryable<Player> users;

        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<Player> userMrg)
        {
            roleManager = roleMgr;
            userManager = userMrg;

            roles = roleManager.Roles;
            users = userManager.Users;
        }

        public async Task<ViewResult> IndexAsync()
		{
			foreach (var role in roles)
			{
				foreach (var user in users)
				{
					if (await userManager.IsInRoleAsync(user, role.Name))
						ViewData[role.Id] += $"{user.Name},";
				}
			}

			return View(roleManager.Roles);
		}

        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<Player> members = new List<Player>();
            List<Player> nonMembers = new List<Player>();

            var userList = await userManager.Users.ToListAsync(); // this fixes the datareader issue
            // this was causing a datareader error
            //foreach (AppUser user in userManager.Users)
            foreach (Player user in userList)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    Player user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    Player user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(IndexAsync));
            else
                return await Update(model.RoleId);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }

        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
