using HardWorkingBLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HardWorkingServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //should be POST, but is a demo, right?
        [HttpGet("{empId}")]
        public async Task<string> CalculateHistory([FromServices] EmployeeHistory history,int empId)
        {
            return await history.CalculateEmployeeHistory(empId);
        }

        [HttpGet("{guid}")]
        public async Task<Results<Ok<string>, ContentHttpResult>> History([FromServices] EmployeeHistory history, string guid)
        {

            var data=await history.GetEmployeeHistory(guid);
            
            if (data == null){
                Console.WriteLine($"the history for {guid} is yet not ready");
                //return TypedResults.StatusCode(503);
                return TypedResults.Content(content:"not yet ready",statusCode: 503);
            }

            return TypedResults.Ok<string>(data);

        }


        [EnableRateLimiting("max3")]
        [HttpGet("{empId}")]
        public async Task<string> GetHistoryNOW_LimitedConcurrency(int empId)
        {
            await Task.Delay(Random.Shared.Next(empId)*10);
            return "this is the history for " + empId;
        }


    }
}
