using System.Net;

namespace ConsoleApp2.Http
{
    public interface IResponse<out T>
    {
        int StatusCode { get; }
        T Value { get; }
    }

    public class Response<T> : IResponse<T>
    {
        internal Response(HttpStatusCode statusCode, T value)
            : this((int)statusCode, value)
        {

        }

        internal Response(int statusCode, T value)
        {
            StatusCode = statusCode;
            Value = value;
        }

        public int StatusCode { get; }
        public T Value { get; }
    }
}