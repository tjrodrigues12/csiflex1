namespace System
{
    public static class StringExtensions
    {
		public static bool HasValue(this string @this)
		{
			return @this != null && !string.IsNullOrEmpty(@this) && !string.IsNullOrWhiteSpace(@this);
		}

		public static bool IsValidUrl(this string @this)
		{
			Uri uriResult;
			return  Uri.TryCreate(@this, UriKind.Absolute, out uriResult)
				&& uriResult.Scheme == Uri.UriSchemeHttp;
		}
    }
}
