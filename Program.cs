namespace Community_BackEnd;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);


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

		app.UseDefaultFiles();
		app.UseStaticFiles();

		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();


		app.MapControllers();

		app.Run();
	}
}
