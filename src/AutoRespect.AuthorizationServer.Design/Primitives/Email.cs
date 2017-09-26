using AutoRespect.Infrastructure.ErrorHandling;
using System;

namespace AutoRespect.AuthorizationServer.Design.Primitives
{
    public class Email
    {
        public string Value { get; private set; }
        private Email(string value) => Value = value;
        
        public static Result<Email> Create(string email)
        {
            if (String.IsNullOrEmpty(email))
                return new EmailCantBeNullOrEmpty();
            
            // TODO: REGEX FOR EMAIL
            //     return ErrorType.WrongEmailFormat;

            return new Email(email);
        }

        //FOR DAPPER QUERIES
        public static implicit operator Email(string login) => new Email(login);
    }

    public class EmailCantBeNullOrEmpty : Error
    {
        public EmailCantBeNullOrEmpty() : base("9A55078E-0F04-48F2-8251-F3E931C74C81", "Email can't be empty or null")
        {
        }
    }

    public class EmailHasWrongFormat : Error
    {
        public EmailHasWrongFormat() : base("895191AE-57AB-45CA-8BF8-A7EECA593734", "Email has wrong format ... [todo: description of format]")
        {
        }
    }
}
