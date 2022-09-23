﻿namespace Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string? Surname { get; set; }
        public string? OtherNames { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
    }
}
