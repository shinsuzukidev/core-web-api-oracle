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
            builder.Services.ConfigureAuthentication();         // �F��
            builder.Services.ConfigureAuthorization();          // �F��
            builder.Services.ConfigureApiVersioning();          // API�o�[�W���j���O
            builder.Services.ConfigureApiBehaviorOptions();     // API�̐U�镑��
            builder.Services.ConfigureFilter();                 // �t�B���^�[
            builder.Services.ConfigureMySettings();             // �ݒ�t�@�C��
            builder.Services.ConfigureSwagger();                // Swagger�ݒ�



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();                       // Swagger�~�h���E�F�A��ǉ�
                app.UseSwaggerUI();                     // �ÓI�t�@�C���~�h���E�F�A��L����
                app.UseDeveloperExceptionPage();        // �J���җ�O�y�[�W��L����
            } 
            else
            {
                app.UseHsts();                          // HTTP�̑����HTTPS���g�p����悤�w��
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();                  // �����I�� HTTP �v���� HTTPS �փ��_�C���N�g���܂�
            app.UseAuthentication();                    // �F��L����
            app.UseAuthorization();                     // �F�؂�L����

            app.MapControllers();                       // �������[�e�B���O �R���g���[���[���}�b�v����܂�
            app.Run();
        }
    }
}
