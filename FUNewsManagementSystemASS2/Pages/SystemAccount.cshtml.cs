using FUNewsManagementSystem.BusinessLogic.Service;
using FUNewsManagementSystem.DataAccess.Contexts;
using FUNewsManagementSystem.DataAccess.Models;
using FUNewsManagementSystem.WebApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagementSystem.WebApp.Pages
{
    [IgnoreAntiforgeryToken]
    public class SystemAccountModel : PageModel
    {
        private readonly AccountService _accountService;
        private readonly IHubContext<NotificationHub> _hubContext;
        public SystemAccountModel(AccountService accountService, IHubContext<NotificationHub> hubContext)
        {
            _accountService = accountService;
            _hubContext = hubContext;
        }

        public IEnumerable<SystemAccount> Accounts { get; set; } = new List<SystemAccount>();

        [BindProperty(SupportsGet = true)]
        public string? Keyword { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Role { get; set; }

        [BindProperty]
        public SystemAccount NewAccount { get; set; } = new();

        public void OnGet()
        {
            Accounts = _accountService.GetAll(Keyword, Role);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var deletedUser = _accountService.GetById(id);
            var result = _accountService.Delete(id);

            if (result == "Success" && deletedUser != null)
            {
                // ⚠️ Ngắt kết nối user bị xóa (nếu đang online)
                await _hubContext.Clients.User(deletedUser.AccountId.ToString())
                    .SendAsync("AccountDeleted", deletedUser.AccountId);

                return new JsonResult(new { success = true });
            }

            return new JsonResult(new { success = false, message = result });
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            var result = _accountService.Add(NewAccount);

            if (result == "Success")
            {
                // 🔔 Gửi thông báo realtime đến nhóm Staff
                await _hubContext.Clients.Group("Group_Staff")
                    .SendAsync("ReceiveNotification", $"Admin đã thêm tài khoản mới: {NewAccount.AccountEmail}");

                return new JsonResult(new { success = true });
            }

            return new JsonResult(new { success = false, message = result });
        }


    }
}
