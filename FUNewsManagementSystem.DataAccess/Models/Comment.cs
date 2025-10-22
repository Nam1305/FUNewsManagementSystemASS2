using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagementSystem.DataAccess.Models;

[Table("Comment")]
public partial class Comment
{
    [Key]
    [Column("CommentID")]
    public int CommentId { get; set; }

    [Column("NewsArticleID")]
    public int NewsArticleId { get; set; }

    [Column("AccountID")]
    public int AccountId { get; set; }

    [StringLength(1000)]
    public string Content { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [ForeignKey("AccountId")]
    [InverseProperty("Comments")]
    public virtual SystemAccount Account { get; set; } = null!;

    [ForeignKey("NewsArticleId")]
    [InverseProperty("Comments")]
    public virtual NewsArticle NewsArticle { get; set; } = null!;
}
