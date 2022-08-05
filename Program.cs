using Community_BackEnd.Data;
using Community_BackEnd.Entities;
using Community_BackEnd.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Community_BackEnd;
public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		//var tokenKey = builder.Configuration.GetValue<string>("TokenKey");
		//var key = Encoding.ASCII.GetBytes(tokenKey);
		ConfigurationManager configuration = builder.Configuration;

		builder.Services.AddCors(
			options => options.AddPolicy(
				name: "AllowCORS",
				policy => policy
					.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
					.AllowCredentials()
					.AllowAnyMethod()
					.AllowAnyHeader()
				)
			);

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddDbContext<AppDbContext>(
			options => options.UseSqlServer(
				builder.Configuration.GetConnectionString("DefaultConnection"),
					options => options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
				
				)
			);
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddIdentity<AppUser, IdentityRole>(
			options => options.SignIn.RequireConfirmedAccount = false)
			.AddDefaultTokenProviders()
			.AddEntityFrameworkStores<AppDbContext>();
		// Adding Authentication service
		builder.Services.AddAuthentication(
			options => {
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			// Adding Jwt Bearer
			.AddJwtBearer(options => {
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new TokenValidationParameters() {
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidAudience = configuration["JWT:ValidAudience"],
					ValidIssuer = configuration["JWT:ValidIssuer"],
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
				};
			});

		if(builder.Environment.IsDevelopment())
		{
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddSwaggerGen();
		}


		var app = builder.Build();

		if(app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			//app.UseSwagger();
			app.UseSwaggerUI();
		}
		//app.UseEndpoints(endpoints =>
		//{
		//	endpoints.MapControllers();
		//});
		app.UseHttpsRedirection();

		app.UseStaticFiles();
		app.UseRouting();
		app.UseCors("AllowCORS");
		//authentication preceds authorization
		app.UseAuthentication();
		app.UseAuthorization();

		//app.MapControllers();
		
		app.Run();
	}
}
