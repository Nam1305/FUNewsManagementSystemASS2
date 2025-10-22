using FUNewsManagementSystem.BusinessLogic.Service;
using FUNewsManagementSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagementSystemASS2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly NewsArticleService _newsArticleService;

        public IndexModel(ILogger<IndexModel> logger, NewsArticleService newsArticleService)
        {
            _logger = logger;
            _newsArticleService = newsArticleService;

        }
        public List<NewsArticle> ActiveNews { get; set; } = new();
        public void OnGet()
        {
            ActiveNews = _newsArticleService.GetActiveNews();
        }
    }
}
