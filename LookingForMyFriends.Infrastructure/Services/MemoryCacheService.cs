using LookingForMyFriends.Infrastructure.Services.Interfaces;
using System;
using System.Runtime.Caching;

namespace LookingForMyFriends.Infrastructure.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly ObjectCache _objCache = MemoryCache.Default;

        public void Insert(string cacheKey, object value)
        {
            _objCache.Add(cacheKey, value, DateTimeOffset.MaxValue);
        }

        public object Get(string cacheKey)
        {
            return cacheKey != null ? _objCache.Get(cacheKey) : default(object);
        }

        public bool Exists(string cacheKey)
        {
            return _objCache.Get(cacheKey) != null;
        }

        public void Remove(string cacheKey)
        {
            _objCache.Remove(cacheKey);
        }
    }
}
