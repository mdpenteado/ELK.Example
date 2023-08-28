using ELK.Example.Domain.Ports.Input;
using ELK.Example.ELK.Example.Adapter.Logs;
using ELK.Example.ELK.Example.Adapter.Logs.Facades;
using System.Text;

namespace ELK.Example.Infrastructure.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogFacade _logFacade;
        public RequestResponseLoggingMiddleware(RequestDelegate next,
                                                ILogFacade logFacade)
        {
            _next = next;
            _logFacade = logFacade;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Registrar o request
            await LogRequest(context.Request);

            // Capturar o response
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                // Chamar o próximo middleware no pipeline
                await _next(context);

                // Registrar o response
                await LogResponse(context.Response);

                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task LogRequest(HttpRequest request)
        {
            request.EnableBuffering();

            var requestBodyStream = new StreamReader(request.Body);
            var requestBody = await requestBodyStream.ReadToEndAsync();
            request.Body.Position = 0;

            var log = new LogModel();
            log.Path = request.Path;
            log.Body = requestBody;
            log.Method = request.Method;

            _logFacade.CreateLog(log);
        }

        private async Task LogResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            var logMessage = new StringBuilder();
            logMessage.AppendLine($"Response Status Code: {response.StatusCode}");
            logMessage.AppendLine($"Response Body: {responseBody}");

            var log = new LogModel();
            log.StatusCode = response.StatusCode;
            log.Body = responseBody;

            _logFacade.CreateLog(log);
        }
    }
}