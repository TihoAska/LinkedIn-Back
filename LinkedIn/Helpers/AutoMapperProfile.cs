using AutoMapper;
using LinkedIn.Models.Pages;
using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.Languages;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Locations;
using LinkedIn.Models.ProfileDetails.Skills;
using LinkedIn.Models.Users;

namespace LinkedIn.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserCreateRequest, User>();

            CreateMap<UserUpdateRequest, User>();

            CreateMap<PageCreateRequest, Page>();

            CreateMap<EducationCreateRequest, UserEducation>();

            CreateMap<ExperienceCreateRequest, Experience>()
                .ForMember(dest => dest.CompanyLocationId, opt => opt.MapFrom(src => src.Location.Id))
                .ForMember(dest => dest.Location, opt => opt.Ignore());

            CreateMap<LanguagesCreateRequest, UserLanguages>();

            CreateMap<LicensesCreateRequest, LicensesAndCertifications>();

            CreateMap<CompanyLocationCreateRequest, CompanyLocation>();

            CreateMap<SkillsCreateRequest, UserSkills>();
        }
    }
}
