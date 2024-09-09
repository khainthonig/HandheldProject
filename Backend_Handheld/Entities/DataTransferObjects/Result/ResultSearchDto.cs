namespace Backend_Handheld.Entities.DataTransferObjects.Result
{
    public class ResultSearchDto
    {
        public int? Id { get; set; }
        public int? ClassificationId { get; set; }
        public int? UserId { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
