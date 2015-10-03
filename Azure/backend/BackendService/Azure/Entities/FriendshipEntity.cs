namespace Backend
{
    public class FriendshipEntity : StorageEntity
    {
        //TODO: Allocate properties: Friendship
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public bool Accepted { get; set; }

        public FriendshipEntity()
        {
            Map = (storage, entity) => {
                var friendship = (FriendshipEntity)entity;

                friendship.Accepted = FromStoredPropertyBoolean(storage, "Accepted").GetValueOrDefault(false);
                friendship.FriendId = FromStoredPropertyString(storage, "FriendId");
                friendship.Id = FromStoredPropertyString(storage, "Id");
                friendship.UserId = FromStoredPropertyString(storage, "UserId");
            };
        }
    }
}