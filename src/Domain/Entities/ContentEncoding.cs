using CSharpFunctionalExtensions;
using codecrafters_http_server.src.Application.Services.Helpers;

namespace codecrafters_http_server.src.Domain.Entities
{
    public class ContentEncoding : ValueObject
    {
        public string Value { get; set; }

        private readonly string _value;
        private static readonly List<string> _validContentEncodingList = new List<string> { "gzip" };
        private ContentEncoding(string _value)
        {
            Value = _value;
        }

        public static Result<ContentEncoding> Create(Maybe<string> contentEncodingList)
        {
            return contentEncodingList
                .ToResult("ContentEncoding must not be null")
                .Map(content => content.Trim())
                .Ensure(content => _validContentEncodingList.Contains(content), "Content-Encoding is unsupported")
                .Map(content => new ContentEncoding(content));
        }

        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
