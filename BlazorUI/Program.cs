using ElectionAppLibrary.DataAccess;
using ElectionAppLibrary.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddSingleton<LoginService>();
            builder.Services.AddSingleton<SearchService>();
            builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
			builder.Services.AddTransient<IUserData, UserData>();
            builder.Services.AddTransient<IRepAPIRequests, RepAPIRequests>();
            builder.Services.AddTransient<IRepData, RepData>();
			builder.Services.AddTransient<IVoterInfoAPI, VoterInfoAPI>();
			builder.Services.AddHttpClient("RepByDiv", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://civicinfo.googleapis.com/civicinfo/v2/representatives/");
            });
            builder.Services.AddHttpClient("VoterInfo", httpClient =>
            {
                httpClient.BaseAddress = new Uri("https://civicinfo.googleapis.com/civicinfo/v2/voterinfo");
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
