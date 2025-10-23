using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUNewsManagementSystem.DataAccess.Models;

namespace FUNewsManagementSystem.DataAccess.Repositories
{
    public interface ICommentRepository
    {
        List<Comment> GetCommentsByArticleId(int articleId);
    }
}
