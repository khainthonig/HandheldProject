namespace Backend_Handheld.Entities.DataTransferObjects.Result
{
    public class ResultDto
    {
        public int Id { get; set; }
        public int? ClassificationId { get; set; }
        public string ClassificationName { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Image { get; set; }
    }
}
