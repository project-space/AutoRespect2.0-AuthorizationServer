using AutoRespect.AuthorizationServer.Design.Primitives;

namespace AutoRespect.AuthorizationServer.Design.Models
{
    public class Credentials
    {
        public Login Login { get; set; }
        public Password Password { get; set; }
    }
}