using Microsoft.Extensions.Caching.Memory;

namespace Kariyer.Common.Services.Impl;

public class CacheServiceImpl : CacheService {

	private readonly IMemoryCache _cache;

	public CacheServiceImpl() {
		_cache = new MemoryCache(new MemoryCacheOptions());
	}

	public T Get<T>(string key) {
		return _cache.Get<T>(key);
	}

	public bool TryGetValue<T>(string key, out T value) {
		T _value = _cache.Get<T>(key);

		if (_value == null) {
			value = default;
			return false;
		}

		value = _value;
		return true;
	}

	public void Set<T>(string key, T value, TimeSpan expiration) {
		var cacheEntryOptions = new MemoryCacheEntryOptions()
			.SetAbsoluteExpiration(expiration);

		_cache.Set(key, value, cacheEntryOptions);
	}

	public bool Remove(string key) {
		if (_cache.TryGetValue(key, out _)) {
			_cache.Remove(key);
			return true;
		}

		return false;
	}

	public void Clear() {
		_cache.Dispose();
	}

}
