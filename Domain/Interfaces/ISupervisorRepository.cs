﻿using Domain.Entities;

namespace Domain.Interfaces
{
	public interface ISupervisorRepository : IGenericRepository<Supervisor>
	{
		Supervisor GetByFileNo(string id);
	}
}