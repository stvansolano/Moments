namespace Backend
{
    using System;

    public class MomentEntity : StorageEntity
    {
        //TODO: Allocate properties: Moment
        public string Id { get; set; }
		public string MomentUrl { get; set; }
		public string SenderUserId { get; set; }
		public string SenderName { get; set; }
		public string SenderProfileImage { get; set; }
		public string RecipientUserId { get; set; }
		public int DisplayTime { get; set; }
		public DateTime TimeSent { get; set; }

        public MomentEntity()
        {
            Map = (storage, entity) => {
                var account = (MomentEntity)entity;

                account.Id = FromStoredPropertyString(storage, "Id");
                account.MomentUrl = FromStoredPropertyString(storage, "MomentUrl");
                account.SenderUserId = FromStoredPropertyString(storage, "SenderUserId");
                account.SenderName = FromStoredPropertyString(storage, "SenderName");
                account.SenderProfileImage = FromStoredPropertyString(storage, "SenderProfileImage");
                account.RecipientUserId = FromStoredPropertyString(storage, "RecipientUserId");
                account.DisplayTime = FromStoredPropertyInt32(storage, "DisplayTime").GetValueOrDefault(0);
                account.TimeSent = FromStoredPropertyDateTime(storage, "TimeSent").GetValueOrDefault(DateTime.Now);
            };
        }
	}
}