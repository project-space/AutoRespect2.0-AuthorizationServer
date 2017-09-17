using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using System;

namespace AutoRespect.AuthorizationServer.Design.Primitives
{
    public class UserPassword
    {
        public string Value { get; private set; }
        private UserPassword(string value) => Value = value;

        public static Result<ErrorType, UserPassword> Create(string password)
        {
            if (String.IsNullOrEmpty(password))
                return ErrorType.UserPasswordCantBeNullOrEmpty;

            return new UserPassword(password);
        }

        // FOR DAPPER QUERIES
        public static implicit operator UserPassword(string password) => new UserPassword(password);
    }
}
