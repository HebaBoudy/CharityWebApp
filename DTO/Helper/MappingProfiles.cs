using AutoMapper;
using WebAppTutorial.Models;
using Shared.Models;
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
            CreateMap<Campaign, CampaignDto>();
            CreateMap<CampaignDto, Campaign>();




        }
    }
}
