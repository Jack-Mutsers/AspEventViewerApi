using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspEventVieuwerAPI.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAuthAttribute : Attribute, IAsyncActionFilter
    {
        private const string ApiKeyHeaderName = "ApiKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (false) {
                if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var potentialApiKey))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                try
                {
                    Guid ParsedData = Guid.Parse(potentialApiKey);
                    //CurlConnector connector = new CurlConnector(ParsedData);

                    //var valid = connector.GetValidation();

                    /*
                    var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
                    var apiKey = configuration.GetValue<string>("ApiKey");

                    if (!apiKey.Equals(potentialApiKey))
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }
                    //*/

                    //if (valid)
                    //{
                    //    await next();
                    //}
                    //else
                    //{
                    //    context.Result = new UnauthorizedResult();
                    //    return;
                    //}
                }
                catch (Exception ex)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
            else
            {
                await next();
            }
        }
    }
}
