using ASPNetProject.Data;
using ASPNetProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetProject.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly Context _context;
    
    public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Context context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }
    
    
    public IActionResult Index()
    {
        return RedirectToAction("Login");
    }
    
    public async Task<IActionResult> Users()
    {
        var users = await _context.Users.ToListAsync();
        return View(users);
    }
    
    
    public IActionResult Login()
    {
        return View(new LoginVM());
    }

    public IActionResult Register()
    {
        return View(new RegisterVM());
    }

    
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM loginVM)
    {
         if (!ModelState.IsValid)
         {
             return View(loginVM);
         }
         
         var user = await _userManager.FindByEmailAsync(loginVM.Email);
         if (user != null)
         {
             var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var signInResult = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Niepoprawny email lub hasło";
                return View(loginVM);
         }
         
         TempData["Error"] = "Niepoprawny email lub hasło";
         return View(loginVM);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM registerVm)
    {
        if (!ModelState.IsValid)
        {
            return View(registerVm);
        }
        
        var userFromDb = await _userManager.FindByEmailAsync(registerVm.Email);
        if (userFromDb != null)
        {
            TempData["Error"] = "Użytkownik o podanym emailu już istnieje";
            return View(registerVm);
        }
        
        var user = new ApplicationUser()
        {
            Email = registerVm.Email,
            UserName = registerVm.FullName,
            FullName = registerVm.FullName
        };
        var resultRegister = await _userManager.CreateAsync(user, registerVm.Password);

        if (resultRegister.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
            return View("Login", new LoginVM()
            {
                Email =  registerVm.Email,
            });
        }
        return View(registerVm);
    }
    
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
}