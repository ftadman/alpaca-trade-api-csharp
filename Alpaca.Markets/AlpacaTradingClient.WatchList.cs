﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Alpaca.Markets
{
    internal sealed partial class AlpacaTradingClient
    {
        /// <inheritdoc />
        public Task<IReadOnlyList<IWatchList>> ListWatchListsAsync(
            CancellationToken cancellationToken = default) =>
            _httpClient.GetAsync<IReadOnlyList<IWatchList>, List<JsonWatchList>>(
                "v2/watchlists", cancellationToken);

        /// <inheritdoc />
        public Task<IWatchList> CreateWatchListAsync(
            NewWatchListRequest request,
            CancellationToken cancellationToken = default) =>
            _httpClient.PostAsync<IWatchList, JsonWatchList, NewWatchListRequest>(
                "v2/watchlists", request,  cancellationToken);

        /// <inheritdoc />
        public Task<IWatchList> GetWatchListByIdAsync(
            Guid watchListId,
            CancellationToken cancellationToken = default) =>
            _httpClient.GetAsync<IWatchList, JsonWatchList>(
                getEndpointUri(watchListId), cancellationToken);

        /// <inheritdoc />
        public async Task<IWatchList> GetWatchListByNameAsync(
            String name,
            CancellationToken cancellationToken = default) =>
            await _httpClient.GetAsync<IWatchList, JsonWatchList>(
                await getEndpointUriBuilderAsync(name).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

        /// <inheritdoc />
        public Task<IWatchList> UpdateWatchListByIdAsync(
            UpdateWatchListRequest request,
            CancellationToken cancellationToken = default) =>
            _httpClient.PutAsync<IWatchList, JsonWatchList, UpdateWatchListRequest>(
                getEndpointUri(request.EnsureNotNull(nameof(request)).Validate().WatchListId), request,
                cancellationToken);

        /// <inheritdoc />
        public Task<IWatchList> AddAssetIntoWatchListByIdAsync(
            ChangeWatchListRequest<Guid> request,
            CancellationToken cancellationToken = default) =>
            _httpClient.PostAsync<IWatchList, JsonWatchList, ChangeWatchListRequest<Guid>>(
                getEndpointUri(request.EnsureNotNull(nameof(request)).Validate().Key), request,
                cancellationToken);

        /// <inheritdoc />
        public async Task<IWatchList> AddAssetIntoWatchListByNameAsync(
            ChangeWatchListRequest<String> request,
            CancellationToken cancellationToken = default) =>
            await _httpClient.PostAsync<IWatchList, JsonWatchList, ChangeWatchListRequest<String>>(
                await getEndpointUriBuilderAsync(request.EnsureNotNull(nameof(request)).Validate().Key).ConfigureAwait(false), request,
                cancellationToken).ConfigureAwait(false);

        /// <inheritdoc />
        public Task<IWatchList> DeleteAssetFromWatchListByIdAsync(
            ChangeWatchListRequest<Guid> request,
            CancellationToken cancellationToken = default) =>
            _httpClient.DeleteAsync<IWatchList, JsonWatchList>(
                getEndpointUri(request.EnsureNotNull(nameof(request)).Validate().Key, request.Asset),
                cancellationToken);

        /// <inheritdoc />
        public async Task<IWatchList> DeleteAssetFromWatchListByNameAsync(
            ChangeWatchListRequest<String> request,
            CancellationToken cancellationToken = default) =>
            await _httpClient.DeleteAsync<IWatchList, JsonWatchList>(
                await getEndpointUriBuilderAsync(
                        request.EnsureNotNull(nameof(request)).Validate().Key, request.Asset)
                    .ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

        /// <inheritdoc />
        public Task<Boolean> DeleteWatchListByIdAsync(
            Guid watchListId,
            CancellationToken cancellationToken = default) =>
            _httpClient.TryDeleteAsync(
                getEndpointUri(watchListId), cancellationToken);

        /// <inheritdoc />
        public async Task<Boolean> DeleteWatchListByNameAsync(
            String name,
            CancellationToken cancellationToken = default) =>
            await _httpClient.TryDeleteAsync(
                await getEndpointUriBuilderAsync(name).ConfigureAwait(false),
                cancellationToken).ConfigureAwait(false);

        private async ValueTask<UriBuilder> getEndpointUriBuilderAsync(
            String name,
            String? asset = null) =>
            new (_httpClient.BaseAddress!)
            {
                Path = String.IsNullOrEmpty(asset)
                    ? "v2/watchlists:by_name"
                    : $"v2/watchlists:by_name/{asset}",
                Query = await new QueryBuilder()
                    .AddParameter("name", 
                        name.ValidateWatchListName() ?? throw new ArgumentException(
                            "Watch list name should be from 1 to 64 characters length.", nameof(name)))
                    .AsStringAsync().ConfigureAwait(false)
            };

        private static String getEndpointUri(
            Guid watchListId,
            String? asset = null) => 
            String.IsNullOrEmpty(asset)
                ? $"v2/watchlists/{watchListId:D}"
                : $"v2/watchlists/{watchListId:D}/{asset}";
    }
}
