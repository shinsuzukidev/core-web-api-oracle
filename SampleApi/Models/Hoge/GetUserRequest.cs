using System.ComponentModel.DataAnnotations;

namespace SampleApi.Models.Hoge
{
    [CustomValidation(typeof(GetUserRequest), "CheckGetUserRequest")]
    public class GetUserRequest
    {
        [Required(ErrorMessage="{0} is required.")]
        public int id { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(20, ErrorMessage = "{0} is length cant'be more than {1}.")]
        [CustomValidation(typeof(GetUserRequest), "CheckNgName")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is required.")]
        public int? Age { get; set; } = null;


        /// <summary>
        /// 独自検証
        /// </summary>
        public static ValidationResult CheckNgName(string name)
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
        public static ValidationResult CheckGetUserRequest(GetUserRequest? user)
        {
            if (user?.id < 0 || user?.id > 1000)
            {
                return new ValidationResult("Age or Id Error", new List<string> { "Age, Id" });
            }

            return ValidationResult.Success!;
        }

    }
}
