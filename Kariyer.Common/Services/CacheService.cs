namespace Kariyer.Common.Services;

public interface CacheService {

	T Get<T>(string key);

	bool TryGetValue<T>(string key, out T value);

	void Set<T>(string key, T value, TimeSpan expiration);

	bool Remove(string key);

	void Clear();

}