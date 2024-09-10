namespace ElenaMartiniKreationen.Server.Response
{
    public class LoginDtoResponse : BaseResponse
    {
        public string Token { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public ICollection<string> Roles { get; set; } = default!;
    }
}
