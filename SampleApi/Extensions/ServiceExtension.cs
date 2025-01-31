using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using SampleApi.Util.Config;
using System.Text.Json.Serialization;
using System.Text.Json;
using SampleApi.Filter;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SampleApi.Extensions
{
    /// <summary>
    /// サービス拡張クラス
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// APIバージョン管理
        /// </summary>
        public static void ConfigureApiVersioning(this IServiceCollection services) 
        {
            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            });
        }

        /// <summary>
        /// カステム設定
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureMySettings(this IServiceCollection services)
        {
            services.AddSingleton<IMyConfig, MyConfig>();
        }

        /// <summary>
        /// APIの振る舞い設定
        /// </summary>
        public static void ConfigureApiBehaviorOptions(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                // 自動的な400の動作を無効にする（リクエスト検証）
                // グローバルフィルターで検証エラーの結果を対応する
                options.SuppressModelStateInvalidFilter = true;
            });
        }


        public static void ConfigureFilter(this IServiceCollection services) 
        {
            services.AddControllers(config =>
            {
                config.Filters.Add<GlobalActionFilter>();
                // グローバルの例外はフィルターが使いやすい
                // https://www.herlitz.io/2019/05/05/global-exception-handling-asp.net-core/
                config.Filters.Add<GlobalExceptionFilter>();
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SampleApi",
                    Description = "サンプルAPI",
                });
            });
        }
        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie((options) =>
                {
                    options.SlidingExpiration = true;
                    options.Events.OnRedirectToLogin = cxt =>
                    {
                        cxt.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToAccessDenied = cxt =>
                    {
                        cxt.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return Task.CompletedTask;
                    };
                    options.Events.OnRedirectToLogout = cxt => Task.CompletedTask;
                });
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole(new[] { "admin" }));
            });
        }

    }
}
