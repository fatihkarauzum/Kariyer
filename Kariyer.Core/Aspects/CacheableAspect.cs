using Castle.DynamicProxy;
using Kariyer.Common.Services;
using Kariyer.Core.Attributes;
using Kariyer.Core.Interceptors;
using System.Reflection;

namespace Kariyer.Common.Aspects;

public class CacheableAspect : BaseInterceptor<CacheableAttribute> {

	private readonly CacheService cacheService;

	public CacheableAspect(CacheService cacheService) {

		this.cacheService = cacheService;
	}

	public override void Intercept(IInvocation invocation) {

		if (IsCacheable(invocation, out CacheableAttribute cacheAttribute)) {
			string cacheKey = cacheAttribute.Key;
			if (TryGetFromCache(cacheKey, out object value)) {
				invocation.ReturnValue = value;
			}
			else {
				invocation.Proceed();
				if (IsTaskResult(invocation, out object result)) {
					cacheService.Set(cacheKey, result, TimeSpan.FromHours(cacheAttribute.ExpirationHour));
					return;
				}

				cacheService.Set(cacheKey, invocation.ReturnValue, TimeSpan.FromDays(cacheAttribute.ExpirationHour));
			}
		}
		else {
			invocation.Proceed();
		}
	}

	private bool IsCacheable(IInvocation invocation, out CacheableAttribute? cacheAttribute) {

		cacheAttribute = invocation.MethodInvocationTarget
			.GetCustomAttribute(typeof(CacheableAttribute), false) as CacheableAttribute;

		return cacheAttribute != null;
	}

	private bool IsTaskResult(IInvocation invocation, out object result) {

		result = invocation.ReturnValue;

		if (result is Task task) {
			if (task.GetType().IsGenericType) {

				Task.WaitAll((Task) result);
				return result != null;
			}
		}

		return false;
	}

	private bool TryGetFromCache(string cacheKey, out object value) {

		if (cacheService.TryGetValue(cacheKey, out value))
			return true;

		return false;
	}
}
