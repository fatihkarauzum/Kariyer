namespace Kariyer.Core.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class BaseAttribute : Attribute {

	public int Priority { get; set; }
}
