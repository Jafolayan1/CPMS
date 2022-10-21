﻿using Microsoft.AspNetCore.Http;

using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Topic { get; set; }
        public string Matric { get; set; }
        public string Status { get; set; }
        public string? Remark { get; set; }
        public string FileUrl { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public int? SupervisorId { get; set; }
        public virtual Supervisor Supervisor { get; set; }
        public DateTime DateSubmitted { get; set; } = DateTime.UtcNow;
    }
}