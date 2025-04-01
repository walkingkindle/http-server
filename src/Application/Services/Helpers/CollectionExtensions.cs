namespace codecrafters_http_server.src.Application.Services.Helpers
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
          if(enumerable == null)
            return true;

            return !enumerable.Any();
        }
    }
}
