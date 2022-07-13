using Community_BackEnd.Data.Forums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Community_BackEnd.Data;
public class AppDbContext : DbContext 
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<NewsArticle> News { get; set; }
	public DbSet<Forum> Forums { get; set; }
	public DbSet<Topic> Topics { get; set; }
	public DbSet<Post> Posts { get; set; }
	public DbSet<AppUser> Users { get; set; }

}
