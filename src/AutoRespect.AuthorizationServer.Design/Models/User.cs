﻿using AutoRespect.AuthorizationServer.Design.Primitives;

namespace AutoRespect.AuthorizationServer.Design.Models
{
    public class User
    {
        public int Id { get; set; }
        public Login Login { get; set; }
        public Password Password { get; set; }
    }
}
