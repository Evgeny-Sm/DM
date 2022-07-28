namespace DocumentMaster.BlazorServer.Services
{
    public class InvalidUserService
    {
        public string Status { get; set; } = String.Empty;
        public void SetStatus(string message)
        {
            Status = message;
        }
    }
}
