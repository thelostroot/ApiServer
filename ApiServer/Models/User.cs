using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiServer.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[a-z0-9_-]{3,16}$", ErrorMessage = "Логин должен состоять может из латинских букв, цифр и знака нижнего подчеркивания")]
        public string Login { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", ErrorMessage = "Пароль должен быть не менее 8 символов и содержать буквы и цифры")]
        public string Password { get; set; }

        public string Role { get; set; }

        public bool Confirmed { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email введен неверно!")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина имени должна быть больше 2 символов!")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть больше 2 символов!")]
        public string LastName { get; set; }
    
    }
}
