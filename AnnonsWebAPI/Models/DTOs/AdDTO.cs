namespace AnnonsWebAPI.Models.DTOs
{
    public class AdDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TargetUrl { get; set; }
        public DateTime StartDate { get; set; }
    }
}
