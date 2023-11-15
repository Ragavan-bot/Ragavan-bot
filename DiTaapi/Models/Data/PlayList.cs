namespace DTVPortalAPI.Models.Data
{
    public class PlayList
    {
        public string? CompanyID { get; set; }
        public string? ScheduleHeaderID { get; set; }
        public string? ScheduleHeaderName { get; set;}

        public string? OldOrderNo { get; set; }
        public string? ScheduleDetailID { get; set; }
        public string? OrderNo { get; set; }
        public string? MediaID { get; set;}
        public string? MediaName { get; set; }
        public string? MediaDuration { get; set;}
        public string? MediaType { get; set; }

        public string? TotalDuration { get; set;}
    } 
}
