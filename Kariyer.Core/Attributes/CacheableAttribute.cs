namespace Kariyer.Core.Attributes;

public class CacheableAttribute : BaseAttribute {

	public string Key { get; set; } = null!;

	public int ExpirationHour { get; set; } = 1;

}
