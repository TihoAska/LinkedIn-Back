using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.Languages;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Locations;
using LinkedIn.Models.ProfileDetails.Skills;

namespace LinkedIn.Services.IServices
{
    public interface IProfileService
    {
        Task<IEnumerable<Experience>> GetAllExperiencesByUserId(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<UserEducation>> GetAllEducationsByUserId(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<LicensesAndCertifications>> GetAllLicensesAndCertificationsByUserId(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<UserSkills>> GetAllSkillsByUserId(int userId, CancellationToken cancellationToken);
        Task<IEnumerable<UserLanguages>> GetAllLanguagesByUserId(int userId, CancellationToken cancellationToken);
        Task<Experience> CreateExperienceForUser(ExperienceCreateRequest createRequest, CancellationToken cancellationToken);
        Task<UserEducation> CreateEducationForUser(EducationCreateRequest createRequest, CancellationToken cancellationToken);
        Task<LicensesAndCertifications> CreateLicensesAndCertificationsForUser(LicensesCreateRequest createRequest, CancellationToken cancellationToken);
        Task<UserSkills> CreateSkills(SkillsCreateRequest createRequest, CancellationToken cancellationToken);
        Task<UserLanguages> CreateLanguages(LanguagesCreateRequest createRequest, CancellationToken cancellationToken);
        Task<Experience> EditExperienceForUser(ExperienceUpdateRequest updateRequest, CancellationToken cancellationToken);
    }
}
