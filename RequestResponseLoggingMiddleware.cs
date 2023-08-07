using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace ELK.Example
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Serilog.ILogger _logger;
        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;

            _logger = new LoggerConfiguration()
                .WriteTo.LogstashHttp("http://localhost:5044")
                .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .CreateLogger();
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

            var logMessage = new StringBuilder();
            logMessage.AppendLine($"Request Method: {request.Method}");
            logMessage.AppendLine($"Request Path: {request.Path}");
            logMessage.AppendLine($"Request Body: {requestBody}");

            _logger.Information(JsonConvert.SerializeObject(logMessage));
        }

        private async Task LogResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            var logMessage = new StringBuilder();
            logMessage.AppendLine($"Response Status Code: {response.StatusCode}");
            logMessage.AppendLine($"Response Body: {responseBody}");

            _logger.Information(JsonConvert.SerializeObject(logMessage));
        }
    }
}