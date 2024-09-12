using Backend_Handheld.Entities.DataTransferObjects.Result;
using Backend_Handheld.Entities.Models;

namespace HandheldProject.Entities.DataTransferObjects.Results
{
    public class ResultConditionDto
    {
        //public int Year { get; set; }
        //public int Month { get; set; }
        //public int Day { get; set; }
        public int ClassificationId { get; set; }
        public bool Status { get; set; }
        public List<string> Dates { get; set; }
        public List<ResultDto> Results { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CountOK {  get; set; }
        public int CountNG {  get; set; }
    }
}
