using AutoRespect.Infrastructure.ErrorHandling;
using System;

namespace AutoRespect.AuthorizationServer.Design.Primitives
{
    public class Password
    {
        public string Value { get; private set; }
        private Password(string value) => Value = value;

        public static Result<Password> Create(string password)
        {
            if (String.IsNullOrEmpty(password))
                return new PasswordCantBeNullOrEmpty();

            return new Password(password);
        }

        // FOR DAPPER QUERIES
        public static implicit operator Password(string password) => new Password(password);
    }

    public class PasswordCantBeNullOrEmpty : Error
    {
        public PasswordCantBeNullOrEmpty() : base("D9A8DC67-60FB-4508-BD8F-E70DC5981EC1", "Password can't be empty or null")
        {
        }
    }
}
