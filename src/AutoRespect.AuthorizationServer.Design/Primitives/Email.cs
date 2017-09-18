using AutoRespect.AuthorizationServer.Design.ErrorHandling;
using System;
using System.Linq;

namespace AutoRespect.AuthorizationServer.Design.Primitives
{
    public class Email
    {
        public string Value { get; private set; }
        private Email(string value) => Value = value;
        
        public static Result<ErrorType, Email> Create(string email)
        {
            if (String.IsNullOrEmpty(email))
                return ErrorType.UserLoginCantBeNullOrEmpty;
            
            // TODO: REGEX FOR EMAIL
            //     return ErrorType.WrongEmailFormat;


            return new Email(email);
        }

        //FOR DAPPER QUERIES
        public static implicit operator Email(string login) => new Email(login);
    }
}
