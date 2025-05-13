using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.Common;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Application.Common.Result
{
    public static class ResultExtensions
    {
        public static TReturn Match<TData, TReturn>(
            this Result result,
            Func<TReturn> onSuccess,
            Func<Error, TReturn> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result.Error);
        }



        public static IResult ToProblemDetails(this Result result)
        {
            if (result.IsSuccess)
            {
                throw new InvalidOperationException();
            }
            var errorList = new List<object>();

            // Case 1: Handle if result contains a list of errors in result.Errors
            if (result.Errors != null && result.Errors.Any())
            {
                errorList = result.Errors.Select(e => (object)new
                {
                    code = e.Code,
                    description = e.Description,
                    type = e.Type.ToString() // Convert the enum to a string
                }).ToList();
            }
            // Case 2: Handle if result contains a single error in result.Error
            else if (result.Error != null)
            {
                errorList.Add(new
                {
                    code = result.Error.Code,
                    description = result.Error.Description,
                    type = result.Error.Type.ToString() // Convert the enum to a string
                });
            }




            return Results.Problem(
        statusCode: GetStatusCode(result.Errors?.FirstOrDefault()?.Type ?? result.Error.Type), // Handle multiple errors
        title: GetTitle(result.Errors?.FirstOrDefault()?.Type ?? result.Error.Type),
        type: GetType(result.Errors?.FirstOrDefault()?.Type ?? result.Error.Type),
        extensions: new Dictionary<string, object?>
        {
            { "errors", errorList },
        }
    );

            static int GetStatusCode(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => StatusCodes.Status400BadRequest,
                    ErrorType.NotFound => StatusCodes.Status404NotFound,
                    ErrorType.Conflict => StatusCodes.Status409Conflict,
                    _ => StatusCodes.Status500InternalServerError
                };

            static string GetTitle(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => "Bad Request",
                    ErrorType.NotFound => "Not Found",
                    ErrorType.Conflict => "Conflict",
                    _ => "Server Failure"
                };

            static string GetType(ErrorType errorType) =>
                errorType switch
                {
                    ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",  // 400 Bad Request
                    ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",   // 404 Not Found
                    ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",   // 409 Conflict
                    _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"                     // 500 Internal Server Error
                };
        }

        public static IResult ToSuccessDetails(this Result result, string message)
        {
            if (!result.IsSuccess)
            {
                throw new InvalidOperationException("The operation was not successful.");
            }

            return result.Object == null
                ? Results.NoContent() // 204 No Content if no data is present
                : Results.Ok(
                    new Success<object>(
                        statusCode: StatusCodes.Status200OK,
                        title: "Operation Successful",
                        type: "https://tools.ietf.org/html/rfc7231#section-6.3.1", // 200 OK
                                                                                   //  data: result.Object, // use the Object property from the Result
                        extensions: new Dictionary<string, object?>
                        {
                    { "message", message },
                    { "data", result.Object }
                        }
                    )
                );
        }
    }
}
