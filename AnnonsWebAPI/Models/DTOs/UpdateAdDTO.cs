namespace AnnonsWebAPI.Models.DTOs
{
    public class UpdateAdDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TargetUrl { get; set; }
        public DateTime StartDate { get; set; }
    }
}
