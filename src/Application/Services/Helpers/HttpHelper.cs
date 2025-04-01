﻿using codecrafters_http_server.src.Domain.Entities;
using System.Text;

namespace codecrafters_http_server.src.Application.Services.Helpers
{
    public class HttpHelper
    {
        public static string BuildHeaders(HttpResponse? response)
        {
            if(response.ContentEncoding != null)
            {
                return $"{response.StatusCode}\r\nContent-Type: {response.ContentType}\r\nContent-Length: {response.SizeInBytes}\r\nContent-Encoding:{response.ContentEncoding}\r\n\r\n"; 
            }
            return $"{response.StatusCode}\r\nContent-Type: {response.ContentType}\r\nContent-Length: {response.SizeInBytes}\r\n\r\n"; 
        }
    }
}
