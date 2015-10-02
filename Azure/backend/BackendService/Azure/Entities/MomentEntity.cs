namespace Backend
{
    using System;
    using Microsoft.WindowsAzure.Storage.Table;

    public class MomentEntity : TableEntity
    {
		public string Id { get; set; }
		public string MomentUrl { get; set; }
		public string SenderUserId { get; set; }
		public string SenderName { get; set; }
		public string SenderProfileImage { get; set; }
		public string RecipientUserId { get; set; }
		public int DisplayTime { get; set; }
		public DateTime TimeSent { get; set; }
	}
}