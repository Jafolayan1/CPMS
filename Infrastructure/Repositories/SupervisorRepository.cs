﻿using Domain.Entities;
using Domain.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SupervisorRepository : GenericRepository<Supervisor>, ISupervisorRepository
    {
        public SupervisorRepository(ApplicationContext context) : base(context)
        {
        }

        public override Supervisor GetById(object id)
        {
            return _context.Supervisors.Include(u => u.User).Include(d => d.Department).Include(s => s.ProjectStudents).ThenInclude(s => s.Projects).AsNoTracking().FirstOrDefault();
        }
    }
}