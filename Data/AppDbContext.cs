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
	public DbSet<AppUser> Users { get; set; }
	public DbSet<IdentityRole> Roles { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);


	}

}
