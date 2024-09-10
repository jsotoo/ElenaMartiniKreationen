#nullable disable

namespace ElenaMartiniKreationen.Server.Configuration
{
    public class AppSettings
    {

        public Jwt Jwt { get; set; }


    }


    public class Jwt
    {
        public string SecretKey { get; set; }
        public string Transmitter { get; set; }
        public string Audience { get; set; }
    }
}
