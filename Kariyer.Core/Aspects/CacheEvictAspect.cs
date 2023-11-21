using Castle.DynamicProxy;
using Kariyer.Common.Services;
using Kariyer.Core.Attributes;
using Kariyer.Core.Interceptors;
using System.Reflection;

namespace Kariyer.Core.Aspects;

public class CacheEvictAspect : BaseInterceptor<CacheEvictAttribute> {

	private readonly CacheService cacheService;

	public CacheEvictAspect(CacheService cacheService) {

		this.cacheService = cacheService;
	}

	public override void Intercept(IInvocation invocation) {

		if (HasCacheEvict(invocation, out CacheEvictAttribute? cacheAttribute)) {

			string cacheKey = cacheAttribute!.Key;
			EvictCache(cacheKey);
		}

		invocation.Proceed();
	}

	private bool HasCacheEvict(IInvocation invocation, out CacheEvictAttribute? cacheEvictAttribute) {

		cacheEvictAttribute = invocation.MethodInvocationTarget
			.GetCustomAttribute(typeof(CacheEvictAttribute), false) as CacheEvictAttribute;

		return cacheEvictAttribute != null;
	}

	private void EvictCache(string cacheKey) {

		cacheService.Remove(cacheKey);
	}
}