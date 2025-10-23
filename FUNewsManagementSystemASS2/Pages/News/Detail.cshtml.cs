using FUNewsManagementSystem.BusinessLogic.Service;
using FUNewsManagementSystem.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FUNewsManagementSystem.WebApp.Pages.News
{
    public class DetailModel : PageModel
    {
        private readonly NewsArticleService _newsService;
        private readonly CommentService _commentService;

        public DetailModel(NewsArticleService newsService, CommentService commentService)
        {
            _newsService = newsService;
            _commentService = commentService;
        }

        public NewsArticle? News { get; set; }
        public List<NewsArticle> RelatedArticles { get; set; } = new();

        public List<Comment> Comments { get; set; }
        [BindProperty]
        public string NewCommentContent { get; set; } = string.Empty;



        public IActionResult OnGet(int id)
        {
            News = _newsService.GetById(id);
            Comments = _commentService.GetCommentsByArticleId(id);
            if (News == null)
            {
                return RedirectToPage("/Index");
            }

            RelatedArticles = _newsService.GetRelatedArticles(News);
            return Page();
        }
    }
}
