namespace ContactHub.Model.Common
{
    public class ReturnObject <T>
    {
        public bool status {  get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
        public T Data { get; set; }
    }
}
