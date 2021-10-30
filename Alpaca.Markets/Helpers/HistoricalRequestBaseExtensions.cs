﻿using System;
using JetBrains.Annotations;

namespace Alpaca.Markets
{
    /// <summary>
    /// Set of extension methods for <see cref="HistoricalRequestBase"/> inheritors' initialization.
    /// </summary>
    public static class HistoricalRequestBaseExtensions
    {
        private const UInt32 MaxPageSize = 10_000;

        /// <summary>
        /// Sets the request page size using the fluent interface approach.
        /// </summary>
        /// <param name="request">Request parameters object.</param>
        /// <param name="pageSize">The request page size.</param>
        /// <returns>The original request parameters object.</returns>
        [UsedImplicitly]
        [CLSCompliant(false)]
        public static TRequest WithPageSize<TRequest>(
            this TRequest request,
            UInt32 pageSize)
            where TRequest: HistoricalRequestBase
        {
            request.EnsureNotNull(nameof(request)).Pagination.Size = pageSize;
            return request;
        }

        /// <summary>
        /// Sets the request page size using the fluent interface approach.
        /// </summary>
        /// <param name="request">Request parameters object.</param>
        /// <param name="pageToken">The request page size.</param>
        /// <returns>The original request parameters object.</returns>
        [UsedImplicitly]
        public static TRequest WithPageToken<TRequest>(
            this TRequest request,
            String pageToken)
            where TRequest: HistoricalRequestBase
        {
            request.EnsureNotNull(nameof(request)).Pagination.Token = pageToken;
            return request;
        }

        internal static DateTime GetValidatedFrom(
            this HistoricalRequestBase request) =>
            getValidatedDate(request.TimeInterval.From, nameof(request.TimeInterval.From));

        internal static DateTime GetValidatedInto(
            this HistoricalRequestBase request) =>
            getValidatedDate(request.TimeInterval.Into, nameof(request.TimeInterval.Into));

        internal static UInt32 GetPageSize(
            this HistoricalRequestBase request) =>
            request.Pagination.Size ?? MaxPageSize;

        private static DateTime getValidatedDate(
            DateTime? date,
            String paramName) =>
            date ?? throw new ArgumentException(
                "Invalid request time interval - empty date", paramName);
    }
}
