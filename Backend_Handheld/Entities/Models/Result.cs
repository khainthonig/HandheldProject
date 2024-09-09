namespace Backend_Handheld.Entities.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int ClassificationId { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
    }
}
