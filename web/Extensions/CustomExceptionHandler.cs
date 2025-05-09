using AGI.Morn.Application.Common.Exceptions;
using AGI.Morn.Application.Common.Models;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;
using System.Xml;

namespace Morn_Agi.Extensions
{

    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;

        public CustomExceptionHandler()
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new()
            {
                { typeof(AccessViolationException), HandleNotImplementedException },
                { typeof(AggregateException), HandleNotImplementedException },
                { typeof(ArgumentException), HandleNotImplementedException },
                { typeof(ArithmeticException), HandleNotImplementedException },
                { typeof(ArgumentNullException), HandleNotImplementedException },
                { typeof(ArgumentOutOfRangeException), HandleNotImplementedException },
                { typeof(ArrayTypeMismatchException), HandleNotImplementedException },
                { typeof(AbandonedMutexException), HandleNotImplementedException },
                { typeof(AppDomainUnloadedException), HandleNotImplementedException },
                { typeof(ApplicationException), HandleNotImplementedException },
                { typeof(BackgroundServiceExceptionBehavior), HandleNotImplementedException },
                { typeof(BadHttpRequestException), HandleNotImplementedException },
                { typeof(BadImageFormatException), HandleNotImplementedException },
                { typeof(BarrierPostPhaseException), HandleNotImplementedException },
                { typeof(CannotUnloadAppDomainException), HandleNotImplementedException },
                { typeof(ContextMarshalException), HandleNotImplementedException },
                { typeof(DatabaseDeveloperPageExceptionFilterServiceExtensions), HandleNotImplementedException },
                { typeof(DataMisalignedException), HandleNotImplementedException },
                { typeof(DeveloperExceptionPageExtensions), HandleNotImplementedException },
                { typeof(DeveloperExceptionPageMiddleware), HandleNotImplementedException },
                { typeof(DeveloperExceptionPageOptions), HandleNotImplementedException },
                { typeof(DirectoryNotFoundException), HandleNotImplementedException },
                { typeof(DivideByZeroException), HandleNotImplementedException },
                { typeof(DllNotFoundException), HandleNotImplementedException },
                { typeof(DriveNotFoundException), HandleNotImplementedException },
                { typeof(DuplicateWaitObjectException), HandleNotImplementedException },
                { typeof(EndOfStreamException), HandleNotImplementedException },
                { typeof(EntryPointNotFoundException), HandleNotImplementedException },
                { typeof(FieldAccessException), HandleNotImplementedException },
                { typeof(FileLoadExceptionContext), HandleNotImplementedException },
                { typeof(FileNotFoundException), HandleNotImplementedException },
                { typeof(HostAbortedException), HandleNotImplementedException },
                { typeof(HttpIOException), HandleNotImplementedException },
                { typeof(HttpProtocolException), HandleNotImplementedException },
                { typeof(HttpRequestException), HandleNotImplementedException },
                { typeof(FormatException), HandleNotImplementedException },
                //{ typeof(ForbiddenAccessException), HandleForbiddenAccessException },
                { typeof(IndexOutOfRangeException), HandleNotImplementedException },
                { typeof(InvalidOperationException), HandleNotImplementedException },
                { typeof(InsufficientExecutionStackException), HandleNotImplementedException },
                { typeof(InsufficientMemoryException), HandleNotImplementedException },
                { typeof(InternalBufferOverflowException), HandleNotImplementedException },
                { typeof(InvalidCastException), HandleNotImplementedException },
                { typeof(InvalidDataException), HandleNotImplementedException },
                { typeof(InvalidProgramException), HandleNotImplementedException },
                { typeof(InvalidTimeZoneException), HandleNotImplementedException },
                { typeof(IOException), HandleNotImplementedException },
                { typeof(JsonException), HandleNotImplementedException },
                { typeof(KeyNotFoundException), HandleNotImplementedException },
                { typeof(MemberAccessException), HandleNotImplementedException },
                { typeof(MethodAccessException), HandleNotImplementedException },
                { typeof(MissingFieldException), HandleNotImplementedException },
                { typeof(MissingMemberException), HandleNotImplementedException },
                { typeof(MissingMethodException), HandleNotImplementedException },
                { typeof(MulticastNotSupportedException), HandleNotImplementedException },
                { typeof(NotFiniteNumberException), HandleNotImplementedException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(NotImplementedException), HandleNotImplementedException },
                { typeof(NotSupportedException), HandleNotImplementedException },
                { typeof(NullReferenceException), HandleNotImplementedException },
                { typeof(OverflowException), HandleNotImplementedException },
                { typeof(OutOfMemoryException), HandleNotImplementedException },
                { typeof(ObjectDisposedException), HandleNotImplementedException },
                { typeof(OperationCanceledException), HandleNotImplementedException },
                { typeof(PathTooLongException), HandleNotImplementedException },
                { typeof(PlatformNotSupportedException), HandleNotImplementedException },
                { typeof(RankException), HandleNotImplementedException },
                { typeof(RouteCreationException), HandleNotImplementedException },
                { typeof(SemaphoreFullException), HandleNotImplementedException },
                { typeof(StackOverflowException), HandleNotImplementedException },
                { typeof(SynchronizationLockException), HandleNotImplementedException },
                { typeof(SystemException), HandleNotImplementedException },
                { typeof(TaskCanceledException), HandleNotImplementedException },
                { typeof(TaskSchedulerException), HandleNotImplementedException },
                { typeof(ThreadAbortException), HandleNotImplementedException },
                { typeof(ThreadInterruptedException), HandleNotImplementedException },
                { typeof(ThreadStartException), HandleNotImplementedException },
                { typeof(ThreadStateException), HandleNotImplementedException },
                { typeof(TimeZoneNotFoundException), HandleNotImplementedException },
                { typeof(TimeoutException), HandleNotImplementedException },
                { typeof(TypeInitializationException), HandleNotImplementedException },
                { typeof(TypeAccessException), HandleNotImplementedException },
                { typeof(TypeLoadException), HandleNotImplementedException },
                { typeof(TypeUnloadedException), HandleNotImplementedException },
                { typeof(UnauthorizedAccessException), HandleUnauthorizedAccessException },
                { typeof(UriFormatException), HandleNotImplementedException },
                { typeof(ValidationException), HandleValidationException },
                { typeof(WaitHandleCannotBeOpenedException), HandleNotImplementedException },
                { typeof(XmlException), HandleNotImplementedException },
            };
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var exceptionType = exception.GetType();

            if (_exceptionHandlers.ContainsKey(exceptionType))
            {
                await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
                return true;
            }

            return false;
        }

        private async Task HandleValidationException(HttpContext httpContext, Exception ex)
        {
            var exception = (ValidationException)ex;
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                await httpContext.Response.WriteAsJsonAsync(new DataResponse<IDictionary<string, string[]>>()
                {
                    IsSuccess = false,
                    StatusCode = (HttpStatusCode?)StatusCodes.Status404NotFound,
                    ResponseMessage = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                    ResponseData = exception.Errors
                });
            }
        }

        private async Task HandleNotFoundException(HttpContext httpContext, Exception ex)
        {
            var exception = (NotFoundException)ex;

            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                await httpContext.Response.WriteAsJsonAsync(new DataResponse<string>()
                {
                    IsSuccess = false,
                    StatusCode = (HttpStatusCode?)StatusCodes.Status404NotFound,
                    ResponseMessage = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                    ResponseData = contextFeature.Error.Message
                });
            }
        }

        private async Task HandleUnauthorizedAccessException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                await httpContext.Response.WriteAsJsonAsync(new DataResponse<string>()
                {
                    IsSuccess = false,
                    StatusCode = (HttpStatusCode?)StatusCodes.Status401Unauthorized,
                    ResponseMessage = "https://tools.ietf.org/html/rfc7235#section-3.1",
                    ResponseData = contextFeature.Error.Message
                });
            }
        }

        private async Task HandleForbiddenAccessException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                await httpContext.Response.WriteAsJsonAsync(new DataResponse<string>()
                {
                    IsSuccess = false,
                    StatusCode = (HttpStatusCode?)StatusCodes.Status403Forbidden,
                    ResponseMessage = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                    ResponseData = contextFeature.Error.Message
                });
            }
        }

        private async Task HandleNotImplementedException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;
            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                await httpContext.Response.WriteAsJsonAsync(new DataResponse<string>()
                {
                    IsSuccess = false,
                    StatusCode = (HttpStatusCode?)StatusCodes.Status501NotImplemented,
                    ResponseMessage = "https://tools.ietf.org/html/rfc7231#section-6.6.2",
                    ResponseData = contextFeature.Error.Message
                });
            }
        }

        private async Task HandleSqliteException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;
            var contextFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                await httpContext.Response.WriteAsJsonAsync(new DataResponse<string>()
                {
                    IsSuccess = false,
                    StatusCode = (HttpStatusCode?)StatusCodes.Status501NotImplemented,
                    ResponseMessage = "https://tools.ietf.org/html/rfc7231#section-6.6.2",
                    ResponseData = contextFeature.Error.Message
                });
            }
        }
    }
}
