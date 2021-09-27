﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace Alpaca.Markets
{
    /// <summary>
    /// Exchanges supported by Alpaca REST API.
    /// </summary>
    [JsonConverter(typeof(ExchangeEnumConverter))]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public enum CryptoExchange
    {
        /// <summary>
        /// Unknown exchange (not supported by this version of SDK).
        /// </summary>
        [UsedImplicitly]
        [EnumMember(Value = "ERSX")]
        Ersx,

        /// <summary>
        /// NYSE American Stock Exchange.
        /// </summary>
        [UsedImplicitly]
        [EnumMember(Value = "GNSS")]
        Gnss,

        /// <summary>
        /// NYSE Arca Stock Exchange.
        /// </summary>
        [UsedImplicitly]
        [EnumMember(Value = "CBSE")]
        Cbse
    }
}
