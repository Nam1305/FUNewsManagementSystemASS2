using FUNewsManagementSystem.DataAccess.Contexts;
using FUNewsManagementSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagementSystem.WebApp.Pages
{
    public class SystemAccountModel : PageModel
    {
        private readonly FunewsDbContext _dbContext;
        public SystemAccountModel(FunewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<SystemAccount> Accounts { get; set; } = new List<SystemAccount>();

        public async Task OnGetAsync()
        {
            Accounts = await _dbContext.SystemAccounts.AsNoTracking().ToListAsync();
        }
    }
}
