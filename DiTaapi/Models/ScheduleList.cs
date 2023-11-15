using System.Text.Json.Serialization;

namespace DTVPortalAPI.Models
{
    public class API_ScheduleList
    {
        
        public List<ScheduleList> Data = new(); 
        public bool Successs { get; set; }
        public string? Message { get; set; }
        public TimeSpan TotalDuration { get; set; } 
        public int TotalMediaSize { get; set; }

    }

    public class ScheduleList
    {
        public int MediaID { get; set; }
        public string? MediaName { get; set; }
        public string? MediaType { get; set; }
        public string? MediaDuration { get; set; }
        public string? MediaLocation { get; set; }
        public int MediaSize { get; set; }
    }
}
