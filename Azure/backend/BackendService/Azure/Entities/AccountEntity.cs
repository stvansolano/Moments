namespace Backend
{
    public class AccountEntity : StorageEntity
    {
        //TODO: Allocate properties: Account
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserId { get; set; }

        public AccountEntity()
        {
            Map = (storage, entity) => {
                var account = (AccountEntity)entity;

                account.Id = FromStoredPropertyString(storage, "Id");
                account.Username = FromStoredPropertyString(storage, "Username");
                account.Password = FromStoredPropertyString(storage, "Password");
                account.Email = FromStoredPropertyString(storage, "Email");
                account.UserId = FromStoredPropertyString(storage, "UserId");
            };
        }
    }
}