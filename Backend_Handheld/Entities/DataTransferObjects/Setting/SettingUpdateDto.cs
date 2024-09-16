namespace HandheldProject.Entities.DataTransferObjects.Setting
{
    public class SettingUpdateDto
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? ClassificationId { get; set; }
        public double? FocusValue { get; set; }
        public double? ExposureValue { get; set; }
    }
}
