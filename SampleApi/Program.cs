namespace SampleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddApiVersioning();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                //app.UseSwagger();                   // Swaggerミドルウェアを追加
                //app.UseSwaggerUI();                 // 静的ファイルミドルウェアを有効化
                app.UseDeveloperExceptionPage();    // 開発者例外ページを有効化
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();           // 属性ルーティング コントローラーがマップされます
            app.Run();
        }
    }
}
