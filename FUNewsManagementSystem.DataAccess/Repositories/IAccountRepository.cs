using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUNewsManagementSystem.DataAccess.Models;

namespace FUNewsManagementSystem.DataAccess.Repositories
{
    public interface IAccountRepository
    {
        SystemAccount? GetByEmailAndPassword(string email, string password);
    }
}
