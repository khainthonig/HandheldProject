namespace Backend_Handheld.Entities.DataTransferObjects.Result
{
    public class ResultSearchDto
    {
        public int? Id { get; set; }
        public int? ClassificationId { get; set; }
        public int? UserId { get; set; }
        public bool? Status { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? Day { get; set; }
        public List<int>? IdLst { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
