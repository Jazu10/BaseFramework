using Backend.Core.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Backend.Core.Common.ErrorHandlers
{
    public static class ErrorHandler
    {
        public static List<Error> GetErrorAsync(List<Error> errors, string error, int statuscode, string? errorType)
        {
            if (errors == null)
            {
                errors = new List<Error>();
            }

            errors.Add(new Error
            {
                StatusCode = statuscode,
                ErrorMessage = error,
            });

            return errors;
        }

        public static List<Error> GetErrorAsync(List<Error> errors, IdentityError error, int statuscode, string? errorType)
        {
            if (errors == null)
            {
                errors = new List<Error>();
            }

            errors.Add(new Error()
            {
                StatusCode = statuscode,
                ErrorMessage = error.Description,
                InnerException = null,
                ErrorType = errorType,
            });

            return errors;
        }

        public static List<Error> GetErrorAsync(List<Error> errors, Exception error, int statuscode, string? errorType)
        {
            if (errors == null)
            {
                errors = new List<Error>();
            }

            errors.Add(new Error()
            {
                StatusCode = statuscode,
                ErrorMessage = error.Message,
                InnerException = error?.InnerException?.Message,
                ErrorType = errorType,
            });

            return errors;
        }
    }
}

        //public static List<Error> GetErrorAsync<TError>(List<Error> errors, TError error, int statuscode, string? errorType)
        //{
        //    if (errors == null)
        //    {
        //        errors = new List<Error>();
        //    }

        //    errors.Add(new Error()
        //    {
        //        StatusCode = statuscode,
        //        ErrorMessage = err.Description,
        //        ErrorType = errorType,
        //    });

        //    return errors;
        //}



        //public static List<Error> GetErrorAsync<TError>(List<Error> errors, TError error, int statuscode, string? errorType)
        //{
        //    if (errors == null)
        //    {
        //        errors = new List<Error>();
        //    }

        //    dynamic err = error;

        //    switch (typeof(TError).Name.ToString())
        //    {
        //        case "String":
        //            errors.Add(new Error
        //            {
        //                StatusCode = statuscode,
        //                ErrorMessage = err,
        //            });
        //            break;

        //        case "List":
        //            foreach (var item in err)
        //            {
        //                errors.Add(new Error()
        //                {
        //                    StatusCode = statuscode,
        //                    ErrorMessage = item.ErrorMessage,
        //                    InnerException = item.InnerException,
        //                    ErrorType = errorType,
        //                });
        //            }
        //            break;

        //        case "Exception":

        //                errors.Add(new Error()
        //                {
        //                    StatusCode = statuscode,
        //                    ErrorMessage = item.Message,
        //                    InnerException = item.InnerException.Message,
        //                    ErrorType = errorType,
        //                });
        //            break;

        //        case "IdentityError":
        //            errors.Add(new Error()
        //            {
        //                StatusCode = statuscode,
        //                ErrorMessage = err.Description,
        //                ErrorType = errorType,
        //            });
        //            break;
        //    }
        //    return errors;
        //}