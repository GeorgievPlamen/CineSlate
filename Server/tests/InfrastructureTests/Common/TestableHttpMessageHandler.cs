namespace InfrastructureTests.Common;

public class TestableHttpMessageHandler : HttpMessageHandler
{
    public Func<HttpRequestMessage, Task<HttpResponseMessage>> SendAsyncFunc { get; set; } = null!;

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
        => SendAsyncFunc(request);
}