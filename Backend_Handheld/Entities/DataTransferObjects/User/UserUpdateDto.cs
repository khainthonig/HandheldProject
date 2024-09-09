namespace Backend_Handheld.Entities.DataTransferObjects.User
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public int OperatorId { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
    }
}
