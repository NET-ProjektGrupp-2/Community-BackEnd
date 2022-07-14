using Community_BackEnd.Data;
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
		ConfigurationManager configuration = builder.Configuration;
		// For Identity
		builder.Services.AddIdentity<IdentityUser, IdentityRole>()
			.AddEntityFrameworkStores<AppDbContext>()
			.AddDefaultTokenProviders();
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
		builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
		builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
			options.SignIn.RequireConfirmedAccount = false;})
				.AddDefaultTokenProviders()
				.AddEntityFrameworkStores<AppDbContext>();
		// Adding Authentication
		builder.Services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
		})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
	options.SaveToken = true;
	options.RequireHttpsMetadata = false;
	options.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuer = true,
		ValidateAudience = true,
        ValidAudience =configuration["JWT:ValidAudience"],
		ValidIssuer = configuration["JWT:ValidIssuer"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
	};
});
	

			builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();
		var app = builder.Build();

		if(app.Environment.IsDevelopment())
		{
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI();
		}
		app.UseHttpsRedirection();

		app.UseStaticFiles();

		app.UseCors("AllowCORS");

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}
