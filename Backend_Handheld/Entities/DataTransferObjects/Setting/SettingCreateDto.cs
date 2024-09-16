namespace HandheldProject.Entities.DataTransferObjects.Setting
{
    public class SettingCreateDto
    {
        public int? UserId { get; set; }
        public int? ClassificationId { get; set; }
        public double? FocusValue { get; set; }
        public double? ExposureValue { get; set; }
    }
}
