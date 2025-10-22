using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUNewsManagementSystem.DataAccess.Models;
using FUNewsManagementSystem.DataAccess.Contexts;

namespace FUNewsManagementSystem.DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly FunewsDbContext _context;
        public AccountRepository(FunewsDbContext context)
        {
            _context = context;
        }
        public SystemAccount? GetByEmailAndPassword(string email, string password)
        {
            return _context.SystemAccounts.FirstOrDefault(acc => acc.AccountEmail == email && acc.AccountPassword == password);
        }
    }
}
