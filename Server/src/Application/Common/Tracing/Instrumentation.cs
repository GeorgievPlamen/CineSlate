using System.Diagnostics;
using Microsoft.Extensions.Options;

namespace Application.Common.Tracing;

public class Instrumentation(IOptions<App> appConfig)
{
    private readonly string _appName = appConfig.Value.Name;
    private readonly string _appVersion = appConfig.Value.Version;

    public ActivitySource GetActivitySource() => new(_appName, _appVersion);
}
