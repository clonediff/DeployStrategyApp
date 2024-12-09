using Prometheus;

const string Version = "v2",
    VersionLabel = "Version";

Counter RequestsTotal = Metrics.CreateCounter("requests_total", "Total received requests count", VersionLabel);

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    RequestsTotal.WithLabels(Version).Inc();
    return "Hello World!";
});

app.MapMetrics();

app.Run();
