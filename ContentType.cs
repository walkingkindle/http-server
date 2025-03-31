using CSharpFunctionalExtensions;

namespace codecrafters_http_server
{
    public class HttpContentType :ValueObject
    {
        public static readonly string TextType = "text/plain";

        public static readonly string JsonType = "application/json";

        public static readonly string FileType = "application/octet-stream";

        public static readonly string Everything = "*/*";

        private readonly string _value;

        private HttpContentType(string value)
        {
            _value = value;
        }

        public static Result<HttpContentType> Create(Maybe<string> httpContent)
        {
            var validTypes = new HashSet<string> { TextType, JsonType, FileType, Everything};

            return httpContent.ToResult("HttpContent cannot be null")
                .Map(httpContentType => httpContentType.Trim())
                .Ensure(p => validTypes.Contains(p), "httpContent is invalid or not supported")
                .Map(httpContentType => new HttpContentType(httpContentType));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
