using ConferenceRoomBooking.Application.Exceptions;
using ConferenceRoomBooking.Application.Responces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.API.Extensions
{
    public static class ResultExtensions
    {
        public static ActionResult ToActionResult<T>(this Result<T> result, int statusCode = StatusCodes.Status200OK, string location = null)
        {
            return result.Match<ActionResult>(
                success =>
                {
                    if (statusCode == StatusCodes.Status200OK)
                    {
                        return new OkObjectResult(success);
                    }
                    else if (statusCode == StatusCodes.Status201Created)
                    {
                        return new CreatedResult(location, success);
                    }
                    else if (statusCode == StatusCodes.Status204NoContent ||
                        success == null || (success is IEnumerable<object> enumerable && !enumerable.Any()))
                    {
                        return new NoContentResult();
                    }

                    return new ContentResult
                    {
                        StatusCode = statusCode,
                        Content = success == null ? null : success.ToString(),
                        ContentType = "application/json"
                    };
                },
                exception =>
                {
                    return exception switch
                    {
                        NotFoundException => new NotFoundObjectResult(new { Error = exception.Message }),
                        ValidationModelException validationEx => new BadRequestObjectResult(new
                        {
                            Errors = validationEx.Errors
                                .GroupBy(e => e.PropertyName)
                                .ToDictionary(
                                    g => g.Key,
                                    g => g.Select(e => e.ErrorMessage).ToArray()
                                )
                        }),
                        _ => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                    };
                });
        }

        public static ActionResult ToActionResult(this Result result, int statusCode = StatusCodes.Status200OK)
        {
            return result.Match<ActionResult>(
                () =>
                {
                    if (statusCode == StatusCodes.Status204NoContent)
                    {
                        return new NoContentResult();
                    }

                    return new ContentResult
                    {
                        StatusCode = statusCode,
                        Content = null,
                        ContentType = "application/json"
                    };
                },
                exception =>
                {
                    return exception switch
                    {
                        NotFoundException => new NotFoundObjectResult(new { Error = exception.Message }),
                        ValidationModelException validationEx => new BadRequestObjectResult(new
                        {
                            Errors = validationEx.Errors
                                .GroupBy(e => e.PropertyName)
                                .ToDictionary(
                                    g => g.Key,
                                    g => g.Select(e => e.ErrorMessage).ToArray()
                                )
                        }),
                        _ => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                    };
                });
        }
    }
}
