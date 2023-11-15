namespace DTVPortalAPI.Models.Data
{
    public class Schedule
    { 
        public string? ID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? AllDay { get; set; }
        public string? Subject { get; set;}
        public string? Location { get; set; }
        public string? Description { get; set; }
        public string? ResourceID { get; set; }
        public string? ScheduleHeaderID { get; set;}
        public string? ScheduleLibraryID { get; set;}
    }
}
