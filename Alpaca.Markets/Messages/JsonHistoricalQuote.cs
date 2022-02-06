﻿namespace Alpaca.Markets;

internal sealed class JsonHistoricalQuote : IQuote, ISymbolMutable
{
    [JsonProperty(PropertyName = "t", Required = Required.Always)]
    public DateTime TimestampUtc { get; set; }

    [JsonProperty(PropertyName = "ax", Required = Required.Always)]
    public String AskExchange { get; set; } = String.Empty;

    [JsonProperty(PropertyName = "ap", Required = Required.Default)]
    public Decimal AskPrice { get; set; }

    [JsonProperty(PropertyName = "as", Required = Required.Default)]
    public Decimal AskSize { get; set; }

    [JsonProperty(PropertyName = "bx", Required = Required.Always)]
    public String BidExchange { get; set; } = String.Empty;

    [JsonProperty(PropertyName = "bp", Required = Required.Default)]
    public Decimal BidPrice { get; set; }

    [JsonProperty(PropertyName = "bs", Required = Required.Default)]
    public Decimal BidSize { get; set; }

    [JsonProperty(PropertyName = "c", Required = Required.Default)]
    public List<String> ConditionsList { get; set; } = new();

    [JsonProperty(PropertyName = "z", Required = Required.Default)]
    public String Tape { get; set; } = String.Empty;

    [JsonIgnore]
    public String Symbol { get; private set; } = String.Empty;

    [JsonIgnore]
    public IReadOnlyList<String> Conditions =>
        ConditionsList.EmptyIfNull();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void SetSymbol(String symbol) => Symbol = symbol;
}
