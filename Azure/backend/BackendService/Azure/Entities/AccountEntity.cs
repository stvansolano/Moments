namespace Backend
{
    using Microsoft.WindowsAzure.Storage.Table;

    public class AccountEntity : TableEntity
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }
    }
}