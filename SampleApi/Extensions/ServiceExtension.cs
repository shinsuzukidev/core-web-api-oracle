using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;


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
        public static void ConfigurationApiVersioning(this IServiceCollection services) 
        {
            services.AddApiVersioning(options => {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
            });
        }
    }
}
