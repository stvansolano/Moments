namespace Backend
{
    //using Microsoft.WindowsAzure.Storage.Table;

    public class FriendshipEntity //: TableEntity
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FriendId { get; set; }

        public bool Accepted { get; set; }
    }
}