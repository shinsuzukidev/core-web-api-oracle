using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using SampleApi.Models.Account;

namespace SampleApi.Controllers.v1
{
    [ApiController]
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ApiControllerBase
    {
        static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private bool CheckLogin()
        {
            //ログイン認証判定はTrueを返す
            return true;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserRequest? loginUserRequest)
        {
            _logger.Debug(this.ActionInfo() + "login");

            if (this.CheckLogin())
            {
                // サインインに必要なプリンシパルを作る
                //var claims = new[] { new Claim(ClaimTypes.Name, user?.LoginName) };
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, loginUserRequest!.LoginName!));

                var role = loginUserRequest.RoleType switch
                {
                    "admin" => "admin",
                    "user" => "user",
                    "light_user" => "light_user",
                    _ => throw new Exception("login error.")
                };

                claims.Add(new Claim(ClaimTypes.Role, role));

                //claims.Add(new Claim(ClaimTypes.Role, "admin"));
                //claims.Add(new Claim(ClaimTypes.Role, "user"));
                //claims.Add(new Claim(ClaimTypes.Role, "light_user"));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // 認証クッキーをレスポンスに追加
                //await HttpContext.SignInAsync(principal);
                // cookieをブラウザで永続化
                await HttpContext.SignInAsync(principal, new AuthenticationProperties { IsPersistent = true });

                return Ok();
            }

            return BadRequest("ログイン認証 == false");
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            //_logger.LogDebug(this.ActionInfo() + "logout");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
