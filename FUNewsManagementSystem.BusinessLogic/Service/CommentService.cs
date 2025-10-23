using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FUNewsManagementSystem.DataAccess.Models;
using FUNewsManagementSystem.DataAccess.Repositories;

namespace FUNewsManagementSystem.BusinessLogic.Service
{
    public class CommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public List<Comment> GetCommentsByArticleId(int articleId)
        {
            if (articleId <= 0)
                return new List<Comment>();
            return _commentRepository.GetCommentsByArticleId(articleId);
        }
    }
}
