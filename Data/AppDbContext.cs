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
			.WithOne(p => p.Author)
			.HasForeignKey(p => p.AuthorId)
			.HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.SetNull);
		modelBuilder.Entity<AppUser>()
			.HasMany(u => u.Topics)
			.WithOne(t => t.Author)
			.HasForeignKey(t => t.AuthorId)
			.HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.SetNull);
		modelBuilder.Entity<AppUser>()
			.HasMany(a => a.ModeratedForums)
			.WithMany(f => f.Moderators)
			.UsingEntity(fm => fm.HasData(
			new {
				ModeratorsId = StaticDummyDB.Users[0].Id,
				ModeratedForumsId = StaticDummyDB.Forums[0].Id
			},
			new {
				ModeratorsId = StaticDummyDB.Users[1].Id,
				ModeratedForumsId = StaticDummyDB.Forums[0].Id
			},
			new {
				ModeratorsId = StaticDummyDB.Users[2].Id,
				ModeratedForumsId = StaticDummyDB.Forums[0].Id
			},
			new {
				ModeratorsId = StaticDummyDB.Users[3].Id,
				ModeratedForumsId = StaticDummyDB.Forums[0].Id
			}
		));

		modelBuilder.Entity<AppUser>()
			.HasMany(u => u.Articles)
			.WithOne(a => a.Author)
			.HasForeignKey(a => a.AuthorId)
			.HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.SetNull);
		modelBuilder.Entity<AppUser>()
			.HasMany(u => u.IdentityUserRoles)
			.WithOne()
			.HasForeignKey(ur => ur.UserId)
			.HasPrincipalKey(u => u.Id).OnDelete(DeleteBehavior.Cascade);

		modelBuilder.Entity<IdentityRole>()
			.HasMany<Forum>()
			.WithOne(f => f.RestrictedRole)
			.HasForeignKey(f => f.RestrictedRoleId)
			.HasPrincipalKey(r => r.Id).OnDelete(DeleteBehavior.SetNull);

		modelBuilder.Entity<IdentityUserRole<string>>()
			.HasKey(ur => new { ur.UserId, ur.RoleId });

		modelBuilder.Entity<Forum>()
			.HasMany(f => f.Topics)
			.WithOne(t => t.Forum)
			.HasForeignKey(t => t.ForumId)
			.HasPrincipalKey(f => f.Id).OnDelete(DeleteBehavior.Cascade);
		modelBuilder.Entity<Forum>()
			.HasMany(fp => fp.SubForums)
			.WithOne(fs => fs.ParentForum)
			.HasForeignKey(fs => fs.ParentForumId)
			.HasPrincipalKey(fp => fp.Id).OnDelete(DeleteBehavior.ClientSetNull);

		modelBuilder.Entity<Topic>()
			.HasMany(t => t.Posts)
			.WithOne(p => p.Topic)
			.HasForeignKey(p => p.TopicId)
			.HasPrincipalKey(t => t.Id).OnDelete(DeleteBehavior.Cascade);
		modelBuilder.Entity<Topic>()
			.HasOne(t => t.Article)
			.WithOne(a => a.Topic)
			.HasForeignKey<NewsArticle>(a => a.TopicId)
			.HasPrincipalKey<Topic>(t => t.Id).OnDelete(DeleteBehavior.SetNull);

		modelBuilder.Entity<Post>()
			.HasMany(po => po.Replies)
			.WithOne(pr => pr.ContextPost)
			.HasForeignKey(p => p.ContextPostId)
			.HasPrincipalKey(p => p.Id).OnDelete(DeleteBehavior.NoAction);

		modelBuilder.Entity<AppUser>().HasData(StaticDummyDB.Users);
		modelBuilder.Entity<IdentityRole>().HasData(StaticDummyDB.Roles);
		modelBuilder.Entity<Forum>().HasData(StaticDummyDB.Forums);
		modelBuilder.Entity<Topic>().HasData(StaticDummyDB.Topics);
		modelBuilder.Entity<Post>().HasData(StaticDummyDB.Posts);
		modelBuilder.Entity<NewsArticle>().HasData(StaticDummyDB.News);
		modelBuilder.Entity<IdentityUserRole<string>>().HasData(StaticDummyDB.UserRoles);
	}
}
