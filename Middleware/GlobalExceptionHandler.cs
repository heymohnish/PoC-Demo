using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PoC_Demo.DTO;

namespace PoC_Demo.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
     
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context,Exception exception,CancellationToken cancellationToken)
        { 

                _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

                var errorResponse = new ErrorResponseDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Error = "Server Error",
                    ErrorDescription = exception.Message,
                };

                context.Response.StatusCode =StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsJsonAsync(errorResponse);
                return true;
            }
        }
    }

