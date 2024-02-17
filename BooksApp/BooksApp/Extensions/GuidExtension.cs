namespace BooksApp.Extensions;

public static class GuidExtension
{
	public static Guid ToGuid(this Guid? source)
	{
		return source ?? Guid.Empty;
	}
}
