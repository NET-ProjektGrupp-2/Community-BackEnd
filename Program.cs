namespace Community_BackEnd;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

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
