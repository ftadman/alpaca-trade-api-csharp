﻿using System;

namespace Alpaca.Markets
{
    /// <summary>
    /// Provides unified type-safe access for Polygon streaming API via websockets.
    /// </summary>
    public interface IPolygonStreamingClient : IStreamingDataClient
    {
                /// <summary>
        /// Gets the trade updates subscription for the <paramref name="symbol"/> asset.
        /// </summary>
        /// <param name="symbol">Alpaca asset name.</param>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{TApi}.Received"/> event.
        /// </returns>
        IAlpacaDataSubscription<IStreamTrade> GetTradeSubscription(
            String symbol);

        /// <summary>
        /// Gets the quote updates subscription for the <paramref name="symbol"/> asset.
        /// </summary>
        /// <param name="symbol">Alpaca asset name.</param>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{TApi}.Received"/> event.
        /// </returns>
        IAlpacaDataSubscription<IStreamQuote> GetQuoteSubscription(
            String symbol);

        /// <summary>
        /// Gets the minute aggregate (bar) subscription for the all assets.
        /// </summary>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{TApi}.Received"/> event.
        /// </returns>
        IAlpacaDataSubscription<IStreamAgg> GetMinuteAggSubscription();

        /// <summary>
        /// Gets the minute aggregate (bar) subscription for the <paramref name="symbol"/> asset.
        /// </summary>
        /// <param name="symbol">Alpaca asset name.</param>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{TApi}.Received"/> event.
        /// </returns>
        IAlpacaDataSubscription<IStreamAgg> GetMinuteAggSubscription(
            String symbol);

        /// <summary>
        /// Gets the second aggregate (bar) subscription for the all assets.
        /// </summary>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{TApi}.Received"/> event.
        /// </returns>
        IAlpacaDataSubscription<IStreamAgg> GetSecondAggSubscription();

        /// <summary>
        /// Gets the second aggregate (bar) subscription for the <paramref name="symbol"/> asset.
        /// </summary>
        /// <param name="symbol">Alpaca asset name.</param>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{TApi}.Received"/> event.
        /// </returns>
        IAlpacaDataSubscription<IStreamAgg> GetSecondAggSubscription(
            String symbol);
    }
}
