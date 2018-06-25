namespace LookingForMyFriends.Infrastructure.Services.Interfaces
{
    public interface IMemoryCacheService
    {
        void Insert(string cacheKey, object value);

        object Get(string cacheKey);

        bool Exists(string cacheKey);

        void Remove(string cacheKey);
    }
}
