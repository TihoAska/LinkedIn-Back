﻿using LinkedIn.Models.ProfileDetails.Educations;
using LinkedIn.Models.ProfileDetails.Experiences;
using LinkedIn.Models.ProfileDetails.Images;
using LinkedIn.Models.ProfileDetails.Languages;
using LinkedIn.Models.ProfileDetails.LicensesAndCerfitications;
using LinkedIn.Models.ProfileDetails.Locations;
using LinkedIn.Models.ProfileDetails.Skills;
using Microsoft.AspNetCore.Mvc;

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
        Task<IEnumerable<Skill>> GetAllSkills(CancellationToken cancellationToken);
        Task<Skill> CreateSkill(SkillsCreateRequest createRequest, CancellationToken cancellationToken);
        Task<UserSkills> CreateSkillForUser(SkillForUserCreateRequest createRequest, CancellationToken cancellationToken);
        Task<bool> DeleteUserSkill(int userId, string skillName, string skillDescription, CancellationToken cancellationToken);
        Task<UserLanguages> CreateLanguageForUser(LanguagesCreateRequest createRequest, CancellationToken cancellationToken);
        Task<Experience> EditExperienceForUser(ExperienceUpdateRequest updateRequest, CancellationToken cancellationToken);
        Task<IEnumerable<Institution>> GetAllInstitutions(CancellationToken cancellationToken);
        Task<UserEducation> EditEducationForUser(EducationUpdateRequest updateRequest, CancellationToken cancellationToken);
        Task<LicensesAndCertifications> EditLicenseOrCertificationForUser(LicenseUpdateRequest updateRequest, CancellationToken cancellationToken);
        Task<UserLanguages> EditLanguage(LanguagesUpdateRequest updateRequest, CancellationToken cancellationToken);
        Task<BackgroundImage> ChangeBackgroundImage(BackgroundImageUpdateRequest updateRequest, CancellationToken cancellationToken);
        Task<bool> DeleteLanguageForUser(int userId, int languageId, CancellationToken cancellationToken);
        Task<bool> DeleteEducationForUser(int educationId, int userId, CancellationToken cancellationToken);
        Task<bool> DeleteExperienceForUser(int experienceId, int userId, CancellationToken cancellationToken);
        Task<bool> DeleteLicenseForUser(int userId, int licenseId, CancellationToken cancellationToken);
    }
}
