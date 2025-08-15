namespace MediaTracker.Server.Models
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AuthorFirst { get; set; }
        public string AuthorLast { get; set; }
        public string ISBN { get; set; }
        public string PublicationDate { get; set; }
        public string Publisher { get; set; }
        public int Series { get; set; }
    }
}
