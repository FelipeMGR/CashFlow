using System.Globalization;

namespace CashFlow.API.Middleware
{
    public class CultureMiddleware(RequestDelegate next)
    {

        private readonly RequestDelegate _next = next;
        public async Task Invoke(HttpContext context)
        {
            List<CultureInfo> supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

            string requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

            CultureInfo cultureInfo = new("en");

            if ((!string.IsNullOrWhiteSpace(requestedCulture)) && supportedCultures.Exists(language => language.Name.Equals(requestedCulture)))
            {
                cultureInfo = new CultureInfo(requestedCulture);
            }

            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            await _next(context);
        }
    }
}
