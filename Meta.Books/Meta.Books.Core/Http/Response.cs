namespace Meta.Books.Core.Http;

public class Response<T>
{
    public T data { get; set; }
    public string message { get; set; } = "";
    public List<string> errors { get; set; } = new List<string>();
}