﻿namespace Backend_Handheld.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int OperatorId { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
