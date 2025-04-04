﻿using codecrafters_http_server.src.Application.Interfaces;
using codecrafters_http_server.src.Domain.Entities;
using HttpMethod = codecrafters_http_server.src.Domain.Entities.HttpMethod;


namespace codecrafters_http_server.src.Application.Services.Routes.GET
{
    class NotFoundRouteHandler : IHttpRouteHandler
    {
        public override string _route { get; set; } = null;

        public override HttpMethod _method { get; set; } = HttpMethod.Create(HttpMethod.GET).Value;


        public override HttpResponse HandleRoute(HttpRequest request)
        {
            return new HttpResponse(new HttpStatusCode(HttpStatusCode.NotFound), HttpContentType.TextType);
        }
    }
}
