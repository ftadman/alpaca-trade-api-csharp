﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Alpaca.Markets.Extensions
{
    /// <summary>
    /// Set of extension methods for the <see cref="IAlpacaNewsStreamingClient"/> interface.
    /// </summary>
    public static partial class AlpacaNewsStreamingClientExtensions
    {
        /// <summary>
        /// Gets the news articles updates subscription for the all assets from the <paramref name="symbols"/> list.
        /// </summary>
        /// <param name="client">Target instance of the <see cref="IAlpacaNewsStreamingClient"/> interface.</param>
        /// <param name="symbols">Alpaca asset names list (non-empty) for news articles updates subscribing.</param>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{INewsArticle}.Received"/> event.
        /// </returns>
        [UsedImplicitly]
        [CLSCompliant(false)]
        public static IAlpacaDataSubscription<INewsArticle> GetNewsSubscription(
            this IAlpacaNewsStreamingClient client,
            params String[] symbols) =>
            getNewsSubscription(
                client.EnsureNotNull(nameof(client)),
                symbols.EnsureNotNull(nameof(symbols)));

        /// <summary>
        /// Gets the news articles updates subscription for the all assets from the <paramref name="symbols"/> list.
        /// </summary>
        /// <param name="client">Target instance of the <see cref="IAlpacaNewsStreamingClient"/> interface.</param>
        /// <param name="symbols">Alpaca asset names list (non-empty) for news articles updates subscribing.</param>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{INewsArticle}.Received"/> event.
        /// </returns>
        [UsedImplicitly]
        [CLSCompliant(false)]
        public static IAlpacaDataSubscription<INewsArticle> GetNewsSubscription(
            this IAlpacaNewsStreamingClient client,
            IEnumerable<String> symbols) =>
            getNewsSubscription(
                client.EnsureNotNull(nameof(client)),
                symbols.EnsureNotNull(nameof(symbols)));

        /// <summary>
        /// Gets the news articles updates subscription for the <paramref name="symbol"/> asset. This subscription is
        /// returned with pending subscription state and will be unsubscribed on disposing so you can use it
        /// inside the <c>using</c> or <c>await using</c> statements for more clear resource management.
        /// </summary>
        /// <param name="client">Target instance of the <see cref="IAlpacaNewsStreamingClient"/> interface.</param>
        /// <param name="symbol">Alpaca asset name for news articles updates subscribing.</param>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{INewsArticle}.Received"/> event.
        /// </returns>
        [UsedImplicitly]
        [CLSCompliant(false)]
        public static ValueTask<IDisposableAlpacaDataSubscription<INewsArticle>> SubscribeNewsAsync(
            this IAlpacaNewsStreamingClient client,
            String symbol) =>
            DisposableAlpacaDataSubscription<INewsArticle>.CreateAsync(
                client.EnsureNotNull(nameof(client)).GetNewsSubscription(symbol),
                client);

        /// <summary>
        /// Gets the news articles updates subscription for all assets from the <paramref name="symbols"/> list.
        /// This subscription is returned with pending subscription state and will be unsubscribed on disposing
        /// so you can use it inside the <c>using</c> or <c>await using</c> statements for more clear resource management.
        /// </summary>
        /// <param name="client">Target instance of the <see cref="IAlpacaNewsStreamingClient"/> interface.</param>
        /// <param name="symbols">Alpaca asset names list (non-empty) for news articles updates subscribing.</param>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{INewsArticle}.Received"/> event.
        /// </returns>
        [UsedImplicitly]
        [CLSCompliant(false)]
        public static ValueTask<IDisposableAlpacaDataSubscription<INewsArticle>> SubscribeNewsAsync(
            this IAlpacaNewsStreamingClient client,
            params String[] symbols) =>
            DisposableAlpacaDataSubscription<INewsArticle>.CreateAsync(
                client.EnsureNotNull(nameof(client)).GetNewsSubscription(symbols),
                client);

        /// <summary>
        /// Gets the news articles updates subscription for all assets from the <paramref name="symbols"/> list.
        /// This subscription is returned with pending subscription state and will be unsubscribed on disposing
        /// so you can use it inside the <c>using</c> or <c>await using</c> statements for more clear resource management.
        /// </summary>
        /// <param name="client">Target instance of the <see cref="IAlpacaNewsStreamingClient"/> interface.</param>
        /// <param name="symbols">Alpaca asset names list (non-empty) for news articles updates subscribing.</param>
        /// <returns>
        /// Subscription object for tracking updates via the <see cref="IAlpacaDataSubscription{INewsArticle}.Received"/> event.
        /// </returns>
        [UsedImplicitly]
        [CLSCompliant(false)]
        public static ValueTask<IDisposableAlpacaDataSubscription<INewsArticle>> SubscribeNewsAsync(
            this IAlpacaNewsStreamingClient client,
            IEnumerable<String> symbols) =>
            DisposableAlpacaDataSubscription<INewsArticle>.CreateAsync(
                client.EnsureNotNull(nameof(client)).GetNewsSubscription(symbols),
                client);

        private static IAlpacaDataSubscription<INewsArticle> getNewsSubscription(
            IAlpacaNewsStreamingClient client,
            IEnumerable<String> symbols) =>
            getSubscription(client.GetNewsSubscription, symbols);

        private static IAlpacaDataSubscription<TItem> getSubscription<TItem>(
            Func<String, IAlpacaDataSubscription<TItem>> selector,
            IEnumerable<String> symbols) =>
            new AlpacaDataSubscriptionContainer<TItem>(symbols.Select(selector));
    }
}