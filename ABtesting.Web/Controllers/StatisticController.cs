using ABtesting.Service;
using Microsoft.AspNetCore.Mvc;

namespace ABtesting.Web;

[ApiController]
[Route("api/[controller]")]

public class StatisticController : ControllerBase
{
    private readonly IStatisticService _statisticService;

    public StatisticController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }
    
    [HttpGet("devices")]
    public ActionResult<int> GetNumberOfDevices()
    {
        var numberOfDevices = _statisticService.GetAllDevices();
        return Ok(numberOfDevices);
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ExperimentModel>>> GetExperiments()
    {
        var experimentModels = await _statisticService.GetAllExperiments();
        return Ok(experimentModels);
    }
}