namespace MediaTracker.Server.Models
{
    public class SeriesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int Length { get; set; }
    }
}
