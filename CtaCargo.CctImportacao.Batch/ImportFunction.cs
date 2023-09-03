using CtaCargo.CctImportacao.Batch.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CtaCargo.CctImportacao.Batch;

public class ImportFunction
{
    private readonly ILogger _logger;
    private readonly FunctionTimerService _timerService;

    public ImportFunction(FunctionTimerService timerService, ILoggerFactory loggerFactory)
    {
        _timerService = timerService;
        _logger = loggerFactory.CreateLogger<ImportFunction>();
    }

    [Function("ImportFunction")]
    public async Task Run([TimerTrigger("0 * * * * *")] MyInfo myTimer)
    {
        _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");

        await _timerService.CheckFiles();
    }
}

public class MyInfo
{
    public MyScheduleStatus ScheduleStatus { get; set; }

    public bool IsPastDue { get; set; }
}

public class MyScheduleStatus
{
    public DateTime Last { get; set; }

    public DateTime Next { get; set; }

    public DateTime LastUpdated { get; set; }
}