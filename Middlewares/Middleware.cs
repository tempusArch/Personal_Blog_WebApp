using System.Text;

namespace PersonalBlog.Middlewares;

public class BasicAuthMiddleware {
    private readonly RequestDelegate _next;
    private readonly string _username = "admin";
    private readonly string _password = "password";

    public BasicAuthMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context) {
        if (context.Request.Path.StartsWithSegments("/admin")) {
            if (!context.Request.Headers.ContainsKey("Authorization")) {
                await Dame(context);
                return;
            }

            var authHeader = context.Request.Headers["Authorization"].ToString();

            if (authHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase)) {
                var encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                var bubun = decodedUsernamePassword.Split(':');
                var username = bubun[0];
                var password = bubun[1];

                if (username == _username && password == _password) {
                    await _next(context);
                    return;
                }
            }

            await Dame(context);

        } else
            await _next(context);
    }

    #region helper methods
    private static Task Dame(HttpContext context) {
        context.Response.StatusCode = 401;
        context.Response.Headers["WWW-Authenticate"] = "Basic";
        return context.Response.WriteAsync("Unauthorized");
    }

    #endregion
}

