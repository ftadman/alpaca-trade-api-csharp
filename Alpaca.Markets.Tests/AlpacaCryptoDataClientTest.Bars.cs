﻿namespace Alpaca.Markets.Tests;

public sealed partial class AlpacaCryptoDataClientTest
{
    [Fact]
    public async Task GetHistoricalBarsAsyncWorks()
    {
        using var mock = _mockClientsFactory.GetAlpacaCryptoDataClientMock();

        mock.AddMultiBarsPageExpectation(PathPrefix, _symbols);

        var bars = await mock.Client.GetHistoricalBarsAsync(
            new HistoricalCryptoBarsRequest(_symbols, Yesterday, Today, BarTimeFrame.Hour));

        Assert.NotNull(bars);
        Assert.NotEmpty(bars.Items);

        bars.Items[Crypto].Validate(Crypto);
        bars.Items[Other].Validate(Other);
    }

    [Fact]
    public async Task GetHistoricalBarsAsyncForSingleWorks()
    {
        using var mock = _mockClientsFactory.GetAlpacaCryptoDataClientMock();

        mock.AddMultiBarsPageExpectation(PathPrefix, _symbol);

        var bars = await mock.Client.GetHistoricalBarsAsync(
            new HistoricalCryptoBarsRequest(Crypto, BarTimeFrame.Hour, _timeInterval));

        Assert.NotNull(bars);
        Assert.NotEmpty(bars.Items);

        bars.Items[Crypto].Validate(Crypto);
    }

    [Fact]
    public async Task ListHistoricalBarsAsyncWorks()
    {
        using var mock = _mockClientsFactory.GetAlpacaCryptoDataClientMock();

        mock.AddMultiBarsPageExpectation(PathPrefix, _symbol);

        var bars = await mock.Client.ListHistoricalBarsAsync(
            new HistoricalCryptoBarsRequest(Crypto, Yesterday, Today, BarTimeFrame.Hour));

        Assert.NotNull(bars);
        Assert.NotEmpty(bars.Items);
        Assert.Equal(Crypto, bars.Symbol);

        bars.Items.Validate(Crypto);
    }

    [Fact]
    public async Task ListHistoricalBarsAsyncForManyWorks()
    {
        using var mock = _mockClientsFactory.GetAlpacaCryptoDataClientMock();

        mock.AddMultiBarsPageExpectation(PathPrefix, _symbols);

        var bars = await mock.Client.ListHistoricalBarsAsync(
            new HistoricalCryptoBarsRequest(_symbols, _timeInterval, BarTimeFrame.Hour));

        Assert.NotNull(bars);
        Assert.NotEmpty(bars.Items);
        Assert.Equal(String.Empty, bars.Symbol);

        bars.Items.Where(_ => _.Symbol == Crypto).Validate(Crypto);
        bars.Items.Where(_ => _.Symbol != Crypto).Validate(Other);
    }

    [Fact]
    public async Task ListHistoricalBarsAsyncWithoutIntervalWorks()
    {
        using var mock = _mockClientsFactory.GetAlpacaCryptoDataClientMock();

        mock.AddMultiBarsPageExpectation(PathPrefix, _symbol);

        var bars = await mock.Client.ListHistoricalBarsAsync(
            new HistoricalCryptoBarsRequest(Crypto, BarTimeFrame.Hour));

        Assert.NotNull(bars);
        Assert.NotEmpty(bars.Items);
        Assert.Equal(Crypto, bars.Symbol);

        bars.Items.Validate(Crypto);
    }
}
