using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VoedselVerspillingWebApp.Models;

namespace VoedselVerspillingWebApp.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(loginViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var res = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberUser,
                false
            );

            if (res.Succeeded)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Email/wachtwoord incorrect!");
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Register(registerViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email
        };

        var res = await _userManager.CreateAsync(user, model.Password);
        if (res.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }

        foreach (var e in res.Errors)
        {
            ModelState.AddModelError("", e.Description);
        }

        return View(model);
    }

    [Authorize]
    public IActionResult Profile()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}