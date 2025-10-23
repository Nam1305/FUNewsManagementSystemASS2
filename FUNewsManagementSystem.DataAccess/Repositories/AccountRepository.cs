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

        //thêm account
        public void Add(SystemAccount account)
        {
           _context.Add(account);
        }


        //Xóa account
        public void Delete(int id)
        {
            var acc = GetById(id);
            if (acc != null)
                _context.SystemAccounts.Remove(acc);
        }

        //Check xem email có tồn tại không
        public bool EmailExists(string email, int? excludeId = null)
        {
            return _context.SystemAccounts
                .Any(a => a.AccountEmail == email && (!excludeId.HasValue || a.AccountId != excludeId.Value));
        }

        //Lấy full account
        public IEnumerable<SystemAccount> GetAll(string? keyword = null, int? role = null)
        {
            var query = _context.SystemAccounts.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a =>
                    a.AccountName.Contains(keyword) ||
                    a.AccountEmail.Contains(keyword));
            }

            if (role.HasValue)
            {
                query = query.Where(a => a.AccountRole == role);
            }

            return query.ToList();
        }


        //Lấy account dựa vào email và password
        public SystemAccount? GetByEmailAndPassword(string email, string password)
        {
            return _context.SystemAccounts.FirstOrDefault(acc => acc.AccountEmail == email && acc.AccountPassword == password);
        }

        //Lấy account dựa vào id
        public SystemAccount? GetById(int id)
        {
            return _context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
        }

        //check xem account đó đã tạo bài viết nào chưa
        public bool HasCreatedArticles(int accountId)
        {
            return _context.NewsArticles.Any(n => n.CreatedById == accountId);
        }

        //lưu account
        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(SystemAccount account)
        {
            _context.SystemAccounts.Update(account);
        }
    }
}
