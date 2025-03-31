using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace codecrafters_http_server
{
    public class Endpoint : ValueObject
    {
        public string Route { get; set; }

        public string Query { get; set; }

        private readonly string _route;
        private readonly string _query;

        private Endpoint(string route, string query)
        {
             Route = route;
             Query = query;
        }

        public static Result<Endpoint> Create(Maybe<string> value)
        {
            return value
            .ToResult("Value should not be empty")
            .Ensure(endpoint => Regex.IsMatch(endpoint, @"[/,/]"), "Endpoint is invalid")
            .Map(endpoint => GetEndpointFromValue(endpoint));
        }

        private static Endpoint GetEndpointFromValue(string value)
        {
            string[] values = value.Split('/', StringSplitOptions.RemoveEmptyEntries);

            return new Endpoint($"/{values[0]}", values[1] ?? "");

        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}