using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRespect.AuthorizationServer.Design.ErrorHandling
{
    public enum ErrorType
    {
        UserNotFound = 1,
        UserLoginCantBeNullOrEmpty = 2,
        UserPasswordCantBeNullOrEmpty = 3,
        WrongLoginOrPassword = 4
    }
}
