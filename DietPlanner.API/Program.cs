namespace DietPlanner.API;

using Microsoft.AspNetCore.Builder;
using DietPlanner.Services;
using DietPlanner.Services.Interfaces;
using DietPlanner.Services.Implementation;
using DietPlanner.API.MiddleWare;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();        
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddData(builder.Configuration);

        builder.Services.AddScoped<IMealTypeService, MealTypeService>();
        builder.Services.AddScoped<IDietService, DietService>();

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandler>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
