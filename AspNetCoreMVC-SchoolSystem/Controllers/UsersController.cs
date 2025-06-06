﻿using AspNetCoreMVC_SchoolSystem.Models;
using AspNetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMVC_SchoolSystem.Controllers;
[Authorize(Roles = "Admin")]
public class UsersController : Controller {
    UserManager<AppUser> _userManager;
    IPasswordHasher<AppUser> _passwordHasher;
    IPasswordValidator<AppUser> _passwordValidator;
    public UsersController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher, IPasswordValidator<AppUser> passwordValidator) {
        _userManager = userManager;
        _passwordHasher = passwordHasher;
        _passwordValidator = passwordValidator;
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
            AppUser userToAdd = new AppUser
                {
                UserName = newUser.Name,
                Email = newUser.Email
                };
            IdentityResult result = await _userManager.CreateAsync(userToAdd, newUser.Password);
            if (result.Succeeded) {
                return RedirectToAction("Index");
                }
            else {
                AddIdentityError(result);
                }
            }
        return View(newUser);
        }
    [HttpGet]
    public async Task<IActionResult> EditAsync(string id) {
        var userToEdit = await _userManager.FindByIdAsync(id);
        if (userToEdit == null) {
            return View("NotFound");
            }
        return View(userToEdit);
        }
    [HttpPost]
    public async Task<IActionResult> EditAsync(string id, string email, string password) {
        AppUser userToEdit = await _userManager.FindByIdAsync(id);
        if (userToEdit == null) {
            return View("NotFound");
            }
        if (!string.IsNullOrEmpty(email)) {
            userToEdit.Email = email;
            }
        else {
            ModelState.AddModelError("", "Email cannot be empty");
            }
        IdentityResult validPass = null;
        if (!string.IsNullOrEmpty(password)) {
            validPass = await _passwordValidator.ValidateAsync(_userManager, userToEdit, password);
            if (validPass.Succeeded) {
                            userToEdit.PasswordHash = _passwordHasher.HashPassword(userToEdit, password);
                }
            else {
                AddIdentityError(validPass);
                }
            }
        else {
            ModelState.AddModelError("", "Password cannot be empty");
            }
        if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password)) {
            if (validPass.Succeeded) {
                IdentityResult result = await _userManager.UpdateAsync(userToEdit);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                    }
                else {
                    AddIdentityError(result);
                    }
                }
            }
        return View(userToEdit);
        }
    [HttpPost]
    public async Task<IActionResult> DeleteAsync(string id) {
        AppUser userToDelete = await _userManager.FindByIdAsync(id);

        if (userToDelete != null) {
            IdentityResult result = await _userManager.DeleteAsync(userToDelete);
            if (result.Succeeded) {
                return RedirectToAction("Index");
                }
            else {
                AddIdentityError(result);
                }
            }
        else {
            ModelState.AddModelError("", "User not found");
            }
        return View("Index", _userManager.Users);
        }
    private void AddIdentityError(IdentityResult result) {
        foreach (var error in result.Errors) {
            ModelState.AddModelError("", error.Description);
            }
        }
    }