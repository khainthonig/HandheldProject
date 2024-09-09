namespace Backend_Handheld.Entities.DataTransferObjects.Classification
{
    public class ClassificationSearchDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<int>? IdLst { get; set; }
    }
}
