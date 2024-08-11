namespace Common.Responses
{
    public class BaseApiResponse<T>: BaseApiResponse
    {
        public T Data { get; set; }
    }

    public class BaseApiResponse
    {
        public string Message { get; set; }
        public int Status { get; set; }
    }
}
