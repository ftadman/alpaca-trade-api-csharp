﻿namespace Alpaca.Markets.Tests;

internal sealed class ErrorsAndWarningsTracker : IDisposable
{
    private static readonly TimeSpan _timeout = TimeSpan.FromMilliseconds(100);

    private readonly IStreamingClient _client;

    private readonly Int32 _expectedWarnings;

    private readonly Int32 _expectedErrors;

    private readonly Barrier _barrier;

    private Int32 _warnings;

    private Int32 _errors;

    public ErrorsAndWarningsTracker(
        IStreamingClient client,
        Int32 expectedWarnings,
        Int32 expectedErrors)
    {
        _expectedWarnings = expectedWarnings;
        _expectedErrors = expectedErrors;
        _client = client;

        _barrier = new Barrier(expectedErrors + expectedWarnings + 1);

        _client.OnWarning += handleWarning;
        _client.OnError += handleError;
    }

    public void WaitAllEvents() =>
        Assert.True(_barrier.SignalAndWait(_timeout));

    public void Dispose()
    {
        _barrier.Dispose();

        _client.OnError -= handleError;
        _client.OnWarning -= handleWarning;

        Assert.Equal(_expectedWarnings, _warnings);
        Assert.Equal(_expectedErrors, _errors);
    }

    private void handleError(Exception _)
    {
        _barrier.RemoveParticipant();
        ++_errors;
    }

    private void handleWarning(String _)
    {
        _barrier.RemoveParticipant();
        ++_warnings;
    }
}
