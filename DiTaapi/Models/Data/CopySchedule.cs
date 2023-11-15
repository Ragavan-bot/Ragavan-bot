namespace DTVPortalAPI.Models.Data
{
    public class CopySchedule
    {
        public DateTime? CopyStartDate { get; set; }
        public DateTime? CopyEndDate { get; set; }

        public DateTime? ActiveStartDate { get; set; }
        public DateTime? ActiveEndDate { get; set; }
        public string? ScheduleHeaderID { get; set; }
        public string? ScheduleLibraryID { get; set; }

        public string? TVName { get; set; }
    }
}