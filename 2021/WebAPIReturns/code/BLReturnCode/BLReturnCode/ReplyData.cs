namespace BLReturnCode
{
    public class ReplyData<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T ReturnObject { get; set; }
    }

}
