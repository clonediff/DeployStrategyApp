using Prometheus;

const string Version = "v3",
    VersionLabel = "Version";

const string InstanceIdLabel = "InstanceId";
string InstanceId = Guid.NewGuid().ToString();

Counter RequestsTotal = Metrics.CreateCounter("requests_total", "Total received requests count", VersionLabel, InstanceIdLabel);

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    RequestsTotal.WithLabels(Version, InstanceId).Inc();
    return $"Version: {Version}\tResponse from {InstanceId}\n\n";
});

app.MapGet("/healthcheck", () => Results.Ok());

app.MapMetrics();

app.Run();
