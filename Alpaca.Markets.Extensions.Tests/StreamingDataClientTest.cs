﻿namespace Alpaca.Markets.Extensions.Tests;

public sealed class StreamingDataClientTest
{
    [Fact]
    public async Task SubscribeAsyncWorks()
    {
        var cts = new CancellationTokenSource();
        var client = new Mock<IStreamingDataClient>();

        client.Setup(_ => _.SubscribeAsync(
                It.IsAny<IEnumerable<IAlpacaDataSubscription>>(),
                It.Is<CancellationToken>(__ => __.Equals(cts.Token))))
            .Returns(ValueTask.CompletedTask);

        await client.Object.SubscribeAsync(createSubscription(), createSubscription())
            .WithCancellation(cts.Token);

        client.VerifyAll();
    }

    [Fact]
    public async Task UnsubscribeAsyncWorks()
    {
        var cts = new CancellationTokenSource();
        var client = new Mock<IStreamingDataClient>();

        client.Setup(_ => _.UnsubscribeAsync(
                It.IsAny<IEnumerable<IAlpacaDataSubscription>>(),
                It.Is<CancellationToken>(__ => __.Equals(cts.Token))))
            .Returns(ValueTask.CompletedTask);

        await client.Object.UnsubscribeAsync(createSubscription(), createSubscription())
            .WithCancellation(cts.Token);

        client.VerifyAll();
    }

    private static IAlpacaDataSubscription createSubscription() => 
        new Mock<IAlpacaDataSubscription>().Object;
}
