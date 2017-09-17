using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AutoRespect.AuthorizationServer.Design.ErrorHandling
{
    public class Result<TFailure, TSuccess>
    {
        public TFailure Failure { get; private set; }
        public TSuccess Success { get; private set; }
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;

        private Result() { }
        private Result(TFailure value)
        {
            Failure = value;
            IsSuccess = false;
        }

        private Result(TSuccess value)
        {
            Success = value;
            IsSuccess = true;
        }

        public static implicit operator Result<TFailure, TSuccess>(TFailure value) => new Result<TFailure, TSuccess>(value);
        public static implicit operator Result<TFailure, TSuccess>(TSuccess value) => new Result<TFailure, TSuccess>(value);

        public T Switch<T>(
            Func<TFailure, T> failureHandler,
            Func<TSuccess, T> successHandler
        ) => IsSuccess ?
                successHandler(Success) :
                failureHandler(Failure);
    }

    public static class ResultExt
    {
        public static Result<IEnumerable<TFailure>, (TSuccess0, TSuccess1)> Combine<TFailure, TSuccess0, TSuccess1> (
            Result<TFailure, TSuccess0> r1, 
            Result<TFailure, TSuccess1> r2)
        {
            var errors = new Collection<TFailure>();
            if (r1.IsFailure) errors.Add(r1.Failure);
            if (r2.IsFailure) errors.Add(r2.Failure);

            if (errors.Any()) return errors;
            else return (r1.Success, r2.Success);
        }
    }
}
