namespace BookStore.Models;

public class Result<T>
{
    public Result(bool success = true, string message = "")
    {
        Success = success;
        Message = message;
    }

    public bool Success { get; set; }
    public string Message { get; set; }

    public T Output { get; set; }
}