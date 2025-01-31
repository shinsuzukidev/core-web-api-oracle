using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SampleApi.Models.Account
{
    [CustomValidation(typeof(LoginUserRequest), "CheckNameAndPassBakaWord")]
    public class LoginUserRequest 
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} length cant'be more than {1}.")]
        [CustomValidation(typeof(LoginUserRequest), "CheckNgWord")]
        public string? LoginName { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} length cant'be more than {1}.")]
        public string? Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required")]
        [StringLength(10, ErrorMessage = "{0} length cant'be more than {1}.")]
        public string? RoleType { get; set; }

        /// <summary>
        /// 独自検証
        /// </summary>
        public static ValidationResult CheckNgWord(string? name)
        {
            if (name?.ToLower().Contains("ngword") ?? false)
            {
                return new ValidationResult("NGワードあり");
            }

            return ValidationResult.Success!;
        }

        /// <summary>
        /// 複数プロパティ検証
        /// </summary>
        public static ValidationResult CheckNameAndPassBakaWord(LoginUserRequest? loginUserRequest)
        {
            if ((loginUserRequest?.LoginName?.ToLower().Contains("bakaword") ?? false)
                    && (loginUserRequest.Password?.ToLower().Contains("bakaword") ?? false))
            {
                return new ValidationResult("NGワードあり", new List<string> { "loginName, Password" });
            }

            return ValidationResult.Success!;
        }
    }
}
