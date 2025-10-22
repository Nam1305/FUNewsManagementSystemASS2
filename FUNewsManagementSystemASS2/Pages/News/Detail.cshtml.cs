using FUNewsManagementSystem.BusinessLogic.Service;
using FUNewsManagementSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagementSystem.WebApp.Pages.News
{
    public class DetailModel : PageModel
    {
        private readonly NewsArticleService _newsService;

        public DetailModel(NewsArticleService newsService)
        {
            _newsService = newsService;
        }

        public NewsArticle? News { get; set; }
        public List<NewsArticle> RelatedArticles { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            News = _newsService.GetById(id);
            if (News == null)
            {
                return RedirectToPage("/Index");
            }

            RelatedArticles = _newsService.GetRelatedArticles(News);
            return Page();
        }
    }
}
