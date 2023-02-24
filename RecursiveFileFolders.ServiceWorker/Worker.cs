using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecursiveFileFolders.ServiceWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger) => _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var input = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var searchPattern = "*.*";
            
            _logger.LogInformation("Current directory: \r\n{input}", input);
            
            var output = await Task.Run(() =>
            {
                var allFiles = FileSystemHelper.GetAllFiles(input, searchPattern);
                return string.Join(Environment.NewLine, allFiles);
            });

            _logger.LogInformation("Output: \r\n{output}", output.Replace(input, string.Empty));

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);

        }
    }
}
