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

        //lấy full account
        IEnumerable<SystemAccount> GetAll(string? keyword = null, int? role = null);

        //Lấy account theo ID
        SystemAccount? GetById(int id);

        //Thêm 1 account
        void Add(SystemAccount account);

        //Chỉnh sửa thông tin account
        void Update(SystemAccount account);

        //Xóa account
        void Delete(int id);

        //Check xem email có tồn tại không
        bool EmailExists(string email, int? excludeId = null);

        //Check xem 1 account có tạo cái article nào chưa
        bool HasCreatedArticles(int accountId);

        //lưu account
        void Save();

    }
}
