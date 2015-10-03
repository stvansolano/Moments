namespace Backend
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class MomentRepository : RepositoryBase<MomentEntity>
    {
        public MomentRepository() : base("Moment")
        {

        }

        public new IEnumerable<Moment> Find(Predicate<MomentEntity> predicate)
        {
            return (from entity in base.Find(predicate)
                    select FromEntity(entity)).ToArray();
        }

        public new async Task<IEnumerable<Moment>> GetAll()
        {
            return (from entity in await base.GetAll()
                    select FromEntity(entity)).ToArray();
        }

        private Moment FromEntity(MomentEntity entity)
        {
            return new Moment
            {
                DisplayTime = entity.DisplayTime,
                Id = entity.Id,
                MomentUrl = entity.MomentUrl,
                SenderName = entity.SenderName,
                SenderUserId = entity.SenderUserId,
                RecipientUserId = entity.RecipientUserId,
                SenderProfileImage = entity.SenderProfileImage,
                TimeSent = entity.TimeSent
            };
        }
    }
}