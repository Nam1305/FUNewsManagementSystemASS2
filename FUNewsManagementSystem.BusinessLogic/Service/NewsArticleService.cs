using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUNewsManagementSystem.DataAccess.Models;
using FUNewsManagementSystem.DataAccess.Repositories;

namespace FUNewsManagementSystem.BusinessLogic.Service
{
    public class NewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepository;
        public NewsArticleService(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        public List<NewsArticle> GetActiveNews()
        {
            return _newsArticleRepository.GetActiveNews();
        }

        public NewsArticle? GetById(int id) => _newsArticleRepository.GetById(id);

        public List<NewsArticle> GetRelatedArticles(NewsArticle article)
        {
            if (article == null) return new List<NewsArticle>();

            return _newsArticleRepository.GetRelatedArticles(article.CategoryId, article.NewsArticleId);
        }
    }
}
