namespace Backend_Handheld.Entities.DataTransferObjects.Result
{
    public class ResultCreateDto
    {
        public int ClassificationId { get; set; }
        public int UserId { get; set; }
        public bool Status { get; set; }
        public string? Image { get; set; }
    }
}
