using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Cache;
public interface ICacheService
{
    Task<T> GetDataAsync<T>(string key, CancellationToken cancellationToken = default);
    Task RemoveData(string key);
    Task SetDataAsync<T>(string key, T value, DateTimeOffset expirationTime);
    Task SetDataAsync<T>(string key, T value, double milliSeconds);
    Task SetStreamData(string key, Stream stream);
}