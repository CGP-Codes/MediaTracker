namespace MediaTracker.Server.Models
{
    public class BookDetailsViewModel : BookViewListModel
    {
        public string ISBN { get; set; }
        public string PublicationDate { get; set; }
        public string Publisher { get; set; }
    }
}
