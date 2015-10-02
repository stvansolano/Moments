using System;
using System.Collections.Generic;

namespace Backend
{
    public class FriendshipRepository : RepositoryBase<FriendshipEntity>
    {
        public FriendshipRepository() : base("Friendship")
        {

        }

        internal IEnumerable<FriendshipEntity> Where(Predicate<FriendshipEntity> predicate)
        {
            return new FriendshipEntity[0];
        }
    }
}