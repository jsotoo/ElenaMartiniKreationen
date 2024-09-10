using System.ComponentModel.DataAnnotations;

namespace ElenaMartiniKreationen.Server.Request
{
    public class RegisterUserDto
    {
        [Required]
        public string FullName { get; set; } = default!;

        [Required]
        public string Address { get; set; } = default!;

        [Required]
        public string UserName { get; set; } = default!;

        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        public string Phone { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Compare(nameof(Password), ErrorMessage = "Las claves no coinciden")]
        public string ConfirmPassword { get; set; } = default!;
    }
}
