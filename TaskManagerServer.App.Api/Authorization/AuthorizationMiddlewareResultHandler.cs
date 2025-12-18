using System.Net;
using System.Text.Encodings.Web;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagerServer.App.Api.Authorization;

/// <summary>
/// Переопределяет сообщение для 401/403 ответов API
/// </summary>
public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly Microsoft.AspNetCore.Authorization.Policy.AuthorizationMiddlewareResultHandler _defaultHandler = new();

    /// <summary>
    /// Обработать и переопределить сообщение для ответов 401/403
    /// </summary>
    /// <param name="next">следующий мидлваре</param>
    /// <param name="context">Http контекст</param>
    /// <param name="policy">Auth policy</param>
    /// <param name="authorizeResult">Результат авторизации/аутентификации</param>
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        switch (authorizeResult)
        {
            case { Challenged: true, Succeeded: false, Forbidden: false }:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync( JsonSerializer.Serialize(new ProblemDetails(),new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                }));
                return;
            case { Challenged: true, Succeeded: false, Forbidden: true }:
                context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails(),
                    new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                }));
                return;
            default:
                await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
                break;
        }
    }
}