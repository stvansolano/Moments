namespace Backend
{
    //using Microsoft.WindowsAzure.Storage.Table;

    public class UserEntity //: TableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public bool SendMoment { get; set; }
    }
}