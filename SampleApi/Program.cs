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
                //app.UseSwagger();                   // Swagger�~�h���E�F�A��ǉ�
                //app.UseSwaggerUI();                 // �ÓI�t�@�C���~�h���E�F�A��L����
                app.UseDeveloperExceptionPage();    // �J���җ�O�y�[�W��L����
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();           // �������[�e�B���O �R���g���[���[���}�b�v����܂�
            app.Run();
        }
    }
}
