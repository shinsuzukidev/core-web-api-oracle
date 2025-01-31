using System.Text.Encodings.Web;
using SampleApi.Extensions;
using System.Text.Unicode;
using Microsoft.Extensions.WebEncoders;

namespace SampleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();                  //
            builder.Services.ConfigureAuthentication();         // 認可
            builder.Services.ConfigureAuthorization();          // 認証
            builder.Services.ConfigureApiVersioning();          // APIバージョニング
            builder.Services.ConfigureApiBehaviorOptions();     // APIの振る舞い
            builder.Services.ConfigureFilter();                 // フィルター
            builder.Services.ConfigureMySettings();             // 設定ファイル
            builder.Services.ConfigureSwagger();                // Swagger設定



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();                       // Swaggerミドルウェアを追加
                app.UseSwaggerUI();                     // 静的ファイルミドルウェアを有効化
                app.UseDeveloperExceptionPage();        // 開発者例外ページを有効化
            } 
            else
            {
                app.UseHsts();                          // HTTPの代わりにHTTPSを使用するよう指示
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();                  // 強制的に HTTP 要求を HTTPS へリダイレクトします
            app.UseAuthentication();                    // 認可を有効化
            app.UseAuthorization();                     // 認証を有効化

            app.MapControllers();                       // 属性ルーティング コントローラーがマップされます
            app.Run();
        }
    }
}
