using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace MediaTracker.Server.Entities
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name of the book is required.")]
        public string Name { get; set; }
        public string AuthorFirst {  get; set; }
        public string AuthorLast { get; set; }
        public string ISBN { get; set; }
        public string PublicationDate { get; set; }
        public string Publisher {  get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public int SeriesId { get; set; }
        public Series Series { get; set; } = null!;

    }
}
