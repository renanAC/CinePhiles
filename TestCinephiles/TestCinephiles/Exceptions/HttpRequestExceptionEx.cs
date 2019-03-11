using System;
using System.Net;
using System.Net.Http;

namespace TestCinephiles.Exceptions
{
    public class HttpRequestExceptionEx : HttpRequestException
    {
        public HttpStatusCode StatusCode { get; }

        public HttpRequestExceptionEx(HttpStatusCode statusCode) : this(statusCode, null, null)
        {
        }

        public HttpRequestExceptionEx(HttpStatusCode statusCode, string message) : this(statusCode, message, null)
        {
        }

        public HttpRequestExceptionEx(HttpStatusCode statusCode, string message, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
