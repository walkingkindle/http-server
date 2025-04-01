using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace codecrafters_http_server.src.Domain.Entities
{
    public class Endpoint : ValueObject
    {
        public string Route { get; set; }

        public string Query { get; set; }

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
            if (value.Trim() == "/")
            {
                return new Endpoint(route: "/", query: "");
            }

            string[] values = value.Split('/', StringSplitOptions.RemoveEmptyEntries);

            string route = values.Length > 0 ? $"/{values[0].Trim()}" : "/";
            string query = values.Length > 1 ? values[1].Trim() : "";

            return new Endpoint(route, query);
            }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}