namespace Core.Entities
{
    public class Result<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? Error { get; set; }


        public static Result<T> Success(T data) => new() { IsSuccess = true, Data = data };
        public static Result<T> Fail(string error) => new() { IsSuccess = false, Error = error };
    }
}
