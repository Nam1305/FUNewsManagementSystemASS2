using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUNewsManagementSystem.DataAccess.Contexts;
using FUNewsManagementSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagementSystem.DataAccess.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly FunewsDbContext _context;
        public NewsArticleRepository(FunewsDbContext context)
        {
            _context = context;
        }
        public List<NewsArticle> GetActiveNews()
        {
            return _context.NewsArticles
                .Where(na => na.NewsStatus == true)
                .ToList();
        }

        public NewsArticle? GetById(int id)
        {
            return _context.NewsArticles
                .Include(n => n.Category)
                .Include(n => n.CreatedBy)
                .Include(n => n.Tags)
                .FirstOrDefault(n => n.NewsArticleId == id && n.NewsStatus == true);
        }

        public List<NewsArticle> GetRelatedArticles(int? categoryId, int excludeId)
        {
            return _context.NewsArticles
                .Where(n => n.CategoryId == categoryId && n.NewsArticleId != excludeId && n.NewsStatus == true)
                .OrderByDescending(n => n.CreatedDate)
                .Take(5)
                .ToList();
        }
    }
}
