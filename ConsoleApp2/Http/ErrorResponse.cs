using System;
using System.Net;

namespace ConsoleApp2.Http
{
    public interface IErrorResponse<out T> : IResponse<T>
    {
        string Error { get; }
        string OriginalData { get; }
        Exception Exception { get; }
        bool HasException { get; }
    }

    public class ErrorResponse<T> : IErrorResponse<T>
    {
        internal ErrorResponse(HttpStatusCode statusCode, string error)
            : this((int) statusCode, error)
        {

        }

        internal ErrorResponse(int statusCode, string error)
        {
            StatusCode = statusCode;
            Error = error;
        }

        public int StatusCode { get; }
        public string Error { get; }

        public string OriginalData { get; internal set; }
        public Exception Exception { get; internal set; }
        public bool HasException => Exception != null;

        // Explicit implementation to hide access to this redundant member when using ErrorResponse<T>
        T IResponse<T>.Value => default;
    }
}