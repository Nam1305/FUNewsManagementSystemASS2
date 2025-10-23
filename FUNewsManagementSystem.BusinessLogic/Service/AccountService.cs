using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUNewsManagementSystem.DataAccess.Models;
using FUNewsManagementSystem.DataAccess.Repositories;

namespace FUNewsManagementSystem.BusinessLogic.Service
{
    public class AccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public SystemAccount? ValidateUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            return _accountRepository.GetByEmailAndPassword(email, password);
        }

        public IEnumerable<SystemAccount> GetAll(string? keyword = null, int? role = null)
            => _accountRepository.GetAll(keyword, role);

        public SystemAccount? GetById(int id)
            => _accountRepository.GetById(id);

        public string Add(SystemAccount account)
        {
            if (_accountRepository.EmailExists(account.AccountEmail))
                return "Email đã tồn tại!";

            _accountRepository.Add(account);
            _accountRepository.Save();
            return "Success";
        }

        public string Update(SystemAccount account)
        {
            if (_accountRepository.EmailExists(account.AccountEmail, account.AccountId))
                return "Email đã tồn tại!";

            _accountRepository.Update(account);
            _accountRepository.Save();
            return "Success";
        }

        public string Delete(int id)
        {
            if (_accountRepository.HasCreatedArticles(id))
                return "Không thể xóa tài khoản đã tạo bài viết!";

            _accountRepository.Delete(id);
            _accountRepository.Save();
            return "Success";
        }

    }
}
