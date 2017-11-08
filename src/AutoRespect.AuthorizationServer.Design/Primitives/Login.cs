﻿using System;
using AutoRespect.Infrastructure.Errors.Design;

namespace AutoRespect.AuthorizationServer.Design.Primitives
{
    public class Login
    {
        public string Value { get; private set; }
        private Login(string value) => Value = value;

        public static R<Login> Create(string login)
        {
            if (String.IsNullOrEmpty(login))
                return new LoginCantBeNullOrEmpty();

            return new Login(login);
        }

        //FOR DAPPER QUERIES
        public static implicit operator Login(string login) => new Login(login);
    }

    public class LoginCantBeNullOrEmpty : E
    {
        public LoginCantBeNullOrEmpty() : base("25EECBF8-1CE0-4C17-A627-D4F17DCDB4BA", "Login cant be empty or null")
        {
        }
    }
}
