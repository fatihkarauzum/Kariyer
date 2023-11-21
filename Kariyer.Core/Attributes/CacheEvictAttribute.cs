namespace Kariyer.Core.Attributes;

public class CacheEvictAttribute : BaseAttribute {

	public string Key { get; set; } = null!;
}
