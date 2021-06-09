﻿using SmartSql.Abstractions.Cache;
using System;
using System.Collections.Generic;
using SmartSql.Abstractions;
using Microsoft.Extensions.Logging;
using SmartSql.Configuration.Statements;
using SmartSql.Abstractions.DbSession;
using System.Collections.Concurrent;

namespace SmartSql.Cahce
{
    public class CacheManager : ICacheManager
    {
        private readonly ILogger<CacheManager> _logger;
        private readonly SmartSqlContext _smartSqlContext;
        private readonly IDbConnectionSessionStore _dbSessionStore;
        private readonly ConcurrentDictionary<Guid, SessionRequest> _cachedSessionRequest = new ConcurrentDictionary<Guid, SessionRequest>();
        private IDictionary<string, DateTime> _cacheMappedLastFlushTime;
        private readonly System.Threading.Timer _timer;
        private readonly TimeSpan _defaultDueTime = TimeSpan.FromMinutes(1);
        private readonly TimeSpan _defaultPeriodTime = TimeSpan.FromMinutes(1);
        public CacheManager(
            ILogger<CacheManager> logger
            , SmartSqlContext smartSqlContext
            , IDbConnectionSessionStore dbSessionStore)
        {
            _logger = logger;
            _smartSqlContext = smartSqlContext;
            _dbSessionStore = dbSessionStore;
            InitCacheMappedLastFlushTime();
            _timer = new System.Threading.Timer(Run, null, _defaultDueTime, _defaultPeriodTime);
        }

        public void RequestExecuted(IDbConnectionSession dbSession, RequestContext context)
        {
            var sessionId = dbSession.Id;
            if (dbSession.Transaction is null)
            {
                FlushOnExecute(context);
            }
            else
            {

                if (_cachedSessionRequest.TryGetValue(sessionId, out SessionRequest sessionRequest))
                {
                    sessionRequest.Requests.Add(context);
                }
                else
                {
                    sessionRequest = new SessionRequest
                    {
                        SessionId = sessionId,
                        Requests = new List<RequestContext> { context }
                    };
                    _cachedSessionRequest.TryAdd(sessionId, sessionRequest);
                }
            }
        }
        public void RequestCommitted(IDbConnectionSession dbSession)
        {
            var sessionId = dbSession.Id;
            if (_cachedSessionRequest.TryGetValue(sessionId, out SessionRequest sessionRequest))
            {
                foreach (var context in sessionRequest.Requests)
                {
                    FlushOnExecute(context);
                }
                _cachedSessionRequest.TryRemove(sessionId, out _);
            }
        }

        private void Run(object state)
        {
            FlushInterval();
        }

        private void InitCacheMappedLastFlushTime()
        {
            _cacheMappedLastFlushTime = new Dictionary<string, DateTime>();
            foreach (var cache in _smartSqlContext.Caches)
            {
                if (cache.FlushInterval is null) { continue; }
                _cacheMappedLastFlushTime.Add(cache.Id, DateTime.Now);
            }
        }

        private void FlushInterval()
        {
            try
            {
                foreach (var cache in _smartSqlContext.Caches)
                {
                    if (cache.FlushInterval is null) { continue; }
                    var lastFlushTime = _cacheMappedLastFlushTime[cache.Id];
                    var nextFlushTime = lastFlushTime.Add(cache.FlushInterval.Interval);
                    if (DateTime.Now >= nextFlushTime)
                    {
                        cache.Provider.Flush();
                        UpdateCacheFlushTime(cache.Id, DateTime.Now);
                        if (_logger.IsEnabled(LogLevel.Debug))
                        {
                            _logger.LogDebug($"CacheManager FlushInterval Cache.Id:{cache.Id}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, ex.Message);

            }

        }

        private void FlushOnExecute(RequestContext requestContext)
        {
            try
            {
                if (_smartSqlContext.ExecuteMappedCacheFlush.TryGetValue(requestContext.FullSqlId, out IList<Statement> needFlushStatements))
                {
                    foreach (var needFlushStatement in needFlushStatements)
                    {
                        needFlushStatement.Cache.Provider.Flush();
                        UpdateCacheFlushTime(needFlushStatement.Cache.Id, DateTime.Now);
                    }
                    if (_logger.IsEnabled(LogLevel.Debug))
                    {
                        _logger.LogDebug($"CacheManager FlushOnExecute FullSqlId:{requestContext.FullSqlId}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(ex.HResult), ex, ex.Message);
            }
        }

        private void UpdateCacheFlushTime(string cacheId, DateTime flushTime)
        {
            _cacheMappedLastFlushTime[cacheId] = flushTime;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _timer.Dispose();
            }
        }

        public bool TryGet<T>(RequestContext context, out T cachedResult)
        {
            cachedResult = default(T);
            if (context.Statement is null) { return false; }
            var cachedType = typeof(T);
            string fullSqlId = context.FullSqlId;
            var statement = context.Statement;
            if (statement.Cache is null) { return false; }
            var cacheKey = new CacheKey(context);
            var cache = statement.Cache.Provider[cacheKey, cachedType];
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"CacheManager GetCache FullSqlId:{fullSqlId}，Success:{cache is object} !");
            }
            if (cache is null) { return false; }
            cachedResult = (T)cache;
            return true;
        }

        public void TryAdd<T>(RequestContext context, T cacheItem)
        {
            if (context.Statement is null) { return; }
            var cachedType = typeof(T);
            string fullSqlId = context.FullSqlId;
            var statement = context.Statement;
            if (statement.Cache is null) { return; }
            var cacheKey = new CacheKey(context);
            statement.Cache.Provider[cacheKey, cachedType] = cacheItem;
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.LogDebug($"CacheManager SetCache FullSqlId:{fullSqlId}");
            }
        }
    }
    public class SessionRequest
    {
        public Guid SessionId { get; set; }
        public IList<RequestContext> Requests { get; set; }
    }
}


