namespace MediaTracker.Server.Entities
{
    public class Series
    {
        public Series() 
        {
            Books = new List<Book>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int Length { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }

        public IList<Book> Books { get; set; }
    }
}
