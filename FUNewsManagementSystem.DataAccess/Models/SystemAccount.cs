using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FUNewsManagementSystem.DataAccess.Models;

[Table("SystemAccount")]
public partial class SystemAccount
{
    [Key]
    [Column("AccountID")]
    public int AccountId { get; set; }

    [StringLength(100)]
    public string AccountName { get; set; } = null!;

    [StringLength(70)]
    public string AccountEmail { get; set; } = null!;

    public int AccountRole { get; set; }

    [StringLength(70)]
    public string AccountPassword { get; set; } = null!;

    [InverseProperty("Account")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("CreatedBy")]
    public virtual ICollection<NewsArticle> NewsArticleCreatedBies { get; set; } = new List<NewsArticle>();

    [InverseProperty("UpdatedBy")]
    public virtual ICollection<NewsArticle> NewsArticleUpdatedBies { get; set; } = new List<NewsArticle>();
}
