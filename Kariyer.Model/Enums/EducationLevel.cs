namespace Kariyer.Model.Enums;

[Flags]
public enum EducationLevel : byte {

	NONE = 0,
	ELEMENTARY = 1,
	SECONDARY = 2,
	TERTIARY = 4,
	POSTGRADUATE = 8,
	DOCTORATE = 16
}
