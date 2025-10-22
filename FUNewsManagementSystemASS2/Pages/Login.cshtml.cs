using FUNewsManagementSystem.BusinessLogic.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagementSystem.WebApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AccountService _accountService;
        private readonly IConfiguration _config;

        public LoginModel(AccountService accountService, IConfiguration config)
        {
            _accountService = accountService;
            _config = config;
        }

        [BindProperty] public string Email { get; set; } = "";
        [BindProperty] public string Password { get; set; } = "";
        public string ErrorMessage { get; set; } = "";


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // --- TH1: Admin (appsettings.json) ---
            var adminEmail = _config["AdminAccount:Email"];
            var adminPass = _config["AdminAccount:Password"];

            if (Email == adminEmail && Password == adminPass)
            {
                HttpContext.Session.SetInt32("UserId", 0);
                HttpContext.Session.SetString("UserName", "Admin");
                HttpContext.Session.SetInt32("Role", 0); // Role 0 for Admin
                return RedirectToPage("/Dashboard/Admin");
            }

            var user = _accountService.ValidateUser(Email, Password);

            if (user == null)
            {
                ErrorMessage = "Sai email hoặc mật khẩu!";
                return Page();
            }

            HttpContext.Session.SetInt32("UserId", user.AccountId);
            HttpContext.Session.SetString("UserName", user.AccountName);
            HttpContext.Session.SetInt32("Role", user.AccountRole);


            if (user.AccountRole == 1)
            {
                return RedirectToPage("/Dashboard/Staff");
            }

            if (user.AccountRole == 2)
            {
                return RedirectToPage("/Dashboard/Lecturer");
            }
            //không có vai trò hợp lệ
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
