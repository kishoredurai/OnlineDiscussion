using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineDiscussion.Areas.Identity.Data;
using OnlineDiscussion.Models;

namespace OnlineDiscussion.Data;

public class OnlineDiscussionContext : IdentityDbContext<OnlineDiscussionUser>
{
    public OnlineDiscussionContext(DbContextOptions<OnlineDiscussionContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public DbSet<OnlineDiscussion.Models.TopicViewModel>? TopicViewModel { get; set; }

    public DbSet<OnlineDiscussion.Models.FriendsView>? FriendsView { get; set; }

    public DbSet<OnlineDiscussion.Models.CommentView>? CommentView { get; set; }
}
