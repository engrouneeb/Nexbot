using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

Console.WriteLine("🔧 CalimaticChatBot - Background Jobs Service");
Console.WriteLine("============================================");
Console.WriteLine();
Console.WriteLine("Status: Project structure ready");
Console.WriteLine("Implementation: Phase 05 (Document Processing Pipeline)");
Console.WriteLine();
Console.WriteLine("This service will handle:");
Console.WriteLine("  • Document processing and indexing");
Console.WriteLine("  • Embedding generation");
Console.WriteLine("  • Usage metrics aggregation");
Console.WriteLine("  • Data cleanup tasks");
Console.WriteLine();
Console.WriteLine("Press Ctrl+C to exit...");
Console.WriteLine();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        // Services will be added in Phase 05
        services.AddLogging(builder =>
        {
            builder.AddConsole();
        });
    })
    .Build();

await host.RunAsync();
