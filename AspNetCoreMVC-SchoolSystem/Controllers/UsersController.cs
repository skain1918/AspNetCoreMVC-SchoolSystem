using AspNetCoreMVC_SchoolSystem.Models;
using AspNetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC_SchoolSystem.Controllers;
public class UsersController : Controller {
    UserManager<AppUser> _userManager;

    public UsersController(UserManager<AppUser> userManager) {
        _userManager = userManager;
        }

    public IActionResult Index() {
        return View(_userManager.Users);
        }
    public IActionResult Create() {
        return View();
        }
    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserViewModel newUser) {
        if (ModelState.IsValid) {
            AppUser userToAdd = new AppUser {
                UserName = newUser.Name,
                Email = newUser.Email
                };
            IdentityResult result = await _userManager.CreateAsync(userToAdd, newUser.Password);
            if (result.Succeeded) {
                return RedirectToAction("Index");
                }
            else {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                    }
                }
            }
        return View(newUser);
        }
    }