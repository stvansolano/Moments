namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserRepository : RepositoryBase<UserEntity>
    {
        public UserRepository() : base("User")
        {

        }

        internal bool Add(User model)
        {
            return true;
        }

        public new IEnumerable<User> Find(Predicate<UserEntity> predicate)
        {
            return (from entity in base.Find(predicate)
                    select FromEntity(entity)).ToArray();
        }

        public new IEnumerable<User> GetAll()
        {
            return (from entity in base.GetAll()
                    select FromEntity(entity)).ToArray();
        }

        private User FromEntity(UserEntity entity)
        {
            return new User
            {
                Id = entity.Id,
                Name = entity.Name,
                ProfileImage = entity.ProfileImage,
                SendMoment = entity.SendMoment
            };
        }
    }
}