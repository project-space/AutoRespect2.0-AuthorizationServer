using AutoRespect.AuthorizationServer.Design.Primitives;

namespace AutoRespect.AuthorizationServer.Design.Models
{
    public class User
    {
        public int Id { get; set; }
        public UserLogin Login { get; set; }
        public UserPassword Password { get; set; }
    }
}
