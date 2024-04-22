namespace ContactHub.Model.DTOs
{
    public class LoginResult
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
