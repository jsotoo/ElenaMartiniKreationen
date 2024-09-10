using System.ComponentModel.DataAnnotations;

namespace ElenaMartiniKreationen.Server.Request
{
    public class LoginDtoRequest
    {
        [Required]
        public string User { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
