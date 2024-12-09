using Prometheus;

const string Version = "v2",
    VersionLabel = "Version";

const string InstanceIdLabel = "InstanceId";
string InstanceId = Guid.NewGuid().ToString();

Counter RequestsTotal = Metrics.CreateCounter("requests_total", "Total received requests count", VersionLabel, InstanceIdLabel);

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () =>
{
    RequestsTotal.WithLabels(Version, InstanceId).Inc();
    return "Hello World!";
});

app.MapMetrics();

app.Run();
