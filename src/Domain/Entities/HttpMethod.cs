using CSharpFunctionalExtensions;

namespace codecrafters_http_server.src.Domain.Entities
{
    public class HttpMethod:ValueObject<HttpMethod>
    {
        public static readonly string POST = "POST";
        public static readonly string GET = "GET";

        private static readonly HashSet<string> _validMethods = new HashSet<string> { POST, GET};

        private readonly string _value;

        private HttpMethod(string value)
        {
            _value = value;
        }

        public static Result<HttpMethod> Create(Maybe<string> value)
        {

            return value.ToResult("Value cannot be null")
                .Map(httpMethod => httpMethod.Trim())
                .Ensure(httpMethod => _validMethods.Contains(httpMethod), "Method is invalid or not supported")
                .Map(httpMethod => new HttpMethod(httpMethod));
        }
        public override bool Equals(object obj)
        {
            if (obj is HttpMethod other)
                return _value.Equals(other._value, StringComparison.OrdinalIgnoreCase);

            return false;
        }
        public override int GetHashCode()
        {
            return _value.ToUpper().GetHashCode();
        }

        protected override bool EqualsCore(HttpMethod other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(HttpMethod left, HttpMethod right)
        {
            if (ReferenceEquals(left, right)) return true;
            if (left is null || right is null) return false;
            return left.Equals(right);
        }

        public static bool operator !=(HttpMethod left, HttpMethod right)
        {
            return !(left == right);
        }
    }
}
