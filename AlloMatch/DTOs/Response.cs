using System.Collections.Generic;

namespace AlloMatch.DTOs
{
    public class Response
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public Response() { }

        public Response(string message)
        {
            Succeeded = true;
            Message = message;
        }

        public Response(string message, IEnumerable<string> errors = null)
        {
            Succeeded = false;
            Message = message;
            Errors = errors;
        }
    }

    public class Response<T> : Response
    {
        public Response() { }

        public Response(string message, IEnumerable<string> errors = null) : base(message, errors) { }

        public Response(T data, string message = null) : base(message)
        {
            Data = data;
        }

        public T Data { get; set; }

        public static Response<T> Failure(params string[] errors)
            => new Response<T>("Failure", errors);

        public static Response<T> Success(T data)
            => new Response<T>(data, "Success");
    }
}
