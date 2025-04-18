using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Reports.Application;
using System.Net;

namespace Reports.Api.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("Cors")]
public class ReportExtractorController : ControllerBase
{
    private readonly ILogger<ReportExtractorController> logger;
    private readonly IQueryExecutingManager queryExecutingManager;

    public ReportExtractorController(ILogger<ReportExtractorController> logger, IQueryExecutingManager queryExecutingManager)
    {
        this.logger = logger;
        this.queryExecutingManager = queryExecutingManager;
    }

    [HttpGet(Name ="Get")]
    public async Task<Response> Get(string commandText)
    {
        var result = await queryExecutingManager.GetReport(commandText);
        if (result.Count()>0)
        {
            return new Response
            {
                Result = result,
                RequestStatus = HttpStatusCode.OK,
                Message = "Request completed successfully"
            };
        }
        else
        {
            return new Response
            {
                Result = null, // Updated from 'Data' to 'Result' to match the Response class definition
                RequestStatus = HttpStatusCode.NoContent,
                Message = "Data dont exist for the asked requirement."
            };
        }
    }
}
