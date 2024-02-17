namespace BooksApp.Extensions;

public static class StringExtension
{
	public static string ToCapitalized(this string str)
	{
		if (str.Length == 0)
		{
			return str;
		}

		if (str.Length == 1)
		{
			return str.ToUpper();
		}

		return str[0].ToString().ToUpper() + str[1..];
	}
}
