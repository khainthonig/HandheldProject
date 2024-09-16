namespace HandheldProject.Entities.DataTransferObjects.Setting
{
    public class SettingDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public int? ClassificationId { get; set; }
        public string ClassificationName { get; set; }
        public double FocusValue { get; set; }
        public double ExposureValue { get; set; }
    }
}
