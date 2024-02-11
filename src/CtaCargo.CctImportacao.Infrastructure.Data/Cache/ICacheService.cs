using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CtaCargo.CctImportacao.Infrastructure.Data.Cache;
public interface ICacheService
{
    Task<T> GetData<T>(string key, CancellationToken cancellationToken = default);
    Task RemoveData(string key);
    Task SetData<T>(string key, T value, DateTimeOffset expirationTime);
    Task SetStreamData(string key, Stream stream);
}