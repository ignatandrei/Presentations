using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace demoFrom
{
    //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-2.1
    public class MiddleWareHeader : IMiddleware
    {
        public MiddleWareHeader(InjectedService ij)
        {
            Ij = ij;
        }

        public InjectedService Ij { get; }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            context.Items.Add("testH", Ij.MyId);
            context.Request.Headers.Add("testHeader", Ij.MyId.ToString());
            context.Request.QueryString.Add("injected", Ij.MyId.ToString());
            await next(context);
            
        }
    }
}
