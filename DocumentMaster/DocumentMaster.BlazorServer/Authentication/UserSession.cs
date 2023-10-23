namespace DocumentMaster.BlazorServer.Authentication
{
    public class UserSession
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PersonId { get; set; }
    }
}
