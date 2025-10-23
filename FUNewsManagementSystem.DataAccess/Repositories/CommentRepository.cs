using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUNewsManagementSystem.DataAccess.Contexts;
using FUNewsManagementSystem.DataAccess.Models;

namespace FUNewsManagementSystem.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly FunewsDbContext _context;

        public CommentRepository(FunewsDbContext context)
        {
            _context = context;
        }
        public List<Comment> GetCommentsByArticleId(int articleId)
        {
            return _context.Comments.Where(c => c.NewsArticleId == articleId)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();

        }
    }
}
