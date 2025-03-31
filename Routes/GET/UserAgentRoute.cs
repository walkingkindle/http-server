﻿using System.Text;

namespace codecrafters_http_server.Routes.GET
{
    public class UserAgentRoute : IHttpRouteHandler
    {
        public override string _route { get; set; } = "/user-agent";
        public override HttpMethod _method { get; set; } = HttpMethod.Create(HttpMethod.GET).Value;

        public override HttpResponse HandleRoute(HttpRequest request)
        {
            if (request.UserAgent is not null)
            {
                return new HttpResponse(request.UserAgent, HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.OK), HttpContentType.TextType, Encoding.UTF8.GetByteCount(request.UserAgent));
            }
            else
            {
                return new HttpResponse("", HttpStatusCodes.GetHttpResponseStatus(HttpStatusCodes.NotFound), HttpContentType.TextType, Encoding.UTF8.GetByteCount(""));
            }

        }
    }
}
