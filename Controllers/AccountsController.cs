using asp_identity.Data;
using asp_identity.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace asp_identity.Controllers
{
    public class AccountsController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountsController(ApplicationDbContext context,UserManager<IdentityUser> userManager , SignInManager<IdentityUser> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register model)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.Phone,
            };

                var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }

            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {

            var user = signInManager.UserManager.Users.Where(u => u.Email == model.Email).FirstOrDefault();

            var result = await signInManager.PasswordSignInAsync(user.UserName,model.Password,model.RememberMe,false);



            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(model);

        }
    }
}
