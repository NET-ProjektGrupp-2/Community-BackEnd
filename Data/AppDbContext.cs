using Community_BackEnd.Data.Forums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Community_BackEnd.Data;
public class AppDbContext : IdentityDbContext<AppUser> 
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<NewsArticle> News { get; set; }
	public DbSet<Forum> Forums { get; set; }
	public DbSet<Topic> Topics { get; set; }
	public DbSet<Post> Posts { get; set; }
	//public DbSet<AppUser> Users { get; set; }
	//public DbSet<IdentityRole> Roles { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<AppUser>()
			.HasMany(u => u.Posts)
			.WithOne()
			.HasForeignKey(p => p.AuthorId)
			.HasPrincipalKey(u => u.Id);
		modelBuilder.Entity<AppUser>()
			.HasMany(u => u.Topics)
			.WithOne()
			.HasForeignKey(t => t.AuthorId)
			.HasPrincipalKey(u => u.Id);
		modelBuilder.Entity<AppUser>()
			.HasMany<Forum>()
			.WithMany(f => f.Moderators);
		modelBuilder.Entity<AppUser>()
			.HasMany<NewsArticle>()
			.WithOne()
			.HasForeignKey(a => a.AuthorId);

		modelBuilder.Entity<IdentityRole>()
			.HasMany<Forum>()
			.WithOne()
			.HasForeignKey(f => f.RestrictedRoleId)
			.HasPrincipalKey(r => r.Id);

		modelBuilder.Entity<Forum>()
			.HasMany(f => f.Topics)
			.WithOne()
			.HasForeignKey(t => t.ForumId)
			.HasPrincipalKey(f => f.Id);
		modelBuilder.Entity<Forum>()
			.HasMany(f => f.SubForums)
			.WithOne()
			.HasForeignKey(f => f.ParentForumId)
			.HasPrincipalKey(f => f.Id);

		modelBuilder.Entity<Topic>()
			.HasMany(t => t.Posts)
			.WithOne()
			.HasForeignKey(p => p.TopicId)
			.HasPrincipalKey(t => t.Id);
		modelBuilder.Entity<Topic>()
			.HasOne<NewsArticle>()
			.WithOne()
			.HasForeignKey<NewsArticle>(a => a.DiscussionThreadId)
			.HasPrincipalKey<Topic>(t => t.Id);

		modelBuilder.Entity<Post>()
			.HasMany<Post>()
			.WithOne()
			.HasForeignKey(p => p.ReplyToId)
			.HasPrincipalKey(p => p.Id);

		if(!Users.Any())
		{
			modelBuilder.Entity<AppUser>().HasData(StaticDummyDB.Users);
			modelBuilder.Entity<IdentityRole>().HasData(StaticDummyDB.Roles);
			modelBuilder.Entity<Forum>().HasData(StaticDummyDB.Forums);
			modelBuilder.Entity<Topic>().HasData(StaticDummyDB.Topics);
			modelBuilder.Entity<Post>().HasData(StaticDummyDB.Posts);
			modelBuilder.Entity<NewsArticle>().HasData(StaticDummyDB.News);
			modelBuilder.Entity<IdentityUserRole<string>>().HasData(StaticDummyDB.UserRoles);
		}
	}
}
