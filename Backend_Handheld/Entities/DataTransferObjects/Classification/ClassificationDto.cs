namespace Backend_Handheld.Entities.DataTransferObjects.Classification
{
    public class ClassificationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
