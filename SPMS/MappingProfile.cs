using AutoMapper;

using Domain.Entities;

using SPMS.Models;

namespace CPMS
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<StudentVM, Student>();
			CreateMap<SupervisorVM, Supervisor>();
			CreateMap<ProjectVM, Project>();
		}
	}
}