using AutoMapper;
using WebAppTutorial.Models;

namespace WebAppTutorial.DTO.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap <Company, CompanyDto>();
            CreateMap<Login, LoginDto>();
            CreateMap< CompanyDto,Company>();
            CreateMap<LoginDto, Login>();
            CreateMap<UsersRegistrationDto, UsersRegistration>();
            CreateMap<UsersRegistration, UsersRegistrationDto>();
       



        }
    }
}
