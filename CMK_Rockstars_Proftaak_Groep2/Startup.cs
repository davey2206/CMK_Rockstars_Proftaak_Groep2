using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Rsk.AspNetCore.Authentication.Saml2p;

namespace CMK_Rockstars_Proftaak_Groep2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "cookie";
                options.DefaultChallengeScheme = "saml2";
            }).AddCookie("cookie")
            .AddSaml2p("saml2", options =>
            {
                options.Licensee = "DEMO";
                options.LicenseKey = "eyJTb2xkRm9yIjowLjAsIktleVByZXNldCI6NiwiU2F2ZUtleSI6ZmFsc2UsIkxlZ2FjeUtleSI6ZmFsc2UsIlJlbmV3YWxTZW50VGltZSI6IjAwMDEtMDEtMDFUMDA6MDA6MDAiLCJhdXRoIjoiREVNTyIsImV4cCI6IjIwMjItMDUtMDdUMDI6NTI6NTEuMTI2OTk3NiswMDowMCIsImlhdCI6IjIwMjItMDQtMDdUMDI6NTI6NTEiLCJvcmciOiJERU1PIiwiYXVkIjoyfQ==.f41bmB1kRLBSB8wgUGZYJfwI8AcmFjf7sMfJ1HBSBcHJ4ENGb+CJWDQmjX2doq3iqJDsC6ixZEYQn8vL/6c9AtdoV8fwt/dZp1HyWppJFCnV2f92QWsN50xsIuOjPUlRASPib5+67x+dOwyTM890+Wmgdh4ZX+XW/UKcGsd7J+SDUQIeic7EwXSoz0HUybd5p56GhEjGgMB4S9vjm6WkAKC6GT952HSjVbIYea3+BMNGdwO5m3DBD1AsRCCvvQh2I47OnhuThU1xYXRfmQFzkB5Ex//pFsoCmmxLji2hnPafc2DF0F/XVzHAyBZ2eWAgxYsC6bC4P2qaYwh29AmjxqD+eD/qshNhbxQAA09RO3+X6X9yHazQpXfDofWO/utfgM0jQ7/FBMxyPtkjtFOQfDjH04/l/xW3GfY4fvC/bxvRdJ5OchT81XEO8kgq4x3PnRkfwK8QteXOk7sdol8XdBlSn74S9Uoezlasc0tvmGTDDw3b1aYbL2sL0mHaR4HG2gdkM/UQlhdF7oJSJfYar+jt+lkEz5Ld38yo1Ym8A7QqOHHehhek81KgnphVRbs3q4dy+jw9ClwYSIOnX7YTQcKISaz2KhQxgk9MD4cSMG3qmAfkuvde/tBnZMMExrwoj2f6P72+/lUgA8QUNTIQFdKx2BKOrwHOo9wDEEYwKLY=";

                options.CallbackPath = "/signin-saml";
                options.SignInScheme = "cookie";

                options.ServiceProviderOptions = new SpOptions()
                {
                    EntityId = "https://localhost:5001/saml"
                };

                options.IdentityProviderMetadataAddress = "https://login.microsoftonline.com/0e199aab-b45d-4371-aecf-971d3ae0f357/federationmetadata/2007-06/federationmetadata.xml?appid=6d06b9d0-64eb-4c7f-8f8a-d8df1b6f11ba";
                options.TimeComparisonTolerance = 120;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
