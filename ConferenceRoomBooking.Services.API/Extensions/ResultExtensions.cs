using ConferenceRoomBooking.Bll.Exceptions;
using ConferenceRoomBooking.Bll.Responces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomBooking.Services.API.Extensions
{
    public static class ResultExtensions
    {
        public static ActionResult ToActionResult<T>(this Result<T> result, int statusCode = StatusCodes.Status200OK, string location = null)
        {
            return result.Match<ActionResult>(
                success =>
                {
                    if (success == null || (success is IEnumerable<object> enumerable && !enumerable.Any()))
                    {
                        return new NoContentResult();
                    }

                    return statusCode switch
                    {
                        StatusCodes.Status201Created => new CreatedResult(location, success),
                        _ => new OkObjectResult(success)
                    };
                },
                exception =>
                {
                    return exception switch
                    {
                        NotFoundException => new NotFoundObjectResult(new { Error = exception.Message }),
                        ValidationModelException validationEx => new BadRequestObjectResult(new
                        {
                            Errors = validationEx.Errors.Select(e => e.ErrorMessage).ToList()
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
                    return statusCode switch
                    {
                        StatusCodes.Status201Created => new CreatedResult(),
                        StatusCodes.Status204NoContent => new NoContentResult(),
                        _ => new StatusCodeResult(statusCode),
                    };
                },
                exception =>
                {
                    return exception switch
                    {
                        NotFoundException => new NotFoundObjectResult(new { Error = exception.Message }),
                        ValidationModelException validationEx => new BadRequestObjectResult(new
                        {
                            Errors = validationEx.Errors.Select(e => e.ErrorMessage).ToList()
                        }),
                        _ => new StatusCodeResult(StatusCodes.Status500InternalServerError),
                    };
                });
        }
    }
}
