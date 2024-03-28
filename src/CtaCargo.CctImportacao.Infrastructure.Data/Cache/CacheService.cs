using Microsoft.Extensions.Caching.Distributed;
using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Cache;
public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T> GetDataAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        string cachedValue = await _distributedCache.GetStringAsync(
            key,
            cancellationToken);

        if (string.IsNullOrEmpty(cachedValue))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(cachedValue);
    }

    public async Task SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime)
    {
        await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize<T>(value), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expirationTime - DateTime.UtcNow
        });
    }

    public async Task SetDataAsync<T>(string key, T value, double milliSeconds)
    {
        var expiraion = ConvertMillisecondsToTime(milliSeconds);

        await _distributedCache.SetStringAsync(key, JsonSerializer.Serialize<T>(value), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiraion.AddMinutes(-2) - DateTime.UtcNow 
        });
    }

    public async Task SetStreamData(string key, Stream stream)
    {
        await _distributedCache.SetAsync(key, UseBinaryReader(stream), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1)
        });
    }

    public async Task RemoveData(string key)
    {
        await _distributedCache.RemoveAsync(key);
    }

    private byte[] UseBinaryReader(Stream stream)
    {
        byte[] bytes;
        using (var binaryReader = new BinaryReader(stream))
        {
            bytes = binaryReader.ReadBytes((int)stream.Length);
        }
        return bytes;
    }

    public static DateTime ConvertMillisecondsToTime(double milliseconds)
    {
        return UnixEpoch.AddMilliseconds(milliseconds);
    }
}