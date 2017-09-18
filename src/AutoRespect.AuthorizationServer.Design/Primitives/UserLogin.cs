using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using System;

namespace AutoRespect.AuthorizationServer.Design.Primitives
{
    public class UserLogin
    {
        public string Value { get; private set; }
        private UserLogin(string value) => Value = value;
        
        public static Result<ErrorType, UserLogin> Create(string login)
        {
            if (String.IsNullOrEmpty(login))
                return ErrorType.UserLoginCantBeNullOrEmpty;

            return new UserLogin(login);
        }

        //FOR DAPPER QUERIES
        public static implicit operator UserLogin(string login) => new UserLogin(login);
    }
}
