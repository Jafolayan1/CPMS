using AutoMapper;

using CPMS.Models;

using Domain.Entities;

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