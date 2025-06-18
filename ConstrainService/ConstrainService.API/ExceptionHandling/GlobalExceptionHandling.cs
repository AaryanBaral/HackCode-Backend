using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Diagnostics;
using System.Net;
using Confluent.Kafka;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;

namespace ConstrainService.API.ExceptionHandling
{
    public class GlobalExceptionHandling(ILogger<GlobalExceptionHandling> logger) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandling> _logger = logger;

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
            _logger.LogError(exception,
                "Could not process a request on machine {MachineName}, TraceId:{TraceId}",
                Environment.MachineName,
                traceId);

            var (statusCode, title, errorCode) = MapException(exception, context);

            await Results.Problem(
                title: title,
                statusCode: statusCode,
                extensions: new Dictionary<string, object?>
                {
                { "traceId", traceId },
                { "errorCode", errorCode }
                }
            ).ExecuteAsync(context);
            return true;
        }

        private static (int statusCode, string title, string errorCode) MapException(Exception exception, HttpContext context)
        {
            if (context.Items.TryGetValue("ErrorCode", out var errorCodeObj) &&
                context.Items.TryGetValue("ErrorMessage", out var errorMessageObj))
            {
                string errorCode = errorCodeObj?.ToString() ?? "AUTH_ERROR";
                string message = errorMessageObj?.ToString() ?? "Authentication error occurred";

                return errorCode switch
                {
                    "TOKEN_EXPIRED" => ((int)HttpStatusCode.Unauthorized, message, "TOKEN_EXPIRED"),
                    "UNAUTHORIZED" => ((int)HttpStatusCode.Unauthorized, message, "UNAUTHORIZED"),
                    "FORBIDDEN" => ((int)HttpStatusCode.Forbidden, message, "FORBIDDEN"),
                    "AUTH_FAILED" => ((int)HttpStatusCode.Unauthorized, message, "AUTH_FAILED"),
                    _ => ((int)HttpStatusCode.Unauthorized, message, errorCode)
                };
            }
            return exception switch
            {
                ConsumeException ex when (ex.Error.Code == ErrorCode.UnknownTopicId) =>
         ((int)HttpStatusCode.BadRequest,
         $"Kafka topic not available: {ex.Error.Reason}",
         "KAFKA_TOPIC_UNAVAILABLE"),
                ConsumeException ex =>
                    ((int)HttpStatusCode.ServiceUnavailable,
                    $"Kafka broker unavailable: {ex.Message}",
                    "KAFKA_UNAVAILABLE"),
                ArgumentOutOfRangeException => ((int)HttpStatusCode.BadRequest, exception.Message, "ARG_OUT_OF_RANGE"),
                ArgumentNullException => ((int)HttpStatusCode.BadRequest, exception.Message, "ARG_NULL"),
                ArgumentException => ((int)HttpStatusCode.BadRequest, exception.Message, "ARG_INVALID"),
                UnauthorizedAccessException => ((int)HttpStatusCode.Forbidden, exception.Message, "UNAUTHORIZED"),
                InvalidOperationException => ((int)HttpStatusCode.BadRequest, exception.Message, "INVALID_OPERATION"),
                TimeoutException => ((int)HttpStatusCode.RequestTimeout, "Request timed out", "TIMEOUT"),
                DbUpdateException => ((int)HttpStatusCode.BadRequest, "Database update failed", "DB_UPDATE"),
                InvalidCastException => ((int)HttpStatusCode.BadRequest, exception.Message, "INVALID_CAST"),
                FormatException => ((int)HttpStatusCode.BadRequest, exception.Message, "FORMAT_ERROR"),
                KeyNotFoundException => ((int)HttpStatusCode.NotFound, exception.Message, "NOT_FOUND"),
                AuthenticationFailureException => ((int)HttpStatusCode.Unauthorized, "Authentication failed", "AUTH_FAILED"),
                ValidationException => ((int)HttpStatusCode.BadRequest, exception.Message, "VALIDATION_ERROR"),
                DuplicateNameException => ((int)HttpStatusCode.BadRequest, exception.Message, "DUPLICATE_NAME"),
                SecurityTokenExpiredException => ((int)HttpStatusCode.Unauthorized, "Token expired", "TOKEN_EXPIRED"),
                NullReferenceException => ((int)HttpStatusCode.InternalServerError, exception.Message, "NULL_REFERENCE"),
                MySqlException sqlEx => HandleMySqlException(sqlEx),
                _ => ((int)HttpStatusCode.InternalServerError, "An unexpected error occurred", "INTERNAL_SERVER_ERROR"),
            };
        }

        private static (int statusCode, string title, string errorCode) HandleMySqlException(MySqlException ex)
        {
            return ex.Number switch
            {
                1062 => ((int)HttpStatusCode.BadRequest, "Duplicate entry, unique constraint violation", "UNIQUE_CONSTRAINT"),
                1048 => ((int)HttpStatusCode.BadRequest, "Cannot insert NULL value", "NULL_VALUE"),
                1451 => ((int)HttpStatusCode.BadRequest, "Cannot delete or update due to foreign key constraint", "FOREIGN_KEY"),
                1216 => ((int)HttpStatusCode.BadRequest, "Foreign key constraint fails", "FOREIGN_KEY"),
                1217 => ((int)HttpStatusCode.BadRequest, "Cannot delete or update due to foreign key constraint", "FOREIGN_KEY"),
                1205 => ((int)HttpStatusCode.Conflict, "Lock wait timeout exceeded; deadlock detected", "DEADLOCK"),
                1049 => ((int)HttpStatusCode.ServiceUnavailable, "Unknown database", "UNKNOWN_DATABASE"),
                1045 => ((int)HttpStatusCode.Unauthorized, "Access denied for user", "DB_AUTH_FAILED"),
                2003 => ((int)HttpStatusCode.ServiceUnavailable, "Cannot connect to MySQL server", "SERVER_CONNECTION"),
                2013 => ((int)HttpStatusCode.RequestTimeout, "SQL query timeout", "QUERY_TIMEOUT"),
                _ => ((int)HttpStatusCode.InternalServerError, "Database error occurred", "DB_ERROR"),
            };
        }
    }
}