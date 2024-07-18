using System.ComponentModel.DataAnnotations;

namespace ProjectToSocial.Server.Controllers
{
    public class UserQuery
    {
        [Required]
        public int Id { get; set; }
        public int? IdRole { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Минимальная длина строка 2 символа'")]
        [RegularExpression(@"^(?!.*'{2})(?!.*’{2})([\p{L}]+(['’][\p{L}]+)*)$", ErrorMessage = "Некорректное Имя")]
        public string? FirstName { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Минимальная длина строка 2 символа'")]
        [RegularExpression(@"^(?!.*'{2})(?!.*’{2})([\p{L}]+(['’][\p{L}]+)*)$", ErrorMessage = "Некорректное Имя")]
        public string? LastName { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Минимальная длина строка 2 символа'")]
        [RegularExpression(@"^(?!.*'{2})(?!.*’{2})([\p{L}]+(['’][\p{L}]+)*)$", ErrorMessage = "Некорректное Имя")]
        public string? Family { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный Email")]
        public string? Email { get; set; }

        [StringLength (15, MinimumLength = 8, ErrorMessage = "Пароль должен быть от 3 до 15 символов")]
        [RegularExpression (@"^[a-zA-Z0-9!@#$%^&*().]+$", ErrorMessage = "Пароль не прошел проверку")]
        public string? Password { get; set; }

        [Compare ("Password", ErrorMessage = "Пароли не совпадают")]
        public string? PasswordConfirm { get; set; }

        public string? Avatar { get; set; }
    }
}
